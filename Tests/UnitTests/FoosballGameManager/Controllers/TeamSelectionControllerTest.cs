﻿using System.Collections.Generic;
using System.Web.Mvc;
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
	public class TeamSelectionControllerTest
	{
		private IEnumerable<Player> _players;
		private readonly TeamSelectionController _teamSelectionController;
		private readonly ITeamCreator _teamCreator = Substitute.For<ITeamCreator>();
		private readonly IGetEntitiesQuery<Player> _getPlayerEntitiesQuery = Substitute.For<IGetEntitiesQuery<Player>>();

		public TeamSelectionControllerTest()
		{
			_teamSelectionController = new TeamSelectionController(_getPlayerEntitiesQuery, _teamCreator);
		}

		[Fact]
		public void Given_Players_When_Index_Then_Players_Are_Returned_In_View_Model()
		{
			ArrangeGetEntitiesQueryReturningPlayers();

			var viewResult = _teamSelectionController.Index() as ViewResult;

			viewResult.Model.As<PlayersViewModel>().Players.Should().BeEquivalentTo(_players);
		}

		[Fact]
		public void Given_TeamSelectionController_When_Index_Then_Returned_Model_Type_Is_PlayersViewModel()
		{
			var viewResult = _teamSelectionController.Index() as ViewResult;

			viewResult.Model.Should().BeOfType<PlayersViewModel>();
		}

		[Fact]
		public void Given_GetEntitiesQuery_For_Players_When_Index_Then_GetEntitiesQuery_Is_Executed()
		{
			_teamSelectionController.Index();

			_getPlayerEntitiesQuery.Received(1).Execute();
		}

		[Fact]
		public void Given_TeamCreator_When_Create_Then_TeamCreator_Is_Executed()
		{
			_teamSelectionController.Create(null);

			_teamCreator.Received(1).CreateTeams();
		}

		[Fact]
		public void Given_PlayersViewModel_When_Index_Then_Result_Is_Redirected_To_Index_Action()
		{
			var redirectToRouteResult = _teamSelectionController.Create(new PlayersViewModel()) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Action"].Should().Be("Index");
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