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

			playersPosition = level.ActivePlayer.transform.position;
		}

		#endregion
	}
}

