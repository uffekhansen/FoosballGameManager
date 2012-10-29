using System;

namespace Domain.Entities
{
	public class AssignedIdEntity
	{
		private readonly Guid _id = Guid.NewGuid();

		public virtual Guid Id
		{
			get { return _id; }
		}
	}
}
