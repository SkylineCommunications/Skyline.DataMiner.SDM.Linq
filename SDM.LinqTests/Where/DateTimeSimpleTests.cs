namespace SDM.LinqTests.Where
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
	public class DateTimeSimpleTests
	{
		[TestMethod]
		public void Where_DateTime_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2021, 11, 5);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt == testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().Be(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.Equal(testDate).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_DateTime_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2025, 1, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt != testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().NotBe(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.NotEqual(testDate).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_DateTime_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2019, 6, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt > testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().BeAfter(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.GreaterThan(testDate).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_DateTime_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2019, 6, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt >= testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().BeOnOrAfter(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.GreaterThanOrEqual(testDate).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_DateTime_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2019, 6, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt < testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().BeBefore(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.LessThan(testDate).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_DateTime_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testDate = new DateTime(2019, 6, 15);

			// Act
			var result = repository.Query()
				.Where(x => x.CreatedAt <= testDate)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.CreatedAt.Should().BeOnOrBefore(testDate));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.CreatedAt.LessThanOrEqual(testDate).ToQuery().ToCompleteString());
		}
	}
}
