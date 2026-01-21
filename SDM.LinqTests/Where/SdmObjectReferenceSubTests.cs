namespace SDM.LinqTests.Where
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class SdmObjectReferenceSubTests
	{
		[TestMethod]
		public void Where_SdmObjectReference_Equal()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = new SdmObjectReference<TestClass>(dummyData.First().Identifier);
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference == reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().Be(reference));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.Equal(reference).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_SdmObjectReference_NotEqual()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = new SdmObjectReference<TestClass>(dummyData.First().Identifier);
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference != reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().NotBe(reference));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.NotEqual(reference).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_Equal()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = dummyData.First().Identifier;
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference == reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().Be(new SdmObjectReference<TestClass>(reference)));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.Equal(new SdmObjectReference<TestClass>(reference)).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotEqual()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = dummyData.First().Identifier;
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference != reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().NotBe(new SdmObjectReference<TestClass>(reference)));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.NotEqual(new SdmObjectReference<TestClass>(reference)).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_SdmObject_Equal()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = dummyData.First();
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference == reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().Be(SdmObjectReference<TestClass>.Convert(reference)));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.Equal(reference).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_SdmObject_NotEqual()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var reference = dummyData.First();
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Reference != reference)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Reference.Should().NotBe(SdmObjectReference<TestClass>.Convert(reference)));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Reference.NotEqual(reference).ToQuery().ToCompleteString());
		}
	}
}
