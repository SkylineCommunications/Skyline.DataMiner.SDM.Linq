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
	public class DoubleCollectionTests
	{
		[TestMethod]
		public void Where_Collection_Any_Double_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size == 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size == 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.Equal(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Collection_Any_Double_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size != 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size != 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.NotEqual(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Collection_Any_Double_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size > 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size > 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.GreaterThan(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Collection_Any_Double_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size >= 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size >= 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.GreaterThanOrEqual(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Collection_Any_Double_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size < 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size < 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.LessThan(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Collection_Any_Double_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.SubClasses.Any(t => t.Size <= 30))
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.SubClasses.Should().Contain(s => s.Size <= 30));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.SubClasses.Size.LessThanOrEqual(30).ToQuery().ToCompleteString());
		}
	}
}
