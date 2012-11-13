using System.Collections.Generic;
using Domain.ValueObjects;

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
