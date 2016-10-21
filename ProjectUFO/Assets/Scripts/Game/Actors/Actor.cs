using System;
using UnityEngine;

namespace Assets.Scripts.Game.Actors
{
	public abstract class Actor : MonoBehaviour
	{
		public abstract void ExecuteNextMove();
		public abstract void RewindTime();
	}
}

