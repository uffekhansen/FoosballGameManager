using Domain.Entities;

namespace DAL.Queries
{
	public interface IAddPlayerQuery
	{
		void Execute(Player player);
	}
}