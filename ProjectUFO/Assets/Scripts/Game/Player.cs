using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	public class Player : MonoBehaviour
	{
		Movable movementController = null;

		static List<Vector2> displacements = new List<Vector2> { new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1)}; 

		void Start()
		{
			movementController = GetComponent<Movable>();
		}

		void Update()
		{
			if (!movementController.IsMoving)
			{
				Vector2 destination = transform.position;
				if (Input.GetKeyDown(KeyCode.LeftArrow))
					destination += displacements[0];
				else if (Input.GetKeyDown(KeyCode.RightArrow))
					destination += displacements[1];
				else if (Input.GetKeyDown(KeyCode.DownArrow))
					destination += displacements[2];
				else if (Input.GetKeyDown(KeyCode.UpArrow))
					destination += displacements[3];

				if (destination != (Vector2)transform.position)
					movementController.StartMovement(destination);
			}
		}
	}
}

