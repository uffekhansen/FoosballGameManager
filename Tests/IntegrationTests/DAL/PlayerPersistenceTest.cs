using System.Collections.Generic;
using Domain.Entities;
using FluentNHibernate.Testing;
using Tests.Builders;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL
{
	public class TournamentPersistenceTest : InDatabaseTest
	{
		[Fact]
		public void Given_Tournament_When_Executing_Then_Tournament_Is_Returned()
		{
			var teams = ArrangeTeams();
			_persister.Persist();

			new PersistenceSpecification<Tournament>(_session)
				.CheckProperty(p => p.Teams, teams)
				.VerifyTheMappings();
		}

		private IEnumerable<Team> ArrangeTeams()
		{
			return new List<Team>
			{
				new Team(ArrangePlayers()),
				new Team(ArrangePlayers()),
			};
		}

		private IEnumerable<Player> ArrangePlayers()
		{
			return new List<Player>
			{
				ArrangePlayer(),
				ArrangePlayer(),
			};
		}

		private Player ArrangePlayer()
		{
			return new PlayerBuilder(_persister).Build();
		}
	}
}
