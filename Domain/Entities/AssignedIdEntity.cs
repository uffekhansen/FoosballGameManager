using System;

namespace Domain.Entities
{
	public class AssignedIdEntity
	{
		private Guid _id = Guid.NewGuid();

		public virtual Guid Id
		{
			set { _id = value; }
			get { return _id; }
		}
	}
}
