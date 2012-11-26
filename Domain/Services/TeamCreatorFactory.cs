using System.Linq;
using Domain.Enums;

namespace Domain.Services
{
	public class TeamCreatorFactory : ITeamCreatorFactory
	{
		private readonly ITeamCreator[] _teamCreators;

		public TeamCreatorFactory(ITeamCreator[] teamCreators)
		{
			_teamCreators = teamCreators;
		}

		public ITeamCreator MakeTeamCreator(TeamGenerationMethod teamGenerationMethod)
		{
			if (teamGenerationMethod == TeamGenerationMethod.Random)
			{
				return _teamCreators.OfType<RandomTeamCreator>().Single();
			}

			return _teamCreators.OfType<GroupedAffiliationTeamCreator>().Single();
		}
	}
}
