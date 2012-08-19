using System;

namespace Domain.Entities
{
	public class Player
	{
		public virtual Guid Id
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string Affiliation
		{
			get;
			set;
		}
	}
}
