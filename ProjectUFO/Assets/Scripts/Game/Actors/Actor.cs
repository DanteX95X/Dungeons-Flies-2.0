using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Actors
{
	public abstract class Actor : MonoBehaviour
	{
		public abstract void ExecuteNextMove();
		public abstract void RewindTime();
	}
}

