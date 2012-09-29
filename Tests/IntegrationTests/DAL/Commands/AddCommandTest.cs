using System.Linq;
using Domain.Entities;
using FluentAssertions;
using NHibernate;
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
		public void Given_Player_When_Calling_Execute_Then_Player_Is_Persisted()
		{
			var player = new Player();

			_session.ExecuteWithTransactionAndClear(() => _addCommand.Execute(player));

			var persistedPlayer = _session.Query<Player>().First();
			persistedPlayer.Should().NotBeNull();
		}
	}

	internal class AddCommand<T> where T : class
	{
		private readonly ISession _session;

		public AddCommand(ISession session)
		{
			_session = session;
		}

		public void Execute(T t)
		{
			_session.Save(t);
		}
	}
}