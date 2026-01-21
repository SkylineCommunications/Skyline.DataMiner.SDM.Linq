namespace SDM.LinqTests.Mock
{
	using System.Collections.ObjectModel;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Types.Querying;

	internal class ReadTracker<T> : IReadableMiddleware<T>, ICountableMiddleware<T>
		where T : SdmObject<T>
	{
		private readonly List<FilterElement<T>> _filters = new List<FilterElement<T>>();
		private readonly List<IQuery<T>> _queries = new List<IQuery<T>>();

		public IReadOnlyList<FilterElement<T>> Filters { get => new ReadOnlyCollection<FilterElement<T>>(_filters); }

		public IReadOnlyList<IQuery<T>> Queries { get => new ReadOnlyCollection<IQuery<T>>(_queries); }

		public long OnCount(FilterElement<T> filter, Func<FilterElement<T>, long> next)
		{
			_filters.Add(filter);
			return next(filter);
		}

		public long OnCount(IQuery<T> query, Func<IQuery<T>, long> next)
		{
			_queries.Add(query);
			return next(query);
		}

		public IEnumerable<T> OnRead(FilterElement<T> filter, Func<FilterElement<T>, IEnumerable<T>> next)
		{
			_filters.Add(filter);
			return next(filter);
		}

		public IEnumerable<T> OnRead(IQuery<T> query, Func<IQuery<T>, IEnumerable<T>> next)
		{
			_queries.Add(query);
			return next(query);
		}
	}
}
