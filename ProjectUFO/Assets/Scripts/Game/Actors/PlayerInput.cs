using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Actors
{
	public class PlayerInput : MonoBehaviour
	{
		#region variables

		static List<Vector2> displacements = new List<Vector2> { new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1)}; 

		Player playerController = null;
		Movable movementController = null;

		[SerializeField]
		GameObject playerPrefab = null;

		[SerializeField]
		Material ghostMaterial = null;

		#endregion


		#region methods

		void Start()
		{
			playerController = GetComponent<Player>();
			movementController = GetComponent<Movable>();
		}

		void Update()
		{
			if (!movementController.IsMoving)
			{
				Vector2 destination = HandleInput(); 
				Field dummy = null;

				if (destination != (Vector2)transform.position && Level.Grid.TryGetValue(destination, out dummy))
				{
					playerController.Move(destination);
					foreach (Actor actor in FindObjectsOfType<Actor>())
						actor.ExecuteNextMove();
				}
			}
		}


		Vector2 HandleInput()
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
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				foreach (Actor actor in FindObjectsOfType<Actor>())
					actor.RewindTime();

				Instantiate(playerPrefab, transform.position, Quaternion.identity);
				destination = transform.position;

				GetComponent<Renderer>().material = ghostMaterial;
				Destroy(this);
			}

			return destination;
		}

		#endregion
	}
}

