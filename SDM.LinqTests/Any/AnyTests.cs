namespace SDM.LinqTests.Any
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class AnyTests
	{
		[TestMethod]
		public void Any_NoFilter_WithElements_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Any();

			// Assert
			result.Should().BeTrue();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_WithMatchingFilter_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Any(x => x.Age > 20);

			// Assert
			result.Should().BeTrue();
		}

		[TestMethod]
		public void Any_WithNonMatchingFilter_ReturnsFalse()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Any(x => x.Age > 1000);

			// Assert
			result.Should().BeFalse();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_WithWhere_MatchingFilter_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice")
				.Any();

			// Assert
			result.Should().BeTrue();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice")
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_WithWhere_NonMatchingFilter_ReturnsFalse()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 1000)
				.Any();

			// Assert
			result.Should().BeFalse();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_CombinedFilters_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20)
				.Any(x => x.Name == "Alice");

			// Assert
			result.Should().BeTrue();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ANDFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(20),
					TestClassExposers.Name.Equal("Alice"))
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_WithMatchingSize_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size == 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.Equal(30)
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_WithDescription_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Description == "SubFirst-1"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Description.Equal("SubFirst-1")
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_WithGreaterThan_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size > 20))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.GreaterThan(20)
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_NoMatches_ReturnsEmpty()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size > 1000))
				.ToArray();

			// Assert
			result.Should().BeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.GreaterThan(1000)
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_CombinedWithTopLevelFilter_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 25)
				.Where(x => x.SubClasses.Any(t => t.Size < 10))
				.ToArray();

			// Assert
			result.Should().NotBeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ANDFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(25),
					TestClassExposers.SubClasses.Size.LessThan(10))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_WithComplexCondition_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Description.Contains("Second")))
				.ToArray();

			// Assert
			result.Should().NotBeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Description.Contains("Second")
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Any_NestedCollection_MultipleLevels_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive)
				.Where(x => x.SubClasses.Any(t => t.Size >= 5 && t.Size <= 15))
				.ToArray();

			// Assert
			result.Should().NotBeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ANDFilterElement<TestClass>(
					TestClassExposers.IsActive.Equal(true),
					new ANDFilterElement<TestClass>(
						TestClassExposers.SubClasses.Size.GreaterThanOrEqual(5),
						TestClassExposers.SubClasses.Size.LessThanOrEqual(15)))
				.ToQuery()
				.ToCompleteString());
		}
	}
}
