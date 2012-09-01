namespace Tests.Builders
{
	class NullPersister : IPersister
	{
		public void Add(object obj) {}
		public void Persist() {}
	}
}
