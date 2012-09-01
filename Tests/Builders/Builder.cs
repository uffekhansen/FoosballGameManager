using AutoMapper;

namespace Tests.Builders
{
	public class Builder<T> : IBuilder<T> where T : new()
	{
		private readonly IPersister _persister = new NullPersister();

		public Builder() {}

		public Builder(IPersister persister)
		{
			_persister = persister;
		}

		public virtual T Build()
		{
			var entity = CreateEntity();

			_persister.Add(entity);

			return entity;
		}

		public virtual T CreateEntity()
		{
			return Mapper.DynamicMap<T>(this);
		}
	}
}