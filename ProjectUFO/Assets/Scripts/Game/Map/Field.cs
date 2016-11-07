using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game.Actors;

namespace Assets.Scripts.Game.Map
{
	public abstract class Field : MonoBehaviour 
	{

		#region variables

		Dictionary<Vector2, Field> neighbours = new Dictionary<Vector2, Field>();
		[SerializeField]
		List<GameObject> units = new List<GameObject>();

		protected FieldType type;

		#endregion

		#region properties

		public Dictionary<Vector2, Field> Neighbours
		{
			get { return neighbours; }
			set { neighbours = value; }
		}

		public List<GameObject> Units
		{
			get { return units; }
			set { units = value; }
		}

		public FieldType Type
		{
			get { return type; }
		}

		#endregion

		#region methods

		void Start ()
		{
		
		}

		void Update ()
		{
		
		}

		protected abstract void ApplyEffect();

		public bool ContainsUnitOfType<T>()
		{
			foreach (GameObject actor in units)
			{
				if (actor.GetComponent<T>() != null)
				{
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}
