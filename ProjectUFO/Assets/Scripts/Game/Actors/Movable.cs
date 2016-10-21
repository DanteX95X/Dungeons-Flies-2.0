using System;
using UnityEngine;

namespace Assets.Scripts.Game.Actors
{
	public class Movable : MonoBehaviour
	{
		#region variables

		Vector2 destination;
		Vector2 movementAxis;

		bool isMoving = false;

		[SerializeField]
		static float speed = 3;

		[SerializeField]
		static float proximityThreshold = 0.1f;

		#endregion

		#region properties

		public bool IsMoving
		{
			get { return isMoving; }
		}

		#endregion

		#region methods

		void Start()
		{
			transform.position = new Vector3( Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
			Field occupiedField = null;
			if (Level.Grid.TryGetValue(transform.position, out occupiedField))
			{
				occupiedField.Units.Add(gameObject);
			}
			else
			{
				Debug.Log("Could not find proper starting field");
				Destroy(gameObject);
			}

			destination = (Vector2)transform.position;
		}

		void Update()
		{
			if ((Vector2)transform.position != destination)
			{
				if (Mathf.Abs(destination.x - transform.position.x) < proximityThreshold && Mathf.Abs(destination.y - transform.position.y) < proximityThreshold)
				{
					transform.position = new Vector3(destination.x, destination.y, transform.position.z);
					Level.Grid[transform.position].Units.Add(gameObject);
					isMoving = false;
				}
				else
				{
					transform.position += (Vector3)movementAxis * speed * Time.deltaTime;
				}
			}
		}

		public void StartMovement(Vector2 destination)
		{
			Field dummy = null;
			if (isMoving || !Level.Grid.TryGetValue(destination, out dummy))
				return;

			isMoving = true;
			Level.Grid[transform.position].Units.Remove(gameObject);
			this.destination = destination;
			movementAxis = (destination - (Vector2)transform.position).normalized;
		}

		public void MoveToDestination(Vector2 destination)
		{
			Field dummy = null;
			if (isMoving || !Level.Grid.TryGetValue(destination, out dummy))
				return;

			Level.Grid[transform.position].Units.Remove(gameObject);
			transform.position = new Vector3(destination.x, destination.y, transform.position.z);
			Level.Grid[transform.position].Units.Add(gameObject);
			this.destination = transform.position;
		}

		#endregion
	}
}

