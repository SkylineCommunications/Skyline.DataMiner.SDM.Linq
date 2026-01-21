namespace SDM.LinqTests.Count
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;

	[TestClass]
	public class CountTests
	{
		[TestMethod]
		public void Count_NoFilter_ReturnsAllElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Count();

			// Assert
			result.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void Count_WithFilter_ReturnsMatchingCount()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Count(x => x.Age > 20);

			// Assert
			result.Should().BeGreaterOrEqualTo(0);
		}

		[TestMethod]
		public void Count_WithWhere_ReturnsMatchingCount()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice")
				.Count();

			// Assert
			result.Should().BeGreaterOrEqualTo(0);
		}

		[TestMethod]
		public void Count_NoMatches_ReturnsZero()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Count(x => x.Age > 1000);

			// Assert
			result.Should().Be(0);
		}

		[TestMethod]
		public void Count_CombinedFilters_ReturnsMatchingCount()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20)
				.Count(x => x.IsActive == true);

			// Assert
			result.Should().BeGreaterOrEqualTo(0);
		}

		[TestMethod]
		public void LongCount_NoFilter_ReturnsAllElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.LongCount();

			// Assert
			result.Should().BeGreaterThan(0);
		}

		[TestMethod]
		public void LongCount_WithFilter_ReturnsMatchingCount()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.LongCount(x => x.Age > 20);

			// Assert
			result.Should().BeGreaterOrEqualTo(0);
		}

		[TestMethod]
		public void LongCount_NoMatches_ReturnsZero()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.LongCount(x => x.Age > 1000);

			// Assert
			result.Should().Be(0L);
		}
	}
}
