using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	public class Field : MonoBehaviour 
	{

		#region variables

		Dictionary<Vector2, Field> neighbours = new Dictionary<Vector2, Field>();
		List<GameObject> units = new List<GameObject>();

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

		#endregion

		#region methods

		void Start ()
		{
		
		}

		void Update ()
		{
		
		}

		#endregion
	}
}
