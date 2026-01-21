namespace SDM.LinqTests.First
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
	public class FirstTests
	{
		[TestMethod]
		public void First_NoFilter_ReturnsFirstElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.First();

			// Assert
			result.Should().NotBeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void First_WithFilter_ReturnsMatchingElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.First(x => x.Age > 20);

			// Assert
			result.Should().NotBeNull();
			result.Age.Should().BeGreaterThan(20);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(20)
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void First_WithWhere_ReturnsMatchingElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice")
				.First();

			// Assert
			result.Should().NotBeNull();
			result.Name.Should().Be("Alice");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice")
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void First_EmptyResult_ThrowsException()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act & Assert
			Action act = () => repository.Query()
				.Where(x => x.Age > 1000)
				.First();

			act.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void FirstOrDefault_NoFilter_ReturnsFirstElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.FirstOrDefault();

			// Assert
			result.Should().NotBeNull();
			tracker.Queries.Should().HaveCount(1);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new TRUEFilterElement<TestClass>()
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void FirstOrDefault_WithFilter_ReturnsMatchingElement()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.FirstOrDefault(x => x.Age > 20);

			// Assert
			result.Should().NotBeNull();
			result.Age.Should().BeGreaterThan(20);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(20)
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}

		[TestMethod]
		public void FirstOrDefault_EmptyResult_ReturnsNull()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 1000)
				.FirstOrDefault();

			// Assert
			result.Should().BeNull();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(1000)
				.ToQuery()
				.Limit(1)
				.ToCompleteString());
		}
	}
}
