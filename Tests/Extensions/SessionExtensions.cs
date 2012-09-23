using System;
using Domain.Extensions;
using FluentAssertions;
using FluentAssertions.Assertions;
using NHibernate;
using NSubstitute;

namespace Tests.Extensions
{
	public static class Extensions
	{
		public static object[] ToObjectArray<T>(this T @this)
		{
			return new object[]
			{
				@this,
			};
		}

		public static void ExecuteWithTransactionAndClear(this ISession @this, Action command)
		{
			using (var tx = @this.BeginTransaction())
			{
				try
				{
					command();
					tx.Commit();
					@this.Clear();
				}
				catch
				{
					tx.Rollback();
					throw;
				}
			}
		}

		public static Guid SaveWithTransaction(this ISession @this, object entity)
		{
			using (var tx = @this.BeginTransaction())
			{
				try
				{
					var id = @this.Save(entity);
					tx.Commit();

					return (Guid)id;
				}
				catch
				{
					tx.Rollback();
					throw;
				}
			}
		}

		public static void SaveWithTransaction(this ISession @this, params object[] entities)
		{
			using (var tx = @this.BeginTransaction())
			{
				try
				{
					entities.Each(entity => @this.Save(entity));
					tx.Commit();
				}
				catch
				{
					tx.Rollback();
					throw;
				}
			}
		}
	}
}