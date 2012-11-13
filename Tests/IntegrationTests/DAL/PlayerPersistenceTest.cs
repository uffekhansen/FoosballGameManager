using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;
using FluentNHibernate.Testing;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL
{
	public class TournamentPersistenceTest : InDatabaseTest
	{
		[Fact]
		public void Given_Tournament_When_Executing_Then_Tournament_Is_Returned()
		{
			new PersistenceSpecification<Tournament>(_session)
				//.CheckProperty(p => p.Teams, ArrangeTeams())
				.VerifyTheMappings();
		}

		private IEnumerable<Team> ArrangeTeams()
		{
			return new List<Team>
			{
				new Team(null),
				new Team(null),
			};
		}
	}
}
