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
	public class DecimalSimpleTests
	{
		[TestMethod]
		public void Where_Decimal_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score == 95.5m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().Be(95.5m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.Equal(95.5m).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Decimal_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score != 95.5m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().NotBe(95.5m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.NotEqual(95.5m).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Decimal_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score > 90.0m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().BeGreaterThan(90.0m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.GreaterThan(90.0m).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Decimal_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score >= 90.0m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().BeGreaterOrEqualTo(90.0m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.GreaterThanOrEqual(90.0m).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Decimal_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score < 100.0m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().BeLessThan(100.0m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.LessThan(100.0m).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Decimal_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Score <= 100.0m)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Score.Should().BeLessOrEqualTo(100.0m));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Score.LessThanOrEqual(100.0m).ToQuery().ToCompleteString());
		}
	}
}
