using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Commands;
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
	public class PlayerControllerTest
	{
		private readonly IAddPlayerCommand _addPlayerCommand;
		private readonly PlayerController _playerController;

		public PlayerControllerTest()
		{
			_addPlayerCommand = Substitute.For<IAddPlayerCommand>();
			_playerController = new PlayerController(_addPlayerCommand);
		}

		[Fact]
		public void Given_Player_And_AddPlayerCommand_When_Create_AddPlayerCommand_Is_Executed_With_Player()
		{
			var player = new Player();

			_playerController.Create(player);

			_addPlayerCommand.Received(1).Execute(player);
		}

		[Fact]
		public void Given_PlayerController_When_Create_Then_Result_Is_Redirected_To_Index_Action_On_TeamSelectionController()
		{
			var redirectToRouteResult = _playerController.Create(null) as RedirectToRouteResult;

			redirectToRouteResult.RouteValues["Controller"].Should().Be("TeamSelection");
			redirectToRouteResult.RouteValues["Action"].Should().Be("Index");
		}

		[Fact]
		public void Given_PlayerController_When_New_Then_Returned_Model_Type_Is_Player()
		{
			var viewResult = _playerController.New() as ViewResult;

			viewResult.Model.Should().BeNull();
		}

		[Fact]
		public void Given_AddPlayerCommand_Throws_AlreadyExistsException_When_New_Then_Returned_ViewName_Is_New()
		{
			_addPlayerCommand.When(x => x.Execute(Arg.Any<Player>())).Do(x => { throw new AlreadyExistsException("unit test"); });

			var result = _playerController.Create(null) as ViewResult;

			result.ViewName.Should().Be("New");
		}
	}
}
