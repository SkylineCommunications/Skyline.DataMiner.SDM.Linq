namespace SDM.LinqTests
{
	using System;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using System.Text;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
	using Skyline.DataMiner.Net.Apps.Modules;
	using Skyline.DataMiner.Net.ManagerStore;
	using Skyline.DataMiner.Net.Messages;
	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.Net.Sections;

	public class DomImporter
	{
		private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			ContractResolver = new DefaultContractResolver { IgnoreSerializableInterface = true },
		});

		private readonly ModuleSettingsHelper moduleSettingsHelper;
		private readonly Func<DMSMessage[], DMSMessage[]> sendSLNetMessages;
		private DomHelper domHelper;
		private JsonTextReader jsonTextReader;

		public DomImporter(Func<DMSMessage[], DMSMessage[]> sendSLNetMessages)
		{
			this.sendSLNetMessages = sendSLNetMessages ?? throw new ArgumentNullException(nameof(sendSLNetMessages));
			moduleSettingsHelper = new ModuleSettingsHelper(sendSLNetMessages);
		}

		public void Import(string path)
		{
			using (var reader = new Reader(path))
			{
				jsonTextReader = reader.JsonTextReader;
				jsonTextReader.Read(); // start array
				while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
				{
					ImportModule();
					jsonTextReader.Read(); // end object
				}
			}
		}

		private void Import<T>(ICrudHelperComponent<T> crudHelperComponent, FilterElement<T> equalityFilter, T dataType)
			where T : DataType
		{
			bool exists = crudHelperComponent.Read(equalityFilter).Any();

			if (exists)
			{
				crudHelperComponent.Update(dataType);
			}
			else
			{
				crudHelperComponent.Create(dataType);
			}
		}

		private void ImportDomBehaviorDefinitions()
		{
			jsonTextReader.Read();
			jsonTextReader.Read();
			while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
			{
				var behaviorDefinition = JsonSerializer.Deserialize<DomBehaviorDefinition>(jsonTextReader);
				Import(
					domHelper.DomBehaviorDefinitions,
					DomBehaviorDefinitionExposers.Id.Equal(behaviorDefinition.ID),
					behaviorDefinition);
			}
		}

		private void ImportDomDefinitions()
		{
			jsonTextReader.Read();
			jsonTextReader.Read();
			while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
			{
				var domDefinition = JsonSerializer.Deserialize<DomDefinition>(jsonTextReader);
				Import(domHelper.DomDefinitions, DomDefinitionExposers.Id.Equal(domDefinition.ID), domDefinition);
			}
		}

		private void ImportDomTemplates()
		{
			jsonTextReader.Read();
			jsonTextReader.Read();
			while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
			{
				var domTemplate = JsonSerializer.Deserialize<DomTemplate>(jsonTextReader);
				Import(domHelper.DomTemplates, DomTemplateExposers.Id.Equal(domTemplate.ID), domTemplate);
			}
		}

		private void ImportDomInstances()
		{
			jsonTextReader.Read();
			jsonTextReader.Read();
			while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
			{
				var domInstance = JsonSerializer.Deserialize<DomInstance>(jsonTextReader);
				Import(domHelper.DomInstances, DomInstanceExposers.Id.Equal(domInstance.ID), domInstance);
			}
		}

		private void ImportModule()
		{
			jsonTextReader.Read(); // property name
			jsonTextReader.Read(); // start object
			var moduleSettings = JsonSerializer.Deserialize<ModuleSettings>(jsonTextReader);
			ImportModuleSettings(moduleSettings);
			domHelper = new DomHelper(sendSLNetMessages, moduleSettings.ModuleId);

			ImportSectionDefinitions();
			ImportDomBehaviorDefinitions();
			ImportDomDefinitions();
			ImportDomTemplates();
			ImportDomInstances();
		}

		private void ImportModuleSettings(ModuleSettings moduleSettings)
		{
			Import(
				moduleSettingsHelper.ModuleSettings,
				ModuleSettingsExposers.ModuleId.Equal(moduleSettings.ModuleId),
				moduleSettings);
		}

		private void ImportSectionDefinitions()
		{
			jsonTextReader.Read();
			jsonTextReader.Read();
			while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
			{
				var sectionDefinition = JsonSerializer.Deserialize<CustomSectionDefinition>(jsonTextReader);
				Import(
					domHelper.SectionDefinitions,
					SectionDefinitionExposers.ID.Equal(sectionDefinition.ID),
					sectionDefinition);
			}
		}

		private sealed class Reader : IDisposable
		{
			private readonly Stream stream;
			private readonly StreamReader streamReader;

			public Reader(string path)
			{
				try
				{
					stream = new FileStream(path, FileMode.Open);
					streamReader = new StreamReader(stream, Encoding.UTF8);
					JsonTextReader = new JsonTextReader(streamReader);
					JsonTextReader.SupportMultipleContent = true;
				}
				catch
				{
					Dispose();
					throw;
				}
			}

			public JsonTextReader JsonTextReader { get; }

			public void Dispose()
			{
				((IDisposable)JsonTextReader)?.Dispose();
				streamReader?.Dispose();
				stream?.Dispose();
			}
		}
	}
}
