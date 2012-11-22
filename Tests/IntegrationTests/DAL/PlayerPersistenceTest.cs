using Domain.Entities;
using FluentNHibernate.Testing;
using Tests.Infrastructure.TestBases;
using Xunit;

namespace Tests.IntegrationTests.DAL
{
	public class PlayerPersistenceTest : InDatabaseTest
	{
		[Fact]
		public void Given_Player_When_Persisting_Then_Player_Is_Persisted()
		{
			new PersistenceSpecification<Player>(_session)
				.CheckProperty(p => p.Affiliation, "Player affiliation")
				.CheckProperty(p => p.Name, "Player name")
				.VerifyTheMappings();
		}
	}
}
