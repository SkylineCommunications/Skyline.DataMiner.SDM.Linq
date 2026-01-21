namespace SDM.LinqTests.Where
{
	using System.Linq;

	using FluentAssertions;

	using SDM.LinqTests.Mock;

	using Skyline.DataMiner.Net.Messages.SLDataGateway;
	using Skyline.DataMiner.SDM;

	using SLDataGateway.API.Querying;

	using SDM.LinqTests.Shared;

	[TestClass]
	public class BooleanSimpleTests
	{
		[TestMethod]
		public void Where_Boolean_Equal_True()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive == true)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeTrue());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.IsActive.Equal(true).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Boolean_Equal_False()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive == false)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeFalse());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.IsActive.Equal(false).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Boolean_Direct()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeTrue());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.IsActive.Equal(true).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Boolean_Negated()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => !x.IsActive)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeFalse());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.IsActive.Equal(true)).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Boolean_NotEqual_True()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive != true)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeFalse());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.IsActive.NotEqual(true).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Boolean_NotEqual_False()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result = repository.Query()
				.Where(x => x.IsActive != false)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.IsActive.Should().BeTrue());

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.IsActive.NotEqual(false).ToQuery().ToCompleteString());
		}
	}
}
