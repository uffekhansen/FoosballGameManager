using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using FluentAssertions;
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
		public void Given_TeamCreatorBase_With_Fewer_Players_Than_Players_Per_Team_When_Creating_Teams_Then_TeamGenerationException_Is_Thrown(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInList, numberPlayersPerTeam);

			Action createTeams = () => teamCreator.CreateTeams();

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Not enough players for a single team");
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(10, 6)]
		[InlineData(11, 5)]
		[InlineData(101, 10)]
		public void Given_TeamCreatorBase_With_Players_Not_Divisable_With_Number_Players_Per_Teams_When_Creating_Team_Then_TeamGenerationException_Is_Thrown(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInList, numberPlayersPerTeam);

			Action createTeams = () => teamCreator.CreateTeams();

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Number players not divisable with number players per team in team settings");
		}

		[Fact]
		public void Given_TeamCreatorBase_With_Zero_Players_When_Creating_Teams_Then_TeamGenerationException_Is_Thrown()
		{
			var teamCreator = ArrangeTeamCreator(0, 0);

			Action createTeams = () => teamCreator.CreateTeams();

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Zero players in list");
		}

		private TeamCreatorBase ArrangeTeamCreator(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);
			return new TeamCreatorBase
			{
				Players = playerList,
				PlayersPerTeam = numberPlayersPerTeam,
			};;
		}

		private IEnumerable<Player> ArrangePlayerList(int numberPlayers)
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
		protected override List<Team> GenerateTeams()
		{
			throw new NotImplementedException();
		}
	}
}
