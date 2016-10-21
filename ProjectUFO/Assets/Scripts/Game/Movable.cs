using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class Movable : MonoBehaviour
	{
		#region variables

		Vector2 destination;
		Vector2 movementAxis;

		[SerializeField]
		static float speed = 3;

		[SerializeField]
		static float proximityThreshold = 0.1f;

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
			StartMovement(new Vector2(0,1));
		}

		void Update()
		{
			if ((Vector2)transform.position != destination)
			{
				if (Mathf.Abs(destination.x - transform.position.x) < proximityThreshold && Mathf.Abs(destination.y - transform.position.y) < proximityThreshold)
				{
					transform.position = new Vector3(destination.x, destination.y, transform.position.z);
					Level.Grid[transform.position].Units.Add(gameObject);
				}
				else
				{
					transform.position += (Vector3)movementAxis * speed * Time.deltaTime;
				}
			}
		}

		public void StartMovement(Vector2 destination)
		{
			Level.Grid[transform.position].Units.Remove(gameObject);
			this.destination = destination;
			movementAxis = (destination - (Vector2)transform.position).normalized;
		}

		#endregion
	}
}

