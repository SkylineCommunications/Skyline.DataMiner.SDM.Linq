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
	public class StringSimpleTests
	{
		[TestMethod]
		public void Where_String_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name == "Alice")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Name.Should().Be("Alice"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Equal("Alice").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name != "Alice")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Name.Should().NotBe("Alice"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.NotEqual("Alice").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_Contains()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Name.Contains("ice"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Name.Should().Contain("ice"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Contains("ice").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotContains()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => !x.Name.Contains("ice"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Name.Should().NotContain("ice"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Name.Contains("ice")).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_StartsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => x.Name.StartsWith("A"))
				.ToArray();

			// Assert

			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Name.Should().StartWith("A"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Matches("^A").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotStartsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => !x.Name.StartsWith("A"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Name.Should().NotStartWith("A"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Name.Matches("^A")).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_EndsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => x.Name.EndsWith("b"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Name.Should().EndWith("b"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Name.Matches("b$").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotEndsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => !x.Name.EndsWith("b"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Name.Should().NotEndWith("b"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Name.Matches("b$")).ToQuery().ToCompleteString());
		}
	}
}
