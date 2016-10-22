using UnityEngine;
using System.Collections;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Map;
using System.Collections.Generic;
using Assets.Scripts.LevelEditor;
using Assets.Scripts.Game.Actors;

namespace Assets.Scripts.States
{
	
	public class LoadLevel : State
	{
		int rows, columns;

		[SerializeField]
		GameObject field = null;

		[SerializeField]
		GameObject player = null;

		public override void Init()
		{
			Game.Game.Instance.CurrentLevel.Grid.Clear();
			Game.Game.Instance.CurrentLevel.Grid = new Dictionary<Vector2, Field>();

			LevelInfo info = new LevelInfo("default.level");
			CreateLevel(info);
		}

		public override void UpdateLoop()
		{
			ChangeState<DummyState>();
		}

		void CreateLevel(LevelInfo level)
		{
			GameObject grid = new GameObject();
			grid.name = "grid";

			foreach (var fieldInfo in level.Grid)
			{
				GameObject tile = Instantiate(field, fieldInfo.first, Quaternion.identity) as GameObject;
				tile.transform.parent = grid.transform;
				Game.Game.Instance.CurrentLevel.Grid[tile.transform.position] = tile.GetComponent<Field>();
			}

			foreach (Field field in Game.Game.Instance.CurrentLevel.Grid.Values)
			{
				List<Vector2> displacements = new List<Vector2> { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1) };
				foreach (Vector2 displacement in displacements)
				{
					Field neighbour = null;
					Game.Game.Instance.CurrentLevel.Grid.TryGetValue((Vector2)field.transform.position + displacement, out neighbour);

					if (neighbour != null)
						field.Neighbours[displacement] = neighbour;
				}

			}

			GameObject playersParent = new GameObject();
			playersParent.name = "players";
			GameObject newPlayer = Instantiate(player, level.PlayersPosition, Quaternion.identity) as GameObject;
			Game.Game.Instance.CurrentLevel.ActivePlayer = newPlayer.GetComponent<Player>();
			newPlayer.transform.parent = playersParent.transform;
		}
	}
}
