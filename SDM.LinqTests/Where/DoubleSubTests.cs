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
	public class DoubleSubTests
	{
		[TestMethod]
		public void Where_Double_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size == 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().Be(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.Equal(20).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Double_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size != 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().NotBe(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.NotEqual(20).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Double_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size > 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().BeGreaterThan(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.GreaterThan(20).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Double_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size >= 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().BeGreaterOrEqualTo(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.GreaterThanOrEqual(20).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Double_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size < 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().BeLessThan(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.LessThan(20).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Double_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Size <= 20)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Size.Should().BeLessOrEqualTo(20));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Size.LessThanOrEqual(20).ToQuery().ToCompleteString());
		}
	}
}
