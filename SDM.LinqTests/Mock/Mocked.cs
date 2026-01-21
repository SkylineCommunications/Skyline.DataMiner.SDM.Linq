namespace SDM.LinqTests.Mock
{
	using System.Collections.Generic;

	using SDM.LinqTests.Shared;

	internal static class Mocked
	{
		public static (ITestClassRepository, ReadTracker<TestClass>) CreateRepository()
		{
			var tracker = new ReadTracker<TestClass>();
			var repository = new TestClassRepository_Middleware(
				new TestClassRepository(),
				tracker);

			return (repository, tracker);
		}

		public static (ITestClassRepository, ReadTracker<TestClass>) CreateRepository(IEnumerable<TestClass> initialData)
		{
			var tracker = new ReadTracker<TestClass>();
			var repository = new TestClassRepository_Middleware(
				new TestClassRepository(initialData),
				tracker);

			return (repository, tracker);
		}
	}
}
