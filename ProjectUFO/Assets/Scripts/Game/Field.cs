using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	public class Field : MonoBehaviour 
	{

		#region variables

		List<Field> neighbours = new List<Field>();

		#endregion

		#region properties

		public List<Field> Neighbours
		{
			get { return neighbours; }
			set { neighbours = value; }
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
