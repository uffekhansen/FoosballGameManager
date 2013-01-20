using System.Collections.Generic;
using Domain.Entities;
using Domain.Extensions;
using FluentNHibernate.Testing;
using Tests.Builders;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL
{
	public class TeamPersistenceTest : InDatabaseTest
	{
        //[Fact]
		public void Given_Team_When_Persisting_Then_Team_Is_Persisted()
		{
			var players = ArrangeTwoPlayers();
			_persister.Persist();

			new PersistenceSpecification<Team>(_session)
				.CheckList(p => p.Players, players, new PlayerComparer())
				.VerifyTheMappings();
		}

		private IEnumerable<Player> ArrangeTwoPlayers()
		{
			var players = new List<Player>();

			2.TimesDo(() => players.Add(new PlayerBuilder(_persister).Build()));

			return players;
		}
	}
}
