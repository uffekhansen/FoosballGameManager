using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class PlayerSelectionControllerTest
	{
		private IEnumerable<Player> _players;
		private readonly PlayerSelectionController _playerSelectionController;
		private readonly ITeamCreator _teamCreator = Substitute.For<ITeamCreator>();
		private readonly IGetPlayersQuery _getPlayersQuery;
		private readonly IGetEntitiesQuery<Player> _getPlayerEntitiesQuery = Substitute.For<IGetEntitiesQuery<Player>>();

		public PlayerSelectionControllerTest()
		{
			_getPlayersQuery = Substitute.For<IGetPlayersQuery>();
			_playerSelectionController = new PlayerSelectionController(_getPlayerEntitiesQuery, _getPlayersQuery, _teamCreator);
		}

		[Fact]
		public void Given_Players_When_Index_Then_Players_Are_Returned_In_View_Model()
		{
			ArrangeGetEntitiesQueryReturningPlayers();

			var viewResult = _playerSelectionController.Index() as ViewResult;

			viewResult.Model.As<PlayersViewModel>().Players.Should().BeEquivalentTo(_players);
		}

		[Fact]
		public void Given_PlayerSelectionController_When_Index_Then_Returned_Model_Type_Is_PlayersViewModel()
		{
			var viewResult = _playerSelectionController.Index() as ViewResult;

			viewResult.Model.Should().BeOfType<PlayersViewModel>();
		}

		[Fact]
		public void Given_GetEntitiesQuery_For_Players_When_Index_Then_GetEntitiesQuery_Is_Executed()
		{
			_playerSelectionController.Index();

			_getPlayerEntitiesQuery.Received(1).Execute();
		}

		[Fact]
		public void Given_TeamCreator_When_Create_Then_TeamCreator_Is_Executed()
		{
			_playerSelectionController.Create(new PlayersViewModel());

			_teamCreator.Received(1).CreateTeams();
		}

		[Fact]
		public void Given_PlayerSelectionController_When_Index_Then_Result_Is_Redirected_To_Index_Action()
		{
			var viewResult = _playerSelectionController.Index() as ViewResult;

			viewResult.ViewName.Should().Be("");
		}

		[Fact]
		public void Given_PlayersViewModel_When_Create_Then_Result_Is_Redirected_To_Create_Action_On_TournamentController()
		{
			var redirectToRouteResult = _playerSelectionController.Create(new PlayersViewModel()) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Controller"].Should().Be("Tournament");
			redirectToRouteResult.RouteValues["Action"].Should().Be("Show");
		}

		[Fact]
		public void Given_GetPlayersQuery_Throws_NotFoundException_Then_ModelState_Contains_Exception_Message()
		{
			const string exceptionMessage = "unit test";
			_getPlayersQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException(exceptionMessage); });

			var viewResult = _playerSelectionController.Create(new PlayersViewModel()) as ViewResult;

			viewResult.ViewData.ModelState["_FORM"].Errors.Single().ErrorMessage.Should().Be(exceptionMessage);
		}

		[Fact]
		public void Given_GetPlayersQuery_Throws_NotFoundException_Then_Result_Is_Redirected_To_Index_Action_On_PlayerSelectionController()
		{
			_getPlayersQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException("unit test"); });

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

			_getPlayersQuery.Received(1).Execute(identifierList);
		}

		private void ArrangeGetEntitiesQueryReturningPlayers()
		{
			ArrangePlayers();
			_getPlayerEntitiesQuery.Execute().Returns(_players);
		}

		private void ArrangePlayers()
		{
			_players = new List<Player>
			{
				new Player
				{
					Affiliation = "ATeam",
					Name = "A",
				},
				new Player
				{
					Affiliation = "ATeam",
					Name = "B",
				},
			};
		}
	}
}
