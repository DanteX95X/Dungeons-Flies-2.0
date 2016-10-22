using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;
using UnityEngine;
using Assets.Scripts.Utilities;
using Assets.Scripts.Game;

namespace Assets.Scripts.LevelEditor
{
	public class LevelInfo
	{
		#region variables

		List<Pair<Vector3, FieldType>> grid;

		Vector3 playersPosition;

		#endregion

		#region methods

		public LevelInfo()
		{
			grid = new List<Pair<Vector3, FieldType>>();
			playersPosition = new Vector3(0, 0);
			playersPosition += Vector3.zero;
		}

		public LevelInfo(Level level)
		{
			grid = new List<Pair<Vector3, FieldType>>();

			foreach (Field field in level.Grid.Values)
			{
				grid.Add(new Pair<Vector3, FieldType>(field.transform.position, field.Type));
			}

			if(level.ActivePlayer)
				playersPosition = level.ActivePlayer.transform.position;
		}

		public override string ToString()
		{
			string levelInfo = "";

			levelInfo += grid.Count + "\n";
			foreach (var field in grid)
			{
				levelInfo += field.first.x + " " + field.first.y + " " + field.first.z + " " + field.second.ToString() + "\n";
			}
			levelInfo += "\n";

			levelInfo += playersPosition.x + " " + playersPosition.y + " " + playersPosition.z + "\n\n";

			return levelInfo;
		}

		#endregion
	}
}

