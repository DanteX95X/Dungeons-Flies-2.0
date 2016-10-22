using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;

namespace Assets.Scripts.Game
{
	public class Level
	{
		#region variables

		Dictionary<Vector2, Field> grid = new Dictionary<Vector2, Field>();
		Vector3 playerInitialPosition = new Vector3(3, 3, 0);

		#endregion

		#region properties

		public Dictionary<Vector2, Field> Grid
		{
			get { return grid; }
			set { grid = value; }
		}

		public Vector2 PlayerInitialPosition
		{
			get { return playerInitialPosition; }
			set { playerInitialPosition = value; }
		}

		#endregion
	}
}
