namespace Tests.Builders
{
	public interface IPersister
	{
		void Add(object obj);
		void Persist();
	}
}
