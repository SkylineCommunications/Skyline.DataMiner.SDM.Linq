namespace SDM.LinqTests.OrderBy
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class OrderByTests
	{
		[TestMethod]
		public void OrderBy_Ascending_SortsCorrectly()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(t => t.Age)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInAscendingOrder(t => t.Age);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>().ToQuery().OrderBy(TestClassExposers.Age).ToCompleteString());
		}

		[TestMethod]
		public void OrderByDescending_Descending_SortsCorrectly()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderByDescending(t => t.Age)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInDescendingOrder(t => t.Age);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>().ToQuery().OrderByDescending(TestClassExposers.Age).ToCompleteString());
		}

		[TestMethod]
		public void OrderBy_WithWhere_SortsFilteredResults()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(t => t.Age > 20)
				.OrderBy(t => t.Name)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Age.Should().BeGreaterThan(20));
			result.Should().BeInAscendingOrder(t => t.Name);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(20)
				.ToQuery()
				.OrderBy(TestClassExposers.Name)
				.ToCompleteString());
		}

		[TestMethod]
		public void ThenBy_MultipleOrderings_AppliesSecondarySort()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(t => t.Status)
				.ThenBy(t => t.Age)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInAscendingOrder(t => t.Status).And.ThenBeInAscendingOrder(t => t.Age);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.Status)
				.ThenBy(TestClassExposers.Age)
				.ToCompleteString());
		}

		[TestMethod]
		public void ThenByDescending_MixedOrderings_AppliesCorrectly()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(x => x.Status)
				.ThenByDescending(x => x.Age)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInAscendingOrder(t => t.Status).And.ThenBeInDescendingOrder(t => t.Age);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.Status)
				.ThenByDescending(TestClassExposers.Age)
				.ToCompleteString());
		}

		[TestMethod]
		public void OrderBy_String_SortsAlphabetically()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(t => t.Name)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInAscendingOrder(t => t.Name);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.Name)
				.ToCompleteString());
		}

		[TestMethod]
		public void OrderBy_DateTime_SortsChronologically()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(t => t.CreatedAt)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInAscendingOrder(t => t.CreatedAt);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.CreatedAt)
				.ToCompleteString());
		}

		[TestMethod]
		public void OrderBy_WithTake_LimitsOrderedResults()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(t => t.Age)
				.Take(2)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCountLessThanOrEqualTo(2);
			result.Should().BeInAscendingOrder(t => t.Age);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.Age)
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void OrderByDescending_ThenBy_ComplexSorting()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderByDescending(x => x.IsActive)
				.ThenBy(x => x.Name)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().BeInDescendingOrder(t => t.IsActive).And.ThenBeInAscendingOrder(t => t.Name);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderByDescending(TestClassExposers.IsActive)
				.ThenBy(TestClassExposers.Name)
				.ToCompleteString());
		}
	}
}
