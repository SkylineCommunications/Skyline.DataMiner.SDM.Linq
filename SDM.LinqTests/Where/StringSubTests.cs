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
	public class StringSubTests
	{
		[TestMethod]
		public void Where_String_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Description == "Third")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Description.Should().Be("Third"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Description.Equal("Third").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Description != "Third")
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Description.Should().NotBe("Third"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Description.NotEqual("Third").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_Contains()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Description.Contains("i"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Description.Should().Contain("i"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Description.Contains("i").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotContains()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => !x.Sub.Description.Contains("i"))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Description.Should().NotContain("i"));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Sub.Description.Contains("i")).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_StartsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => x.Sub.Description.StartsWith("T"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Sub.Description.Should().StartWith("T"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Description.Matches("^T").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotStartsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => !x.Sub.Description.StartsWith("T"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Sub.Description.Should().NotStartWith("T"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Sub.Description.Matches("^T")).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_EndsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => x.Sub.Description.EndsWith("d"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Sub.Description.Should().EndWith("d"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Description.Matches("d$").ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_String_NotEndsWith()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = Array.Empty<TestClass>();
			var act = () => result = repository.Query()
				.Where(x => !x.Sub.Description.EndsWith("d"))
				.ToArray();

			// Assert
			act.Should().Throw<NotSupportedException>();

			// The below should work once platform allows for regex filtering.

			////result.Should().NotBeNull();
			////result.Should().AllSatisfy(t => t.Sub.Description.Should().NotEndWith("d"));

			////tracker.Queries.Should().HaveCount(1);
			////tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Sub.Description.Matches("d$")).ToQuery().ToCompleteString());
		}
	}
}
