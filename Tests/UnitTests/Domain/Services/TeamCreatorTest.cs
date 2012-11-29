using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Services;
using Domain.Strategies;
using FluentAssertions;
using NSubstitute;
using Tests.Builders;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public class TeamCreatorTest
	{
		private readonly ITeamCreationStrategyFactory _teamCreationStrategyFactory;

		public TeamCreatorTest()
		{
			_teamCreationStrategyFactory = Substitute.For<ITeamCreationStrategyFactory>();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(3, 10)]
		public void Given_TeamCreatorBase_With_Fewer_Players_Than_Players_Per_Team_When_Creating_Teams_Then_TeamGenerationException_Is_Thrown(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInList, numberPlayersPerTeam);

			Action createTeams = () => teamCreator.CreateTeams(Arg.Any<TeamGenerationMethod>());

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

			Action createTeams = () => teamCreator.CreateTeams(Arg.Any<TeamGenerationMethod>());

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Number players not divisable with number players per team in team settings");
		}

		[Fact]
		public void Given_TeamCreatorBase_With_Zero_Players_When_Creating_Teams_Then_TeamGenerationException_Is_Thrown()
		{
			var teamCreator = ArrangeTeamCreator(0, 0);

			Action createTeams = () => teamCreator.CreateTeams(Arg.Any<TeamGenerationMethod>());

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Zero players in list");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(4)]
		[InlineData(6)]
		[InlineData(10)]
		public void Given_TeamGeneratioMethod_Returns_Wrong_Number_Of_Teams_When_Creating_Teams_Then_TeamGenerationException_Is_Thrown(int numberTeamsReturned)
		{
			var teamCreator = ArrangeTeamCreator(10, 2);
			var teamCreationStrategy = Substitute.For<ITeamCreationStrategy>();
			var teams = new TeamBuilder().Build(numberTeamsReturned);
			teamCreationStrategy.CreateTeams().Returns(teams);
			_teamCreationStrategyFactory.MakeTeamCreationStrategy(Arg.Any<TeamGenerationMethod>()).Returns(teamCreationStrategy);

			Action createTeams = () => teamCreator.CreateTeams(Arg.Any<TeamGenerationMethod>());

			createTeams.ShouldThrow<TeamGenerationException>().WithMessage("Generated number of teams did not match team settings and player count");
		}

		private TeamCreator ArrangeTeamCreator(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);

			var teamCreator = new TeamCreator(_teamCreationStrategyFactory)
			{
				Players = playerList,
				PlayersPerTeam = numberPlayersPerTeam
			};

			return teamCreator;
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
}
