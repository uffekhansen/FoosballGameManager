using System.Collections.Generic;

namespace Domain.Entities
{
	public class Tournament : AssignedIdEntity
	{
		public virtual IEnumerable<Team> Teams
		{
			get;
			set;
		}
	}
}
