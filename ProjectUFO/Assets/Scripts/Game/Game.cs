using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class Game
	{
		#region variables

		static Game instance = null; 


		Level currentLevel = null;

		string levelPath = "default.level";

		#endregion
		
		#region properties

		public static Game Instance
		{
			get
			{
				if (instance == null)
					instance = new Game();
				return instance;
			}
		}


		public Level CurrentLevel
		{
			get { return currentLevel; }
			set { currentLevel = value; }
		}

		public string LevelPath
		{
			get { return levelPath; }
		}


		#endregion

		#region methods

		private Game()
		{
			currentLevel = new Level();
		}

		#endregion
	}
}

