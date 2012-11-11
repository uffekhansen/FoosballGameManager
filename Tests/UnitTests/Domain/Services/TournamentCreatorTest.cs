using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Services;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.Domain.Services
{
	public class TournamentCreatorTest
	{
		private readonly TournamentCreator _tournamentCreator = new TournamentCreator();

		[Fact]
		public void Given_No_Teams_When_CreateTournament_Then_TournamentCreationException_Is_Thrown()
		{
			_tournamentCreator.Teams = ArrangeTeams(0);
			Action createTournament = () => _tournamentCreator.CreateTournament();

			createTournament.ShouldThrow<TournamentCreationException>().WithMessage("No teams to create tournament from");
		}

		[Fact]
		public void Given_A_Single_Team_When_CreateTournament_Then_TournamentCreationException_Is_Thrown()
		{
			_tournamentCreator.Teams = ArrangeTeams(1);
			Action createTournament = () => _tournamentCreator.CreateTournament();

			createTournament.ShouldThrow<TournamentCreationException>().WithMessage("Only 1 team supplied - need at least 2 to create a tournament");
		}

		[Theory]
		[InlineData(2)]
		[InlineData(5)]
		[InlineData(10)]
		public void Given_Teams_When_CreateTournament_Then_Tournament_Created_Contains_Teams(int numberTeams)
		{
			var arrangedTeams = ArrangeTeams(numberTeams);
			_tournamentCreator.Teams = arrangedTeams;

			var tournament = _tournamentCreator.CreateTournament();

			tournament.Teams.Should().Contain(arrangedTeams);
		}

		private IEnumerable<Team> ArrangeTeams(int numberTeams)
		{
			var teams = new List<Team>();
			numberTeams.TimesDo(() => teams.Add(ArrangeTeam()));
			return teams;
		}

		private Team ArrangeTeam()
		{
			return new Team
			(
				new List<Player>
				{
					new Player(),
					new Player(),
				}
			);
		}
	}
}
