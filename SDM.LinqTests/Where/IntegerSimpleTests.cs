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
	public class IntegerSimpleTests
	{
		[TestMethod]
		public void Where_Int_Equal()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age == 25)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 25 == x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().Be(25));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().Be(25));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.Equal(25).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.Equal(25).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_Equals()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age.Equals(25))
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 25.Equals(x.Age))
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().Be(25));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().Be(25));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.Equal(25).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.Equal(25).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_NotEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age != 25)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 25 != x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().NotBe(25));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().NotBe(25));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.NotEqual(25).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.NotEqual(25).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_NotEquals()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => !x.Age.Equals(25))
				.ToArray();
			var result2 = repository.Query()
				.Where(x => !25.Equals(x.Age))
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().NotBe(25));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().NotBe(25));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Age.Equal(25)).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(new NOTFilterElement<TestClass>(TestClassExposers.Age.Equal(25)).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_LessThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age < 30)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 30 >= x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().BeLessThan(30));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().BeLessThan(30));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.LessThan(30).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.LessThan(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_LessThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age <= 30)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 30 > x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().BeLessThanOrEqualTo(30));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().BeLessThanOrEqualTo(30));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.LessThanOrEqual(30).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.LessThanOrEqual(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_GreaterThan()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age > 30)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 30 <= x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().BeGreaterThan(30));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().BeGreaterThan(30));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(30).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThan(30).ToQuery().ToCompleteString());
		}

		[TestMethod]
		public void Where_Int_GreaterThanOrEqual()
		{
			// Arrange
			var (repository, tracker) = Mocked.CreateRepository();

			// Act
			var result1 = repository.Query()
				.Where(x => x.Age >= 30)
				.ToArray();
			var result2 = repository.Query()
				.Where(x => 30 < x.Age)
				.ToArray();

			// Assert
			result1.Should().NotBeNull();
			result1.Should().AllSatisfy(t => t.Age.Should().BeGreaterThanOrEqualTo(30));
			result2.Should().NotBeNull();
			result2.Should().AllSatisfy(t => t.Age.Should().BeGreaterThanOrEqualTo(30));

			tracker.Queries.Should().HaveCount(2);
			tracker.Queries[0].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThanOrEqual(30).ToQuery().ToCompleteString());
			tracker.Queries[1].ToCompleteString().Should().Be(TestClassExposers.Age.GreaterThanOrEqual(30).ToQuery().ToCompleteString());
		}
	}
}
