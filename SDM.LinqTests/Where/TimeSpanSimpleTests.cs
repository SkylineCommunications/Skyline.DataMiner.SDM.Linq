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
	public class TimeSpanSimpleTests
	{
		[TestMethod]
		public void Where_TimeSpan_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(2);

			// Act
			var result = repository.Query()
				.Where(x => x.Range == testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().Be(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.Equal(testTimeSpan).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_TimeSpan_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(5);

			// Act
			var result = repository.Query()
				.Where(x => x.Range != testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().NotBe(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.NotEqual(testTimeSpan).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_TimeSpan_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(2);

			// Act
			var result = repository.Query()
				.Where(x => x.Range > testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().BeGreaterThan(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.GreaterThan(testTimeSpan).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_TimeSpan_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(3);

			// Act
			var result = repository.Query()
				.Where(x => x.Range >= testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().BeGreaterOrEqualTo(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.GreaterThanOrEqual(testTimeSpan).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_TimeSpan_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(10);

			// Act
			var result = repository.Query()
				.Where(x => x.Range < testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().BeLessThan(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.LessThan(testTimeSpan).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_TimeSpan_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			var testTimeSpan = TimeSpan.FromHours(10);

			// Act
			var result = repository.Query()
				.Where(x => x.Range <= testTimeSpan)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Range.Should().BeLessOrEqualTo(testTimeSpan));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Range.LessThanOrEqual(testTimeSpan).ToQuery().ToCompleteString());
		}
	}
}
