using UnityEngine;
using System.Collections;
using Assets.Scripts.Game;
using System.Collections.Generic;

namespace Assets.Scripts.States
{
	
	public class LoadLevel : State
	{
		int rows, columns;

		[SerializeField]
		GameObject field;

		public override void Init()
		{
			rows = 5;
			columns = 5;
			Level.Grid.Clear();
			Level.Grid = new Dictionary<Vector3, Field>();
			GameObject grid = new GameObject("Grid");

			for (int i = rows - 1; i >= 0; --i)
			{
				for (int j = 0; j < columns; ++j)
				{
					GameObject currentField = Instantiate(field, new Vector3(i, j), Quaternion.identity) as GameObject;
					currentField.transform.parent = grid.transform;
					Level.Grid.Add(currentField.transform.position, currentField.GetComponent<Field>());
				}
			}

			foreach (Field field in Level.Grid.Values)
			{
				List<Vector3> displacements = new List<Vector3> { new Vector3(1, 0), new Vector3(0, 1), new Vector3(-1, 0), new Vector3(0, -1) };
				foreach (Vector3 displacement in displacements)
				{
					Field neighbour = null;
					Level.Grid.TryGetValue(field.transform.position + displacement, out neighbour);

					if (neighbour != null)
						field.Neighbours.Add(neighbour);
				}
			}
		}

		public override void UpdateLoop()
		{
			ChangeState<DummyState>();
		}
	}
}
