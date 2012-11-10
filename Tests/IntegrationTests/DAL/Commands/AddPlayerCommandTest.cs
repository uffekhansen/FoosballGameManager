using System;
using DAL.Commands;
using DAL.Queries;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using NSubstitute;
using Tests.Builders;
using Xunit;

namespace Tests.IntegrationTests.DAL.Commands
{
	public class AddPlayerCommandTest
	{
		private readonly AddPlayerCommand _addPlayerCommand;
		private readonly IIsPlayerNameUniqueQuery _isPlayerNameUniqueQuery = Substitute.For<IIsPlayerNameUniqueQuery>();
		private readonly IAddCommand<Player> _addCommandForPlayer = Substitute.For<IAddCommand<Player>>();

		public AddPlayerCommandTest()
		{
			_isPlayerNameUniqueQuery.Execute(Arg.Any<string>()).Returns(true);
			_addPlayerCommand = new AddPlayerCommand(_addCommandForPlayer, _isPlayerNameUniqueQuery);
		}

		[Fact]
		public void Given_Player_When_Calling_Execute_Then_IsPlayerNameUnique_Is_Executed()
		{
			var player = ArrangePlayer();

			_addPlayerCommand.Execute(player);

			_isPlayerNameUniqueQuery.Received(1).Execute(player.Name);
		}

		[Fact]
		public void Given_IsPlayerNameUniqueQuery_Return_False_When_Calling_Execute_Then_AlreadyExistException_Is_Thrown()
		{
			var player = ArrangePlayer();
			_isPlayerNameUniqueQuery.Execute(Arg.Any<string>()).Returns(false);

			Action execute = () => _addPlayerCommand.Execute(player);

			execute.ShouldThrow<AlreadyExistsException>();
		}

		[Fact]
		public void Given_IsPlayerNameUniqueQuery_Return_True_When_Calling_Execute_Then_No_Exception_Is_Thrown()
		{
			var player = ArrangePlayer();

			Action execute = () => _addPlayerCommand.Execute(player);

			execute();
		}

		[Fact]
		public void Given_AddCommand_When_Execute_Then_AddCommand_Is_Executed()
		{
			var player = ArrangePlayer();

			_addPlayerCommand.Execute(player);

			_addCommandForPlayer.Received(1).Execute(player);
		}

		protected Player ArrangePlayer()
		{
			return new PlayerBuilder().Build();
		}
	}
}
