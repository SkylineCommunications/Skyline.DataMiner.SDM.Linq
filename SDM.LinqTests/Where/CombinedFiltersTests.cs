namespace SDM.LinqTests.Where
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;
	using SDM.LinqTests.Shared;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	[TestClass]
	public class CombinedFiltersTests
	{
		[TestMethod]
		public void Where_Or_TwoConditions_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice" || x.Name == "Bob")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Name == "Alice" || x.Name == "Bob");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Name.Equal("Alice"),
					TestClassExposers.Name.Equal("Bob"))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_IntegerComparison_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age < 26 || x.Age > 34)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Age < 26 || x.Age > 34);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Age.LessThan(26),
					TestClassExposers.Age.GreaterThan(34))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_DifferentProperties_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 30 || x.IsActive)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(30),
					TestClassExposers.IsActive.Equal(true))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithEnum_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Status == Status.Active || x.Status == Status.Draft)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Status == Status.Active || x.Status == Status.Draft);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Status.Equal(Status.Active),
					TestClassExposers.Status.Equal(Status.Draft))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_And_TwoConditions_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20 && x.IsActive)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().OnlyContain(x => x.Age > 20 && x.IsActive);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ANDFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(20),
					TestClassExposers.IsActive.Equal(true))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_ComplexOrAndCombination_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => (x.Age > 30 && x.IsActive) || x.Name == "Diana")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => (x.Age > 30 && x.IsActive) || x.Name == "Diana");

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					new ANDFilterElement<TestClass>(
						TestClassExposers.Age.GreaterThan(30),
						TestClassExposers.IsActive.Equal(true)),
					TestClassExposers.Name.Equal("Diana"))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_ThreeConditions_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age == 25 || x.Age == 30 || x.Age == 35)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Age == 25 || x.Age == 30 || x.Age == 35);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					new ORFilterElement<TestClass>(
						TestClassExposers.Age.Equal(25),
						TestClassExposers.Age.Equal(30)),
					TestClassExposers.Age.Equal(35))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithStringContains_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name.Contains("ice") || x.Name.Contains("ob"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Name.Contains("ice") || x.Name.Contains("ob"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Name.Contains("ice"),
					TestClassExposers.Name.Contains("ob"))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithDateTime_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var date1 = new DateTime(2020, 1, 1);
			var date2 = new DateTime(2019, 6, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt == date1 || x.CreatedAt == date2)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.CreatedAt == date1 || x.CreatedAt == date2);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.CreatedAt.Equal(date1),
					TestClassExposers.CreatedAt.Equal(date2))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithDecimal_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score < 90.0m || x.Score > 95.0m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Score < 90.0m || x.Score > 95.0m);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Score.LessThan(90.0m),
					TestClassExposers.Score.GreaterThan(95.0m))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithNestedCollection_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size == 5) || x.SubClasses.Any(t => t.Size == 25))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.SubClasses.Any(t => t.Size == 5) || x.SubClasses.Any(t => t.Size == 25));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.SubClasses.Size.Equal(5),
					TestClassExposers.SubClasses.Size.Equal(25))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_CombinedWithTopLevelAndNestedFilters_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age < 26 || x.SubClasses.Any(t => t.Size > 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();
			result.Should().OnlyContain(x => x.Age < 26 || x.SubClasses.Any(t => t.Size > 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Age.LessThan(26),
					TestClassExposers.SubClasses.Size.GreaterThan(30))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_MultipleWhere_ActsAsAnd_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 20)
				.Where(x => x.IsActive)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().OnlyContain(x => x.Age > 20 && x.IsActive);

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ANDFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(20),
					TestClassExposers.IsActive.Equal(true))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_AllConditionsFalse_ReturnsEmpty()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Age > 1000 || x.Age < 0)
				.ToArray();

			// Assert
			result.Should().BeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.Age.GreaterThan(1000),
					TestClassExposers.Age.LessThan(0))
				.ToQuery()
				.ToCompleteString());
		}

		[TestMethod]
		public void Where_Or_WithBoolean_ReturnsMatchingElements()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive || x.Age > 33)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().NotBeEmpty();

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(
				new ORFilterElement<TestClass>(
					TestClassExposers.IsActive.Equal(true),
					TestClassExposers.Age.GreaterThan(33))
				.ToQuery()
				.ToCompleteString());
		}
	}
}
