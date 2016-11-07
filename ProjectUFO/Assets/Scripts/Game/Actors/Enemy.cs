using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Game.Map;

namespace Assets.Scripts.Game.Actors
{
	public class Enemy : Actor
	{
		#region variables

		Movable movementController = null;

		Vector2 startingPosition;
		Vector2 targetPosition;

		#endregion

		#region methods

		void Start()
		{
			movementController = GetComponent<Movable>();
			startingPosition = transform.position;
			foreach( Field target in Game.Instance.CurrentLevel.Grid.Values)
			{
				targetPosition = target.transform.position;
				break;
			}

			Debug.Log("Moving towards " + targetPosition);
		}

		public override void ExecuteNextMove()
		{
			List<Field> path = Utilities.PathFinding.AStar(Game.Instance.CurrentLevel.Grid[transform.position], Game.Instance.CurrentLevel.Grid[targetPosition], Utilities.PathFinding.EmptyHeuristic);
			if (path.Count > 1)
			{
				Debug.Log(path[1].transform.position);
				movementController.StartMovement(path[1].transform.position);
			}
		}

		public override void RewindTime()
		{
			movementController.MoveToDestination(startingPosition);
		}

		#endregion

	}
}

