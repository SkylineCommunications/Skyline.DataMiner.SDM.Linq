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
	public class FloatSimpleTests
	{
		[TestMethod]
		public void Where_Float_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating == 4.5f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().Be(4.5f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.Equal(4.5f).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Float_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating != 4.5f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().NotBe(4.5f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.NotEqual(4.5f).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Float_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating > 3.0f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().BeGreaterThan(3.0f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.GreaterThan(3.0f).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Float_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating >= 3.0f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().BeGreaterOrEqualTo(3.0f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.GreaterThanOrEqual(3.0f).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Float_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating < 5.0f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().BeLessThan(5.0f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.LessThan(5.0f).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Float_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.Rating <= 5.0f)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Rating.Should().BeLessOrEqualTo(5.0f));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Rating.LessThanOrEqual(5.0f).ToQuery().ToCompleteString());
		}
	}
}
