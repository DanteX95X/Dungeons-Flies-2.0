using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Actors
{
	public abstract class Actor : MonoBehaviour
	{
		static List<Actor> allActors = new List<Actor>();

		void Start()
		{
			allActors.Add(this);
			Debug.Log(allActors.Count);
		}

		public abstract void ExecuteNextMove();
		public abstract void RewindTime();
	}
}

