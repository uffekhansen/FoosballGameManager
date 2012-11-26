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
			if (teamGenerationMethod == TeamGenerationMethod.Random)
			{
				return _teamCreationStrategies.OfType<IRandomTeamCreationStrategy>().Single();
			}

			return _teamCreationStrategies.OfType<IGroupedAffiliationTeamCreationStrategy>().Single();
		}
	}
}
