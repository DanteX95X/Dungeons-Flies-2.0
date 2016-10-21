using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	public class Player : MonoBehaviour
	{
		Movable movementController = null;

		static List<Vector3> displacements = new List<Vector3> { new Vector3(-1,0), new Vector3(1,0), new Vector3(0,-1), new Vector3(0,1)}; 

		void Start()
		{
			movementController = GetComponent<Movable>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				movementController.StartMovement(transform.position + displacements[0]);
			if (Input.GetKeyDown(KeyCode.RightArrow))
				movementController.StartMovement(transform.position + displacements[1]);
			if (Input.GetKeyDown(KeyCode.DownArrow))
				movementController.StartMovement(transform.position + displacements[2]);
			if (Input.GetKeyDown(KeyCode.UpArrow))
				movementController.StartMovement(transform.position + displacements[3]);
			
		}
	}
}

