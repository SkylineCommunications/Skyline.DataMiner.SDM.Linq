namespace SDM.LinqTests.Mock
{
	using System.Runtime.CompilerServices;

	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;
	using Skyline.DataMiner.SDM.Linq;

	using SLDataGateway.API.Types.Querying;

	internal sealed class TestClassRepository_Middleware : ITestClassRepository
	{
		private readonly ITestClassRepository _inner;
		private readonly IMiddlewareMarker<TestClass> _middleware;

		public TestClassRepository_Middleware(ITestClassRepository inner, IMiddlewareMarker<TestClass> middleware)
		{
			_inner = inner ?? throw new ArgumentNullException(nameof(inner));
			_middleware = middleware;
		}

		public IEnumerable<IPagedResult<TestClass>> ReadPaged(FilterElement<TestClass> filter)
		{
			if (_middleware is IPageableMiddleware<TestClass> middleware)
			{
				return middleware.OnReadPaged(filter, _inner.ReadPaged);
			}
			else
			{
				return _inner.ReadPaged(filter);
			}
		}

		public IEnumerable<IPagedResult<TestClass>> ReadPaged(IQuery<TestClass> query)
		{
			if (_middleware is IPageableMiddleware<TestClass> middleware)
			{
				return middleware.OnReadPaged(query, _inner.ReadPaged);
			}
			else
			{
				return _inner.ReadPaged(query);
			}
		}

		public IEnumerable<IPagedResult<TestClass>> ReadPaged(FilterElement<TestClass> filter, int pageSize)
		{
			if (_middleware is IPageableMiddleware<TestClass> middleware)
			{
				return middleware.OnReadPaged(filter, pageSize, _inner.ReadPaged);
			}
			else
			{
				return _inner.ReadPaged(filter, pageSize);
			}
		}

		public IEnumerable<IPagedResult<TestClass>> ReadPaged(IQuery<TestClass> query, int pageSize)
		{
			if (_middleware is IPageableMiddleware<TestClass> middleware)
			{
				return middleware.OnReadPaged(query, pageSize, _inner.ReadPaged);
			}
			else
			{
				return _inner.ReadPaged(query, pageSize);
			}
		}

		public IEnumerable<TestClass> Read(FilterElement<TestClass> filter)
		{
			if (_middleware is IReadableMiddleware<TestClass> middleware)
			{
				return middleware.OnRead(filter, _inner.Read);
			}
			else
			{
				return _inner.Read(filter);
			}
		}

		public IEnumerable<TestClass> Read(IQuery<TestClass> query)
		{
			if (_middleware is IReadableMiddleware<TestClass> middleware)
			{
				return middleware.OnRead(query, _inner.Read);
			}
			else
			{
				return _inner.Read(query);
			}
		}

		public long Count(FilterElement<TestClass> filter)
		{
			if (_middleware is ICountableMiddleware<TestClass> middleware)
			{
				return middleware.OnCount(filter, _inner.Count);
			}
			else
			{
				return _inner.Count(filter);
			}
		}

		public long Count(IQuery<TestClass> query)
		{
			if (_middleware is ICountableMiddleware<TestClass> middleware)
			{
				return middleware.OnCount(query, _inner.Count);
			}
			else
			{
				return _inner.Count(query);
			}
		}

		public TestClass Create(TestClass oToCreate)
		{
			if (_middleware is ICreatableMiddleware<TestClass> middleware)
			{
				return middleware.OnCreate(oToCreate, _inner.Create);
			}
			else
			{
				return _inner.Create(oToCreate);
			}
		}

		public TestClass Update(TestClass oToUpdate)
		{
			if (_middleware is IUpdatableMiddleware<TestClass> middleware)
			{
				return middleware.OnUpdate(oToUpdate, _inner.Update);
			}
			else
			{
				return _inner.Update(oToUpdate);
			}
		}

		public void Delete(TestClass oToDelete)
		{
			if (_middleware is IDeletableMiddleware<TestClass> middleware)
			{
				middleware.OnDelete(oToDelete, _inner.Delete);
			}
			else
			{
				_inner.Delete(oToDelete);
			}
		}

		public IQueryable<TestClass> Query()
		{
			RuntimeHelpers.RunClassConstructor(typeof(TestClassExposers).TypeHandle);
			if (_middleware is IQueryableMiddleware<TestClass> middleware)
			{
				return middleware.OnQuery(() => new RepositoryQuery<TestClass>(
					new RepositoryQueryProvider<TestClass>(this)));
			}
			else
			{
				return new RepositoryQuery<TestClass>(
					new RepositoryQueryProvider<TestClass>(this));
			}
		}
	}
}
