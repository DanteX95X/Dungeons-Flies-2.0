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

		[SerializeField]
		GameObject enemy = null;

		public override void Init()
		{
			Game.Game.Instance.CurrentLevel.Grid.Clear();
			Game.Game.Instance.CurrentLevel.Grid = new Dictionary<Vector2, Field>();

			LevelInfo info = new LevelInfo(Game.Game.Instance.LevelPath);
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

			Vector2 maxCoordinates = new Vector2(float.MinValue, float.MinValue);
			Vector2 minCoordinates = new Vector2(float.MaxValue, float.MaxValue);

			foreach (var fieldInfo in level.Grid)
			{
				GameObject tile = Instantiate(field, fieldInfo.first, Quaternion.identity) as GameObject;
				tile.transform.parent = grid.transform;
				Game.Game.Instance.CurrentLevel.Grid[tile.transform.position] = tile.GetComponent<Field>();

				if (tile.transform.position.x < minCoordinates.x)
					minCoordinates.x = tile.transform.position.x;
				if (tile.transform.position.x > maxCoordinates.x)
					maxCoordinates.x = tile.transform.position.x;
				if (tile.transform.position.y < minCoordinates.y)
					minCoordinates.y = tile.transform.position.y;
				if (tile.transform.position.y > maxCoordinates.y)
					maxCoordinates.y = tile.transform.position.y;
			}

			Debug.Log(maxCoordinates + " " + minCoordinates);

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

			GameObject enemiesParent = new GameObject();
			enemiesParent.name = "enemies";
			foreach (Vector3 position in level.EnemiesPositions)
			{
				GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity) as GameObject;
				Game.Game.Instance.CurrentLevel.Enemies.Add(newEnemy.GetComponent<Enemy>());
				newEnemy.transform.parent = enemiesParent.transform;
			}

			SetCamera(new Vector2[] { maxCoordinates, minCoordinates});
		}

		void SetCamera(Vector2[] positions)
		{
			Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
			Vector3 desiredPosition = new Vector3((positions[0].x + positions[1].x)/2, (positions[0].y + positions[1].y)/2, -10);
			mainCamera.transform.position = desiredPosition;


			Vector3 desiredLocalPosition = mainCamera.transform.InverseTransformPoint(desiredPosition);
			float size = 0;
			foreach (Vector3 position in positions)
			{
				Vector3 targetLocalPosition = mainCamera.transform.InverseTransformPoint(position);
				Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

				size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.y));
				size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / mainCamera.aspect);
			}
			size += 1;
			mainCamera.orthographicSize = size;
		}
	}
}
