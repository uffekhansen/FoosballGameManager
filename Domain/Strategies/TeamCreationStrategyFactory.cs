using System.Linq;
using Domain.Enums;

namespace Domain.Strategies
{
	public class TeamCreationStrategyFactory : ITeamCreationStrategyFactory
	{
		private readonly ITeamCreationStrategy[] _teamCreationStrategies;

		public TeamCreationStrategyFactory(ITeamCreationStrategy[] teamCreationStrategies)
		{
			_teamCreationStrategies = teamCreationStrategies;
		}

		public ITeamCreationStrategy MakeTeamCreationStrategy(TeamGenerationMethod teamGenerationMethod)
		{
			switch (teamGenerationMethod)
			{
				default:
				case TeamGenerationMethod.Random:
					return _teamCreationStrategies.OfType<IRandomTeamCreationStrategy>().Single();
				case TeamGenerationMethod.MixedAffiliation:
					return _teamCreationStrategies.OfType<IMixedAffiliationTeamCreationStrategy>().Single();
				case TeamGenerationMethod.GroupByAffiliation:
					return _teamCreationStrategies.OfType<IGroupedAffiliationTeamCreationStrategy>().Single();
			}
		}
	}
}
