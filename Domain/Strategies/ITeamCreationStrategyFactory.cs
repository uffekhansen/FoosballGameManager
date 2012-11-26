using Domain.Enums;

namespace Domain.Strategies
{
	public interface ITeamCreationStrategyFactory
	{
		ITeamCreationStrategy MakeTeamCreationStrategy(TeamGenerationMethod teamGenerationMethod);
	}
}
