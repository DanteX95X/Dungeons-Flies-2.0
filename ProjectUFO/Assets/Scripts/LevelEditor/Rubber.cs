using System;
using UnityEngine;
using Assets.Scripts.Game.Map;

namespace Assets.Scripts.LevelEditor
{
	public class Rubber : Brush
	{
		protected override void UpdateLevel(GameObject newObject)
		{
			Field field = null;

			Game.Game.Instance.CurrentLevel.Grid.TryGetValue(newObject.transform.position, out field);

			if (field != null)
			{
				Destroy(field.gameObject);
				Game.Game.Instance.CurrentLevel.Grid.Remove(newObject.transform.position);
			}
			Destroy(newObject);
		}
	}
}

