namespace DAL.Queries
{
	public interface IIsPlayerNameUniqueQuery
	{
		bool Execute(string name);
	}
}
