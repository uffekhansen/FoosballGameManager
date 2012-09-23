using System;
using Domain.Entities;
using FluentNHibernate.Testing;
using Xunit;
using Xunit.Extensions;

namespace Tests.IntegrationTests.DAL
{
	public class GetPlayerQuerytest : TestBase
	{
		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public void Given_Players_When_Calling_Execute_Then_All_Existing_Players_Are_Returned(int numberPlayers)
		{
			//numberPlayers.TimesDoM
		}
	}
}
