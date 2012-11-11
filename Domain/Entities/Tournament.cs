using System.Collections.Generic;

namespace Domain.Entities
{
	public class Tournament : AssignedIdEntity
	{
		public IEnumerable<Team> Teams
		{
			get;
			set;
		}
	}
}
