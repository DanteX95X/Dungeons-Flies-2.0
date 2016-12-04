using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game.Actors;

namespace Assets.Scripts.Game
{
	public class Level
	{
		#region variables

		Dictionary<Vector2, Field> grid;
		Player player;
		List<Enemy> enemies;
		string levelName;

		#endregion

		#region properties

		public Dictionary<Vector2, Field> Grid
		{
			get { return grid; }
			set { grid = value; }
		}

		public Player ActivePlayer
		{
			get { return player; }
			set { player = value; }
		}

		public List<Enemy> Enemies
		{
			get { return enemies; }
			set { enemies = value; }
		}

		public string LevelName
		{
			get { return levelName; }
			set { levelName = value; }
		}

		#region methods

		public Level()
		{
			grid = new Dictionary<Vector2, Field>();
			player = null;
			enemies = new List<Enemy>();
		}
			
		#endregion

		#endregion
	}
}
