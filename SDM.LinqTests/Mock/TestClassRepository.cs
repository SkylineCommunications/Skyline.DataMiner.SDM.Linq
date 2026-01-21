namespace SDM.LinqTests.Mock
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.CompilerServices;

	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.SDM;
	using Skyline.DataMiner.SDM.Linq;

	internal interface ITestClassRepository : IRepository<TestClass>, IQueryableRepository<TestClass>
	{
	}

	internal class TestClassRepository : DummyRepository<TestClass>, ITestClassRepository
	{
		public TestClassRepository()
			: base(DummyData.GetDummyData())
		{
		}

		public TestClassRepository(IEnumerable<TestClass> initialData)
			: base(initialData)
		{
		}

		public IQueryable<TestClass> Query()
		{
			RuntimeHelpers.RunClassConstructor(typeof(TestClassExposers).TypeHandle);
			return new RepositoryQuery<TestClass>(
				new RepositoryQueryProvider<TestClass>(this));
		}
	}
}
