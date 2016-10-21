using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Actors
{
	public class Player : Actor
	{
		#region variables

		Movable movementController = null;

		List<Vector2> path = null;
		int pathSegmentsPassed = 0;

		#endregion


		#region methods

		void Start()
		{
			movementController = GetComponent<Movable>();

			path = new List<Vector2>();

			path.Add(Level.Grid[transform.position].transform.position);
			++pathSegmentsPassed;

		}

		public void Move(Vector2 destination)
		{
			movementController.StartMovement(destination);

			if (pathSegmentsPassed == path.Count)
				path.Add(destination);
			++pathSegmentsPassed;
		}

		public override void ExecuteNextMove()
		{
			if (path.Count == pathSegmentsPassed)
				return;
			
			Vector2 destination = path[pathSegmentsPassed];
			Move(destination);
		}
			

		public override void RewindTime()
		{
			movementController.MoveToDestination(path[0]);
			pathSegmentsPassed = 1;

		}

		#endregion
	}
}

