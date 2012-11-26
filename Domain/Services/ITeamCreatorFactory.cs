using Domain.Enums;

namespace Domain.Services
{
	public interface ITeamCreatorFactory
	{
		ITeamCreator MakeTeamCreator(TeamGenerationMethod teamCreationMethod);
	}
}
