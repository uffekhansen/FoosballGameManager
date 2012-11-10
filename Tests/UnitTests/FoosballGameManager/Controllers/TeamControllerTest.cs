using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Queries;
using Domain.Exceptions;
using FluentAssertions;
using FoosballGameManager.Controllers;
using FoosballGameManager.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.FoosballGameManager.Controllers
{
	public class TeamControllerTest
	{
		private readonly TeamController _teamController;
		private readonly IGetPlayersQuery _getPlayersQuery;

		public TeamControllerTest()
		{
			_getPlayersQuery = Substitute.For<IGetPlayersQuery>();
			_teamController = new TeamController();
		}

		[Fact]
		public void Given_PlayersViewModel_When_Create_Then_Result_Is_Redirected_To_Create_Action_On_TournamentController()
		{
			var redirectToRouteResult = _teamController.Create(new PlayersViewModel()) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Controller"].Should().Be("Tournament");
			redirectToRouteResult.RouteValues["Action"].Should().Be("Create");
		}

		[Fact]
		public void Given_GetPlayersQuery_Throws_NotFoundException_Then_ModelState_Contains_Exception_Message()
		{
			const string exceptionMessage = "unit test";
			_getPlayersQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException(exceptionMessage); });

			var viewResult = _teamController.Create(null) as ViewResult;

			viewResult.ViewData.ModelState["_Form"].Errors.Single().ErrorMessage.Should().Be(exceptionMessage);
		}

		[Fact]
		public void Given_GetPlayersQuery_Throws_NotFoundException_Then_Result_Is_Redirected_To_Index_Action_On_TeamSelectionController()
		{
			_getPlayersQuery.When(x => x.Execute(Arg.Any<IEnumerable<Guid>>())).Do(x => { throw new NotFoundException("unit test"); });

			var redirectToRouteResult = _teamController.Create(null) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Controller"].Should().Be("TeamSelection");
			redirectToRouteResult.RouteValues["Action"].Should().Be("Index");
		}

		[Fact]
		public void Given_TeamCreator_And_View_Model_With_Player_Identifier_ListWhen_Create_Then_TeamCreator_Is_Executed_With_Player_Identifier_List()
		{
			var identifierList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
			var viewModel = new PlayersViewModel
			{
				SelectedPlayers = identifierList,
			};

			_teamController.Create(viewModel);

			_getPlayersQuery.Received(1).Execute(identifierList as IEnumerable<Guid>);
		}
	}
}
