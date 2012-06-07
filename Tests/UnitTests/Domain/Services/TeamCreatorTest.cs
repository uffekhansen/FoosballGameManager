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
		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(3, 10)]
		public void Given_TeamCreatorBase_with_fewer_players_than_players_per_team_When_creating_teams_Then_teamgenerationexception_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);
			var teamCreator = new TeamCreatorBase(playersPerTeam, playerList);

			Assert.ThrowsDelegate act = () => teamCreator.CreateTeams();

			Assert.Throws<TeamGenerationException>(act);
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(5, 6)]
		[InlineData(11, 5)]
		[InlineData(101, 10)]
		public void Given_TeamCreatorBase_with_players_not_divisable_with_number_players_per_teams_When_creating_team_Then_teamgenerationException_is_thrown(int numberPlayersInlist, int playersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInlist);
			var teamCreator = new TeamCreatorBase(playersPerTeam, playerList);

			Assert.ThrowsDelegate act = () => teamCreator.CreateTeams();

			Assert.Throws<TeamGenerationException>(act);
		}

		[Fact]
		public void Given_TeamCreatorBase_with_zero_players_When_creating_teams_Then_teamgenerationException_is_thrown()
		{
			const int any = 2;
			var playerList = ArrangePlayerList(0);
			var teamCreator = new TeamCreatorBase(any, playerList);

			Assert.ThrowsDelegate act = () => teamCreator.CreateTeams();

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
		public TeamCreatorBase(int playersPerTeam, List<Player> players)
			: base(playersPerTeam, players)
		{
		}

		protected override List<Team> GenerateTeams()
		{
			throw new System.NotImplementedException();
		}
	}
}
