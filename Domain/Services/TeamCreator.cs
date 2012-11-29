using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Strategies;

namespace Domain.Services
{
	public class TeamCreator : ITeamCreator
	{
		private int _playersPerTeam = 2;
		protected IEnumerable<Player> _players;

		private readonly ITeamCreationStrategyFactory _teamCreationStrategyFactory;

		public int PlayersPerTeam
		{
			get { return _playersPerTeam; }
			set { _playersPerTeam = value; }
		}

		public IEnumerable<Player> Players
		{
			get { return _players; }
			set { _players = value; }
		}

		public TeamCreator(ITeamCreationStrategyFactory teamCreationStrategyFactory)
		{
			_teamCreationStrategyFactory = teamCreationStrategyFactory;
		}

		public IList<Team> CreateTeams(TeamGenerationMethod teamGenerationMethod)
		{
			RequireValidSettings();

			var teamCreationStrategy = _teamCreationStrategyFactory.MakeTeamCreationStrategy(teamGenerationMethod);
			teamCreationStrategy.Players = _players;
			teamCreationStrategy.PlayersPerTeam = _playersPerTeam;

			var teams = teamCreationStrategy.CreateTeams();
			EnsureCorrectNumberTeams(teams);

			return teams;
		}

		private void RequireValidSettings()
		{
			RequirePlayers();
			RequirePlayersEnoughForATeam();
			RequireThatNumberPlayerMatchesTeamSettings();
		}

		private void RequirePlayers()
		{
			if (!_players.Any())
			{
				throw new TeamGenerationException("Zero players in list");
			}
		}

		private void RequirePlayersEnoughForATeam()
		{
			if (_players.Count() < _playersPerTeam)
			{
				throw new TeamGenerationException("Not enough players for a single team");
			}
		}

		private void RequireThatNumberPlayerMatchesTeamSettings()
		{
			if (_players.Count() % _playersPerTeam != 0)
			{
				throw new TeamGenerationException("Number players not divisable with number players per team in team settings");
			}
		}

		private void EnsureCorrectNumberTeams(IEnumerable<Team> teams)
		{
			var expectedNumberTeams = _players.Count() / _playersPerTeam;
			if (teams.Count() != expectedNumberTeams)
			{
				throw new TeamGenerationException("Generated number of teams did not match team settings and player count");
			}
		}
	}
}
