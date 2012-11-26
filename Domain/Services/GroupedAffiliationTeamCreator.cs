using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using Domain.Tools;

namespace Domain.Services
{
	public class GroupedAffiliationTeamCreator : RandomTeamCreator
	{
		private IList<Team> _teams;
		private IList<Player> _excessPlayers;

		public GroupedAffiliationTeamCreator(IRandom random)
			: base(random)
		{
			
		}

		protected override IList<Team> GenerateTeams()
		{
			InitListsAsEmpty();

			var playersGroupedByAffiliation = _players.GroupBy(player => player.Affiliation);

			playersGroupedByAffiliation.Each(playerGroup => CreateTeamsWithSameAffiliation(playerGroup.ToList()));

			CreateRandomTeamsFromExcessPlayers();

			return _teams;
		}

		private void InitListsAsEmpty()
		{
			_teams = new List<Team>();
			_excessPlayers = new List<Player>();
		}

		private void CreateTeamsWithSameAffiliation(List<Player> playersSharingAffiliation)
		{
			while (playersSharingAffiliation.Count >= PlayersPerTeam)
			{
				var playersForTeam = playersSharingAffiliation.Take(PlayersPerTeam).ToList();

				AddTeam(playersForTeam);
				playersSharingAffiliation.RemoveRange(0, PlayersPerTeam);
			}

			AddExcessPlayers(playersSharingAffiliation);
		}

		private void AddTeam(IEnumerable<Player> players)
		{
			_teams.Add(new Team { Players = players });
		}

		private void AddExcessPlayers(IEnumerable<Player> playersSharingAffiliation)
		{
			_excessPlayers.Concat(playersSharingAffiliation);
		}

		private void CreateRandomTeamsFromExcessPlayers()
		{
			_teams.Concat(base.GenerateTeams());
		}
	}
}
