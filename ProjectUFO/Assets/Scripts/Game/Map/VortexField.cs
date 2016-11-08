using System;
using UnityEngine;

namespace Assets.Scripts.Game.Map
{
	public class VortexField : Field
	{
		protected override void ApplyEffect()
		{
			throw new NotImplementedException();
		}

		void Start()
		{
			type = FieldType.VORTEX;
		}
	}
}