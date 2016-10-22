using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class Game
	{
		#region variables

		static Game instance = null; 


		Level currentLevel = null;

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
			/*set
			{
				if (instance == null)
					instance = new Game();
				instance = value;
			}*/
		}


		public Level CurrentLevel
		{
			get { return currentLevel; }
			set { currentLevel = value; }
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

