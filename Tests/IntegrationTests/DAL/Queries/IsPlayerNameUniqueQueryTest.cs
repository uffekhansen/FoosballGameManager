using DAL.Queries;
using Domain.Extensions;
using FluentAssertions;
using Tests.Builders;
using Tests.Infrastructure.TestBases;
using Xunit;
using Xunit.Extensions;

namespace Tests.IntegrationTests.DAL.Queries
{
	public class IsPlayerNameUniqueQueryTest : InDatabaseTest
	{
		private readonly IsPlayerNameUniqueQuery _isPlayerNameUniqueQuery;

		public IsPlayerNameUniqueQueryTest()
		{
			_isPlayerNameUniqueQuery = new IsPlayerNameUniqueQuery(_session);
		}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //[InlineData(2)]
		public void Given_No_Player_With_The_Same_Name_When_Execute_Then_True_Is_Returned(int numberPlayers)
		{
			int nameCounter = 0;
			const string namePrefix = "foo";
			numberPlayers.TimesDo(() => ArrangePlayer(namePrefix + (nameCounter++).ToString()));
			_persister.Persist();

			var isPlayerNameUnique = _isPlayerNameUniqueQuery.Execute("bar");

			isPlayerNameUnique.Should().BeTrue();
		}

        //[Fact]
		public void Given_A_Single_Player_With_The_Same_Name_When_Execute_Then_False_Is_Returned()
		{
			const string sharedName = "foo";
			ArrangePlayer(sharedName);
			_persister.Persist();

			var isPlayerNameUnique = _isPlayerNameUniqueQuery.Execute(sharedName);

			isPlayerNameUnique.Should().BeFalse();
		}

        //[Fact]
		public void Given_A_Single_Player_With_The_Same_Name_And_A_Player_With_Another_Name_When_Execute_Then_False_Is_Returned()
		{
			const string sharedName = "foo";
			ArrangePlayer(sharedName);
			ArrangePlayer("not a shared name");
			_persister.Persist();

			var isPlayerNameUnique = _isPlayerNameUniqueQuery.Execute(sharedName);

			isPlayerNameUnique.Should().BeFalse();
		}

		private void ArrangePlayer(string name)
		{
			new PlayerBuilder(_persister)
			{
				Name = name,
			}.Build();
		}
	}
}
