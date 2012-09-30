using System.Linq;
using DAL.Commands;
using Domain.Entities;
using FluentAssertions;
using NHibernate.Linq;
using Tests.Extensions;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL.Commands
{
	public class AddCommandTest : InDatabaseTest
	{
		private readonly AddCommand<Player> _addCommand;

		public AddCommandTest()
		{
			_addCommand = new AddCommand<Player>(_session);
		}

		[Fact]
		public void Given_Entity_When_Calling_Execute_Then_Player_Is_Persisted()
		{
			var entity = new Player();

			_session.ExecuteWithTransactionAndClear(() => _addCommand.Execute(entity));

			var persistedPlayer = _session.Query<Player>().First();
			persistedPlayer.Should().NotBeNull();
		}
	}
}
