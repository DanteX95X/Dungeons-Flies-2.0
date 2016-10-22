using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;
using UnityEngine;
using Assets.Scripts.Utilities;
using Assets.Scripts.Game;
using System.IO;
using Assets.Scripts.Game.Actors;

namespace Assets.Scripts.LevelEditor
{
	public class LevelInfo
	{
		#region variables

		List<Pair<Vector3, FieldType>> grid;

		Vector3 playersPosition = new Vector3(-100000, -100000);

		List<Vector3> enemiesPositions;

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

		public List<Vector3> EnemiesPositions
		{
			get { return enemiesPositions; }
		}

		#endregion

		#region methods

		public LevelInfo()
		{
			grid = new List<Pair<Vector3, FieldType>>();
			enemiesPositions = new List<Vector3>();
		}

		public LevelInfo(Level level)
		{
			grid = new List<Pair<Vector3, FieldType>>();
			enemiesPositions = new List<Vector3>();

			foreach (Field field in level.Grid.Values)
			{
				grid.Add(new Pair<Vector3, FieldType>(field.transform.position, field.Type));
			}

			if(level.ActivePlayer)
				playersPosition = level.ActivePlayer.transform.position;

			foreach (Enemy enemy in level.Enemies)
			{
				enemiesPositions.Add(enemy.transform.position);
			}
		}

		public LevelInfo(string path)
		{
			grid = new List<Pair<Vector3, FieldType>>();
			enemiesPositions = new List<Vector3>();

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

				line = reader.ReadLine();
				int enemiesCount = Int32.Parse(line);

				for (int i = 0; i < enemiesCount; ++i)
				{
					line = reader.ReadLine();
					words = line.Split();
					enemiesPositions.Add(new Vector3(Int32.Parse(words[0]), Int32.Parse(words[1]), Int32.Parse(words[2])));
				}
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

			levelInfo += playersPosition.x + " " + playersPosition.y + " " + playersPosition.z + "\n";
			levelInfo += enemiesPositions.Count + "\n";
			foreach (Vector3 position in enemiesPositions)
			{
				levelInfo += position.x + " " + position.y + " " + position.z + "\n";
			}

			return levelInfo;
		}

		#endregion
	}
}

