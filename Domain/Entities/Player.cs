namespace Domain.Entities
{
	public class Player : AssignedIdEntity
	{
		public virtual string Name { get; set; }

		public virtual string Affiliation { get; set; }
	}
}
