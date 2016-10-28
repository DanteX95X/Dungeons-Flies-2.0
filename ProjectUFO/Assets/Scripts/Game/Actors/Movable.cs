using System;
using UnityEngine;
using Assets.Scripts.Game.Map;

namespace Assets.Scripts.Game.Actors
{
	public class Movable : MonoBehaviour
	{
		#region variables

		Vector2 destination;
		Vector2 movementAxis;

		Quaternion rotation;

		bool isMoving = false;

		[SerializeField]
		static float speed = 3;

		[SerializeField]
		static float proximityThreshold = 0.1f;

		[SerializeField]
		static float rotationSpeed = 300;

		[SerializeField]
		static float rotationProximityTreshold = 5;

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
			if (Game.Instance.CurrentLevel.Grid.TryGetValue(transform.position, out occupiedField))
			{
				occupiedField.Units.Add(gameObject);
			}
			else
			{
				Debug.Log("Could not find proper starting field");
				Destroy(this);
			}

			destination = (Vector2)transform.position;
			rotation = (Quaternion)transform.rotation;
		}

		void Update()
		{
			if ((Vector2)transform.position != destination)
			{
				if (Mathf.Abs(destination.x - transform.position.x) < proximityThreshold && Mathf.Abs(destination.y - transform.position.y) < proximityThreshold)
				{
					transform.position = new Vector3(destination.x, destination.y, transform.position.z);
					Game.Instance.CurrentLevel.Grid[transform.position].Units.Add(gameObject);
					isMoving = false;
				}
				else
				{
					transform.position += (Vector3)movementAxis * speed * Time.deltaTime;
				}
			}

			if (rotation != transform.rotation)
			{
				if(Mathf.Abs(rotation.eulerAngles.z - transform.rotation.eulerAngles.z) < rotationProximityTreshold)
				{
					transform.rotation = rotation;
				}
				else
				{
					float step = rotationSpeed * Time.deltaTime;
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);
				}
			}
		}

		public void StartMovement(Vector2 destination)
		{
			Field dummy = null;
			if (isMoving || !Game.Instance.CurrentLevel.Grid.TryGetValue(destination, out dummy))
				return;

			isMoving = true;
			Game.Instance.CurrentLevel.Grid[transform.position].Units.Remove(gameObject);
			this.destination = destination;
			movementAxis = (destination - (Vector2)transform.position).normalized;

			if (movementAxis.x == 1)
				rotation = Quaternion.Euler(0,0,0);
			if (movementAxis.x == -1)
				rotation = Quaternion.Euler(0,0,180);
			if (movementAxis.y == 1)
				rotation = Quaternion.Euler(0,0,90);
			if (movementAxis.y == -1)
				rotation = Quaternion.Euler(0,0,270);
		}

		public void MoveToDestination(Vector2 destination)
		{
			Field dummy = null;
			if (isMoving || !Game.Instance.CurrentLevel.Grid.TryGetValue(destination, out dummy))
				return;

			Game.Instance.CurrentLevel.Grid[transform.position].Units.Remove(gameObject);
			transform.position = new Vector3(destination.x, destination.y, transform.position.z);
			Game.Instance.CurrentLevel.Grid[transform.position].Units.Add(gameObject);
			this.destination = transform.position;
		}

		#endregion
	}
}

