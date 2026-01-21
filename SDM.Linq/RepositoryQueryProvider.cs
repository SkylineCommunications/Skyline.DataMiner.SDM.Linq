namespace Skyline.DataMiner.SDM.Linq
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;

	using SLDataGateway.API.Types.Querying;

	public class RepositoryQueryProvider<T> : IQueryProvider
		where T : class
	{
		public RepositoryQueryProvider(IReadableRepository<T> provider)
		{
			Provider = provider ?? throw new ArgumentNullException(nameof(provider));
		}

		public IReadableRepository<T> Provider { get; }

		public IQueryable CreateQuery(Expression expression)
		{
			var elementType = expression.Type.GetGenericArguments()[0];
			var queryType = typeof(RepositoryQuery<>).MakeGenericType(elementType);
			return (IQueryable)Activator.CreateInstance(queryType, this, expression);
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (typeof(TElement) != typeof(T))
			{
				throw new InvalidOperationException($"Invalid element type: expected {typeof(T)}, but got {typeof(TElement)}.");
			}

			return (IQueryable<TElement>)new RepositoryQuery<T>(this, expression);
		}

		public object Execute(Expression expression)
		{
			return Execute<T>(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return RepositoryQueryExecutor<T, TResult>.Execute(this, expression);
		}

		internal FilterElement<T> CreateFilter(string path, Comparer comparer, object value)
		{
			var exposer = ExposerRegistry.findExposer(typeof(T), path);
			if (exposer is null)
			{
				throw new NotSupportedException($"Could not find an exposer for property with path '{path}'");
			}

			return FilterElementFactory.Create<T>(exposer, comparer, value);
		}

		internal IOrderByElement CreateOrderBy(string path, SortOrder sortOrder, bool naturalSort = false)
		{
			var exposer = ExposerRegistry.findExposer(typeof(T), path);
			if (exposer is null)
			{
				throw new NotSupportedException($"Could not find an exposer for property with path '{path}'");
			}

			return OrderByElementFactory.Create(exposer, sortOrder, naturalSort);
		}
	}
}
