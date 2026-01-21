namespace SDM.LinqTests.Single
{
	using System;
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class SingleTests
	{
		[TestMethod]
		public void Single_OneMatchingElement_ReturnsElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Single(x => x.Name == "Alice");

			// Assert
			result.Should().NotBeNull();
			result.Name.Should().Be("Alice");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice")
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void Single_WithWhere_ReturnsMatchingElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice")
				.Single();

			// Assert
			result.Should().NotBeNull();
			result.Name.Should().Be("Alice");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice")
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void Single_EmptyResult_ThrowsException()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act & Assert
			Action act = () => repository.Query()
				.Single(x => x.Age > 1000);

			act.Should().Throw<InvalidOperationException>();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void Single_MultipleMatchingElements_ThrowsException()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act & Assert
			Action act = () => repository.Query()
				.Single(x => x.Age > 20);

			act.Should().Throw<InvalidOperationException>();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(20)
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void SingleOrDefault_OneMatchingElement_ReturnsElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.SingleOrDefault(x => x.Name == "Alice");

			// Assert
			result.Should().NotBeNull();
			result.Name.Should().Be("Alice");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice")
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void SingleOrDefault_EmptyResult_ReturnsNull()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.SingleOrDefault(x => x.Age > 1000);

			// Assert
			result.Should().BeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(2)
				.ToCompleteString());
		}

		[TestMethod]
		public void SingleOrDefault_MultipleMatchingElements_ThrowsException()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act & Assert
			Action act = () => repository.Query()
				.SingleOrDefault(x => x.Age > 20);

			act.Should().Throw<InvalidOperationException>();
		}
	}
}
