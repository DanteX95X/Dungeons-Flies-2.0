using System;
using UnityEngine;

namespace Assets.Scripts.Game.Map
{
	public class FreezeField : Field
	{
		protected override void ApplyEffect()
		{
			throw new NotImplementedException();
		}

		void Start()
		{
			type = FieldType.FREEZE;
		}
	}
}

