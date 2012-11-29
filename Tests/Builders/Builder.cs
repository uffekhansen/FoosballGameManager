using System.Collections.Generic;
using AutoMapper;
using Domain.Extensions;

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

		public virtual IEnumerable<T> Build(int count)
		{
			var entities = new List<T>();
			count.TimesDo(() => entities.Add(Build()));
			return entities;
		}

		public virtual T CreateEntity()
		{
			return Mapper.DynamicMap<T>(this);
		}
	}
}