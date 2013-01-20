using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Services;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Tests.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class TournamentControllerTest
	{
		private readonly IGetEntityByIdQuery<Tournament> _getTournamentByIdQuery = Substitute.For<IGetEntityByIdQuery<Tournament>>();
		private readonly TournamentController _tournamentController;

		public TournamentControllerTest()
		{
			_tournamentController = new TournamentController(_getTournamentByIdQuery);
		}

		[Fact]
		public void Given_Id_When_Show_Then_GetTournamentQuery_Is_Executed_With_Id()
		{
			var id = Guid.NewGuid();

			_tournamentController.Show(id);

			_getTournamentByIdQuery.Received(1).Execute(id);
		}

		[Fact]
		public void Given_Id_When_Show_Then_Model_Type_Is_Tournament()
		{
			var id = Guid.NewGuid();

			var viewResult = _tournamentController.Show(id) as ViewResult;

			viewResult.Model.Should().BeOfType<TournamentViewModel>();
		}

		[Fact]
		public void Given_Id_When_Show_Then_View_Name_Is_Show()
		{
			var viewResult = _tournamentController.Show(Guid.NewGuid()) as ViewResult;

			viewResult.ViewName.Should().Be("");
		}

		[Theory]
		[InlineData(2)]
		[InlineData(5)]
		[InlineData(10)]
		public void Given_GetTournamentQuery_Returns_Tournament_When_Show_Then_Model_Contains_Teams_Found_In_Tournament(int numberTeams)
		{
			var tournament = ArrangeTournament(numberTeams);
			_getTournamentByIdQuery.Execute(Arg.Any<Guid>()).Returns(tournament);

			var viewResult = _tournamentController.Show(Guid.NewGuid()) as ViewResult;
			
			viewResult.Model.As<TournamentViewModel>().Tournament.Teams.Should().ContainExactlyInOrder(tournament.Teams);
		}

        //[Fact]
        //public void Given_GetTournamentQuery_Throws_NotFoundException_When_Show_Then_Model_Contains_Exception_Message()
        //{
        //    const string exceptionMessage = "unit test";
        //    _getTournamentByIdQuery.When(x => x.Execute(Arg.Any<Guid>())).Do(x => { throw new NotFoundException(exceptionMessage); });

        //    Action show = () => _tournamentController.Show(Guid.NewGuid());

        //    show.ShouldThrow<NotFoundException>().WithMessage(exceptionMessage);
        //}

		private Tournament ArrangeTournament(int numberTeams)
		{
			return new Tournament
			{
				Teams = ArrangeTeams(numberTeams),
			};
		}

		private IEnumerable<Team> ArrangeTeams(int numberTeams)
		{
			var teams = new List<Team>();
			numberTeams.TimesDo(() => teams.Add(ArrangeTeam()));
			return teams;
		}

		private Team ArrangeTeam()
		{
			return new Team(new List<Player>());
		}
	}
}
