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
	public class GuidSubTests
	{
		[TestMethod]
		public void Where_Guid_Equal()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var testGuid = dummyData.First().Sub.Guid;
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Guid == testGuid)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Guid.Should().Be(testGuid));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Guid.Equal(testGuid).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Guid_NotEqual()
		{
			// Arrange
			var dummyData = DummyData.GetDummyData();
			var testGuid = dummyData.First().Sub.Guid;
			var (repository, tracker) = Mocked.CreateRepository(dummyData);

			// Act
			var result = repository.Query()
				.Where(x => x.Sub.Guid != testGuid)
				.ToArray();

			// Assert
			result.Should().NotBeNull();
			result.Should().AllSatisfy(t => t.Sub.Guid.Should().NotBe(testGuid));

			tracker.Queries.Should().HaveCount(1);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Sub.Guid.NotEqual(testGuid).ToQuery().ToCompleteString());
		}
	}
}
