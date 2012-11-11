using System.Collections.Generic;

namespace Domain.Entities
{
	public class Tournament
	{
		public IEnumerable<Team> Teams
		{
			get;
			set;
		}
	}
}
