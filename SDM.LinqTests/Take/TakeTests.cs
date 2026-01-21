namespace SDM.LinqTests.Take
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class TakeTests
	{
		[TestMethod]
		public void Take_LimitResults_ReturnsSpecifiedCount()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Take(5)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCountLessOrEqualTo(5);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(5)
				.ToCompleteString());
		}

		[TestMethod]
		public void Take_WithWhere_LimitResults()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20)
				.Take(3)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCountLessOrEqualTo(3);
			result.Should().AllSatisfy(t => t.Age.Should().BeGreaterThan(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(20)
				.ToQuery()
				.Limit(3)
				.ToCompleteString());
		}

		[TestMethod]
		public void Take_Zero_ReturnsEmpty()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Take(0)
				.ToArray();

			// Assert
			result.Should().BeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(0)
				.ToCompleteString());
		}

		[TestMethod]
		public void Take_MoreThanAvailable_ReturnsAll()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 1000)
				.Take(100)
				.ToArray();

			// Assert
			result.Should().BeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(100)
				.ToCompleteString());
		}

		[TestMethod]
		public void Take_One_ReturnsSingleElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Take(1)
				.ToArray();

			// Assert
			result.Should().HaveCountLessOrEqualTo(1);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Take_AfterOrderBy_ReturnsLimitedOrderedResults()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.OrderBy(x => x.Age)
				.Take(3)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCountLessOrEqualTo(3);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.OrderBy(TestClassExposers.Age)
				.Limit(3)
				.ToCompleteString());
		}
	}
}
