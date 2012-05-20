﻿using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Services
{
	public class RandomTeamCreator : TeamCreator
	{
		protected override List<Team> GenerateTeams(List<Player> players, int playersPerTeam)
		{
			var teams = new List<Team>();

			for (int i = 0; i < players.Count; i += playersPerTeam)
			{
				teams.Add(new Team
				{
					Players = players
						.GetRange(i, playersPerTeam)
						.ToList()
				});
			}

			return teams;
		}
	}
}
