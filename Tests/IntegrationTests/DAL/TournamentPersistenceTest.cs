using System.Collections;
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
        //[Fact]
		public void Given_Tournament_When_Persisting_Then_Tournament_Is_Persisted()
		{
			var teams = ArrangeTwoTeams();
			_persister.Persist();

			new PersistenceSpecification<Tournament>(_session)
				.CheckList(p => p.Teams, teams, new TeamComparer())
				.VerifyTheMappings();
		}

		private IEnumerable<Team> ArrangeTwoTeams()
		{
			return new List<Team>
			{
				new TeamBuilder { Players = ArrangeTwoPersistedPlayers() }.Build(),
				new TeamBuilder { Players = ArrangeTwoPersistedPlayers() }.Build(),
			};
		}

		private IEnumerable<Player> ArrangeTwoPersistedPlayers()
		{
			return new List<Player>
			{
				ArrangePersistedPlayer(),
				ArrangePersistedPlayer(),
			};
		}

		private Player ArrangePersistedPlayer()
		{
			return new PlayerBuilder(_persister).Build();
		}
	}

	internal class TeamComparer : IEqualityComparer
	{
		public new bool Equals(object x, object y)
		{
			var teamX = x as Team;
			var teamY = y as Team;

			return teamX.Id == teamY.Id;
		}

		public int GetHashCode(object obj)
		{
			throw new System.NotImplementedException();
		}
	}
}
