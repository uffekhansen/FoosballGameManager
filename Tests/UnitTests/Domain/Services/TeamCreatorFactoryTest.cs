using Domain.Enums;
using Domain.Services;
using Domain.Tools;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Domain.Services
{
	public class TeamCreatorFactoryTest
	{
		private readonly TeamCreatorFactory _teamCreatorFactory;

		public TeamCreatorFactoryTest()
		{
			var teamCreators = new ITeamCreator[]
			{
				new RandomTeamCreator(Substitute.For<IRandom>()),
				new GroupedAffiliationTeamCreator(Substitute.For<IRandom>())
			};

			_teamCreatorFactory = new TeamCreatorFactory(teamCreators);
		}

		[Fact]
		public void Given_Random_As_Input_When_CreateTeamCreator_Then_A_RandomTeamCreator_Is_Returned()
		{
			var teamCreator = _teamCreatorFactory.MakeTeamCreator(TeamGenerationMethod.Random);

			teamCreator.Should().BeOfType<RandomTeamCreator>();
		}

		[Fact]
		public void Given_GroupByAffiliation_As_Input_When_CreateTeamCreator_Then_A_GroupedAffiliationTeamCreator_Is_Returned()
		{
			var teamCreator = _teamCreatorFactory.MakeTeamCreator(TeamGenerationMethod.GroupByAffiliation);

			teamCreator.Should().BeOfType<GroupedAffiliationTeamCreator>();
		}
	}
}
