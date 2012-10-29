using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Entities;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class TeamControllerTest
	{
		private IEnumerable<Player> _players;

		[Fact]
		public void Given_Players_When_Index_Then_Players_Are_Returned_In_View_Model()
		{
			ArrangePlayers();
			var getEntitiesQuery = ArrangeGetEntitiesQuery();
			var teamController = new TeamController(getEntitiesQuery);

			var viewResult = teamController.Index() as ViewResult;

			viewResult.Model.As<PlayersViewModel>().Players.Should().BeEquivalentTo(_players);
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

		private IGetEntitiesQuery<Player> ArrangeGetEntitiesQuery()
		{
			var getEntitiesQuery = Substitute.For<IGetEntitiesQuery<Player>>();
			getEntitiesQuery.Execute().Returns(_players);
			return getEntitiesQuery;
		}
	}
}
