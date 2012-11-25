using Domain.Enums;

namespace Domain.Services
{
	public interface ITeamCreatorFactory
	{
		ITeamCreator CreateTeamCreator(TeamGenerationMethod teamCreationMethod);
	}
}
