using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;
using UnityEngine;
using Assets.Scripts.Utilities;
using Assets.Scripts.Game;
using System.IO;

namespace Assets.Scripts.LevelEditor
{
	public class LevelInfo
	{
		#region variables

		List<Pair<Vector3, FieldType>> grid;

		Vector3 playersPosition;

		#endregion

		#region properties

		public List<Pair<Vector3, FieldType>> Grid
		{
			get { return grid; }
		}

		public Vector3 PlayersPosition
		{
			get { return playersPosition; }
		}

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

		public LevelInfo(string path)
		{
			grid = new List<Pair<Vector3, FieldType>>();

			using (StreamReader reader = new StreamReader(path))
			{
				string line = reader.ReadLine();
				int fieldsCount = Int32.Parse(line);
				string[] words;

				for (int i = 0; i < fieldsCount; ++i)
				{
					line = reader.ReadLine();
					words = line.Split();
					grid.Add(new Pair<Vector3, FieldType>(new Vector3(Int32.Parse(words[0]), Int32.Parse(words[1]), Int32.Parse(words[2])), (FieldType)Int32.Parse(words[3])));

				}

				line = reader.ReadLine();
				words = line.Split();
				playersPosition = new Vector3(Int32.Parse(words[0]), Int32.Parse(words[1]), Int32.Parse(words[2]));
			}
		}

		public override string ToString()
		{
			string levelInfo = "";

			levelInfo += grid.Count + "\n";
			foreach (var field in grid)
			{
				levelInfo += field.first.x + " " + field.first.y + " " + field.first.z + " " + (int)field.second + "\n";
			}

			levelInfo += playersPosition.x + " " + playersPosition.y + " " + playersPosition.z + "\n\n";

			return levelInfo;
		}

		#endregion
	}
}

