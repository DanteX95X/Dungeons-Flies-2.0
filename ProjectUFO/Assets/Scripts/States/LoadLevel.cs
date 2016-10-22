using UnityEngine;
using System.Collections;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Map;
using System.Collections.Generic;

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
			rows = 5;
			columns = 5;

			Game.Game.Instance.CurrentLevel.Grid.Clear();
			Game.Game.Instance.CurrentLevel.Grid = new Dictionary<Vector2, Field>();
			GameObject grid = new GameObject("Grid");

			for (int i = rows - 1; i >= 0; --i)
			{
				for (int j = 0; j < columns; ++j)
				{
					GameObject currentField = Instantiate(field, new Vector3(i, j, 1), Quaternion.identity) as GameObject;
					currentField.transform.parent = grid.transform;
					Game.Game.Instance.CurrentLevel.Grid[currentField.transform.position] = currentField.GetComponent<Field>();
				}
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

			Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
		}

		public override void UpdateLoop()
		{
			ChangeState<DummyState>();
		}
	}
}
