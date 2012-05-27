using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public class TeamCreatorTest
	{
		private readonly TeamCreator _teamCreator;

		public TeamCreatorTest()
		{
			_teamCreator = new TeamCreatorBase();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(3, 10)]
		public void Given_fewer_players_than_players_per_team_When_creating_team_Then_teamgenerationexception_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(5, 6)]
		[InlineData(11, 5)]
		[InlineData(101, 10)]
		public void Given_players_not_divisable_with_number_players_per_team_When_creating_team_Then_teamgenerationException_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam);

			Assert.Throws<TeamGenerationException>(act);
		}

		[Fact]
		public void Given_zero_players_When_creating_team_Then_teamgenerationException_is_thrown()
		{
			const int any = 2;
			var playerList = ArrangePlayerList(0);

			Assert.ThrowsDelegate act = () => _teamCreator.CreateTeams(playerList, playersPerTeam: any);

			Assert.Throws<TeamGenerationException>(act);
		}

		private List<Player> ArrangePlayerList(int numberPlayers)
		{
			return Enumerable.Repeat(ArrangePlayer(), numberPlayers)
				.ToList();
		}

		private Player ArrangePlayer()
		{
			return new Player();
		}
	}

	public class TeamCreatorBase : TeamCreator
	{
		protected override List<Team> GenerateTeams(List<Player> players, int playersPerTeam)
		{
			throw new System.NotImplementedException();
		}
	}
}
