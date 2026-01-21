namespace SDM.LinqTests.All
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;

	[TestClass]
	public class AllTests
	{
		[TestMethod]
		public void All_AllElementsMatch_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.All(x => x.Age >= 0);

			// Assert
			result.Should().BeTrue();
		}

		[TestMethod]
		public void All_NotAllElementsMatch_ReturnsFalse()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.All(x => x.Age > 25);

			// Assert
			result.Should().BeFalse();
		}

		[TestMethod]
		public void All_WithWhere_AllMatch_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20)
				.All(x => x.Age > 15);

			// Assert
			result.Should().BeTrue();
		}

		[TestMethod]
		public void All_WithWhere_NotAllMatch_ReturnsFalse()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 10)
				.All(x => x.Age > 50);

			// Assert
			result.Should().BeFalse();
		}

		[TestMethod]
		public void All_EmptySequence_ReturnsTrue()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 1000)
				.All(x => x.Age > 0);

			// Assert
			result.Should().BeTrue(); // Empty sequence should return true for All
		}

		[TestMethod]
		public void All_StringComparison_ReturnsExpectedResult()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.All(x => x.Name != null);

			// Assert
			// This should be false if any TestClass has null Name, otherwise true
			result.Should().BeTrue();
		}
	}
}
