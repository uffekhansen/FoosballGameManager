using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Commands;
using DAL.Queries;
using Domain.Entities;
using Domain.Services;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class PlayerSelectionControllerTest_Index
	{
		private IEnumerable<Player> _players;
		private readonly PlayerSelectionController _playerSelectionController;
		private readonly ITournamentCreator _tournamentCreator = Substitute.For<ITournamentCreator>();
		private readonly IGetEveryEntityQuery<Player> _getEveryPlayerEntityQuery = Substitute.For<IGetEveryEntityQuery<Player>>();

		public PlayerSelectionControllerTest_Index()
		{
			_tournamentCreator.CreateTournament(Arg.Any<IEnumerable<Team>>()).Returns(new Tournament());
			_playerSelectionController = new PlayerSelectionController(_getEveryPlayerEntityQuery, Substitute.For<IGetPlayersByIdsQuery>(), Substitute.For<IAddCommand<Tournament>>(), Substitute.For<ITeamCreatorFactory>(), _tournamentCreator);
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
		public void Given_GetEveryEntityQuery_For_Players_When_Index_Then_GetEntitiesQuery_Is_Executed()
		{
			_playerSelectionController.Index();

			_getEveryPlayerEntityQuery.Received(1).Execute();
		}

		[Fact]
		public void Given_PlayerSelectionController_When_Index_Then_Result_Is_Redirected_To_Index_Action()
		{
			var viewResult = _playerSelectionController.Index() as ViewResult;

			viewResult.ViewName.Should().Be("");
		}

		private void ArrangeGetEntitiesQueryReturningPlayers()
		{
			ArrangePlayers();
			_getEveryPlayerEntityQuery.Execute().Returns(_players);
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
