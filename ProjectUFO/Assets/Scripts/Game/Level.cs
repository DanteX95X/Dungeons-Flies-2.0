using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	public class Level
	{
		#region variables

		static Dictionary<Vector3, Field> grid = new Dictionary<Vector3, Field>();

		#endregion

		#region properties

		public static Dictionary<Vector3, Field> Grid
		{
			get { return grid; }
			set { grid = value; }
		}

		#endregion
	}
}
