using System;
using Domain.Entities;
using FluentNHibernate.Testing;
using Xunit;

namespace Tests.IntegrationTests.DAL
{
	public class PlayerPersistenceTest : TestBase
	{
		[Fact]
		public void Given_Player_When_Executing_Then_Player_Is_Returned()
		{
			new PersistenceSpecification<Player>(_session)
				.CheckProperty(p => p.Affiliation, "Player affiliation")
				.CheckProperty(p => p.Id, Guid.NewGuid())
				.CheckProperty(p => p.Name, "Player name")
				.VerifyTheMappings();
		}
	}
}
