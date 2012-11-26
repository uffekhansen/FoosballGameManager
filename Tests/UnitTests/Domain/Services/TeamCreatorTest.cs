using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Services;
using Domain.Tools;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public abstract class TeamCreatorTest<T> where T : ITeamCreator
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

		[Theory]
		[InlineData(1, 1, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(2, 2, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(5, 5, 1)]
		[InlineData(3, 6, 2)]
		public void Given_TeamCreator_When_Creating_Teams_Then_The_Expected_Number_Of_Teams_Are_Returned(int expectedNumberOfTeams, int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreator.CreateTeams();

			Assert.Equal(expectedNumberOfTeams, teams.Count);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(2, 1)]
		[InlineData(5, 1)]
		[InlineData(6, 2)]
		[InlineData(6, 3)]
		public void Given_RandomTeamCreator_When_Creating_Teams_Then_Generated_Teams_Contains_Expected_Number_Of_Players(int numberPlayersInlist, int numberPlayersPerTeam)
		{
			var teamCreator = ArrangeTeamCreator(numberPlayersInlist, numberPlayersPerTeam);

			var teams = teamCreator.CreateTeams();

			teams.Each(x => Assert.Equal(numberPlayersPerTeam, x.Players.Count()));
		}

		private ITeamCreator ArrangeTeamCreator(int numberPlayersInList, int numberPlayersPerTeam)
		{
			var playerList = ArrangePlayerList(numberPlayersInList);

			var teamCreator = CreateTeamCreator();
			teamCreator.Players = playerList;
			teamCreator.PlayersPerTeam = numberPlayersPerTeam;

			return teamCreator;
		}

		protected abstract T CreateTeamCreator();

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
		protected override IList<Team> GenerateTeams()
		{
			throw new NotImplementedException();
		}
	}

	public class RandomTeamCreatorBaseTest : TeamCreatorTest<RandomTeamCreator>
	{
		protected override RandomTeamCreator CreateTeamCreator()
		{
			return new RandomTeamCreator(Substitute.For<IRandom>());
		}
	}

	public class GroupedAffiliationTeamCreatorBaseTest : TeamCreatorTest<GroupedAffiliationTeamCreator>
	{
		protected override GroupedAffiliationTeamCreator CreateTeamCreator()
		{
			return new GroupedAffiliationTeamCreator(Substitute.For<IRandom>());
		}
	}
}
