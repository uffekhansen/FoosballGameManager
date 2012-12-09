using Domain.Enums;
using Domain.Strategies;
using Domain.Tools;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Domain.Strategies
{
	public class TeamCreationStrategyFactoryTest
	{
		private readonly TeamCreationStrategyFactory _teamCreationStrategyFactory;

		public TeamCreationStrategyFactoryTest()
		{
			var teamCreationStrategies = new ITeamCreationStrategy[]
			{
				new RandomTeamCreationStrategy(Substitute.For<IRandom>()),
				new GroupedAffiliationTeamCreationStrategy(Substitute.For<IRandomTeamCreationStrategy>(), Substitute.For<IRandom>()),
				new MixedAffiliationTeamCreationStrategy(Substitute.For<IRandom>()),
			};

			_teamCreationStrategyFactory = new TeamCreationStrategyFactory(teamCreationStrategies);
		}

		[Fact]
		public void Given_Random_As_Input_When_CreateTeamCreator_Then_A_RandomTeamCreator_Is_Returned()
		{
			var teamCreationStrategy = _teamCreationStrategyFactory.MakeTeamCreationStrategy(TeamGenerationMethod.Random);

			teamCreationStrategy.Should().BeOfType<RandomTeamCreationStrategy>();
		}

		[Fact]
		public void Given_GroupByAffiliation_As_Input_When_CreateTeamCreator_Then_A_GroupedAffiliationTeamCreator_Is_Returned()
		{
			var teamCreationStrategy = _teamCreationStrategyFactory.MakeTeamCreationStrategy(TeamGenerationMethod.GroupByAffiliation);

			teamCreationStrategy.Should().BeOfType<GroupedAffiliationTeamCreationStrategy>();
		}

		[Fact]
		public void Given_MixedAffiliation_As_Input_When_CreateTeamCreator_Then_A_MixedAffiliationTeamCreationStrategy_Is_Returned()
		{
			var teamCreationStrategy = _teamCreationStrategyFactory.MakeTeamCreationStrategy(TeamGenerationMethod.MixedAffiliation);

			teamCreationStrategy.Should().BeOfType<MixedAffiliationTeamCreationStrategy>();
		}
	}
}
