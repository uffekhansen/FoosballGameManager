using System.Collections;
using Domain.Entities;

namespace Tests.IntegrationTests.DAL
{
	internal class PlayerComparer : IEqualityComparer
	{
		public new bool Equals(object x, object y)
		{
			return (x as Player).Id == (y as Player).Id;
		}

		public int GetHashCode(object obj)
		{
			throw new System.NotImplementedException();
		}
	}
}
