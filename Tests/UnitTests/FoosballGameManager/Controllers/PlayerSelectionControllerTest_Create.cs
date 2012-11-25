using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Commands;
using DAL.Queries;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Services;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Xunit;
using Xunit.Extensions;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class PlayerSelectionControllerTest_Create
	{
		private readonly PlayerSelectionController _playerSelectionController;
		private readonly ITeamCreatorFactory _teamCreatorFactory = Substitute.For<ITeamCreatorFactory>();
		private readonly ITournamentCreator _tournamentCreator = Substitute.For<ITournamentCreator>();
		private readonly IGetPlayersByIdsQuery _getPlayersByIdsQuery;
		private readonly IAddCommand<Tournament> _addTournamentCommand;

		public PlayerSelectionControllerTest_Create()
		{
			_getPlayersByIdsQuery = Substitute.For<IGetPlayersByIdsQuery>();
			_addTournamentCommand = Substitute.For<IAddCommand<Tournament>>();
			_tournamentCreator.CreateTournament(Arg.Any<IEnumerable<Team>>()).Returns(new Tournament());
			_playerSelectionController = new PlayerSelectionController(Substitute.For<IGetEveryEntityQuery<Player>>(), _getPlayersByIdsQuery, _addTournamentCommand, _teamCreatorFactory, _tournamentCreator);
		}

		[Fact]
		public void Given_TeamCreator_Is_Returned_By_Team_Creator_Factory_When_Create_Then_CreateTeams_Is_Called_On_TeamCreator()
		{
			var teamCreator = Substitute.For<ITeamCreator>();
			_teamCreatorFactory.CreateTeamCreator(Arg.Any<TeamGenerationMethod>()).Returns(teamCreator);

			_playerSelectionController.Create(new PlayersViewModel());

			teamCreator.Received(1).CreateTeams();
		}

		[Fact]
		public void Given_PlayersViewModel_When_Create_Then_Result_Is_Redirected_To_Show_Action_On_TournamentController_With_Tournament_Id()
		{
			var tournament = new Tournament();
			_tournamentCreator.CreateTournament(Arg.Any<IEnumerable<Team>>()).Returns(tournament);

			var redirectToRouteResult = _playerSelectionController.Create(new PlayersViewModel()) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Controller"].Should().Be("Tournament");
			redirectToRouteResult.RouteValues["Action"].Should().Be("Show");
			redirectToRouteResult.RouteValues["Id"].Should().Be(tournament.Id);
		}

		[Fact]
		public void Given_GetPlayersByIdsQuery_Throws_NotFoundException_When_Create_Then_ModelState_Contains_Exception_Message()
		{
			const string exceptionMessage = "unit test";
			_getPlayersByIdsQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException(exceptionMessage); });

			var viewResult = _playerSelectionController.Create(new PlayersViewModel()) as ViewResult;

			viewResult.ViewData.ModelState["_FORM"].Errors.Single().ErrorMessage.Should().Be(exceptionMessage);
		}

		[Fact]
		public void Given_GetPlayersByIdsQuery_Throws_NotFoundException_When_Create_Then_Result_Is_Redirected_To_Index_Action_On_PlayerSelectionController()
		{
			_getPlayersByIdsQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException("unit test"); });

			var viewResult = _playerSelectionController.Create(new PlayersViewModel()) as ViewResult;

			viewResult.ViewName.Should().Be("Index");
		}

		[Fact]
		public void Given_TeamCreator_And_View_Model_With_Player_Identifier_List_When_Create_Then_TeamCreator_Is_Executed_With_Player_Identifier_List()
		{
			var identifierList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
			var viewModel = new PlayersViewModel
			{
				SelectedPlayers = identifierList,
			};

			_playerSelectionController.Create(viewModel);

			_getPlayersByIdsQuery.Received(1).Execute(identifierList);
		}

		[Fact]
		public void Given_Teams_When_Create_Then_CreateTournament_Is_Called_With_Teams()
		{
			var teams = ArrangeTwoTeams();
			var teamCreator = Substitute.For<ITeamCreator>();
			teamCreator.CreateTeams().Returns(teams);
			_teamCreatorFactory.CreateTeamCreator(Arg.Any<TeamGenerationMethod>()).Returns(teamCreator);

			_playerSelectionController.Create(new PlayersViewModel());

			_tournamentCreator.Received(1).CreateTournament(teams);
		}

		[Fact]
		public void Given_TournamentCreator_Returns_Tournament_When_Create_Then_AddTournamentCommand_Is_Executed_With_Tournament()
		{
			var tournament = ArrangeTournament();
			_tournamentCreator.CreateTournament(Arg.Any<IEnumerable<Team>>()).Returns(tournament);

			_playerSelectionController.Create(new PlayersViewModel());

			_addTournamentCommand.Received(1).Execute(tournament);
		}

		[Theory]
		[InlineData(TeamGenerationMethod.Random)]
		[InlineData(TeamGenerationMethod.GroupByAffiliation)]
		public void Given_Team_Generation_Enum_When_Create_Then_Team_Creator_Factory_Is_Execute_With_Team_Generation_Enum(TeamGenerationMethod teamGenerationMethod)
		{
			var playersViewModel = new PlayersViewModel
			{
				TeamGenerationMethod = teamGenerationMethod
			};

			_playerSelectionController.Create(playersViewModel);

			_teamCreatorFactory.Received(1).CreateTeamCreator(teamGenerationMethod);
		}


		private Tournament ArrangeTournament()
		{
			return new Tournament
			{
				Teams = ArrangeTwoTeams(),
			};
		}

		private IEnumerable<Team> ArrangeTwoTeams()
		{
			return new List<Team>
			{
				new Team(null),
				new Team(null),
			};
		}
	}
}
