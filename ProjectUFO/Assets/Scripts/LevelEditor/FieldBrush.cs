using System;
using UnityEngine;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Map;

namespace Assets.Scripts.LevelEditor
{
	public class FieldBrush : Brush
	{
		protected override void UpdateLevel(GameObject newObject)
		{
			Field previousField = null;
			Game.Game.Instance.CurrentLevel.Grid.TryGetValue(newObject.transform.position, out previousField);
			Game.Game.Instance.CurrentLevel.Grid[newObject.transform.position] = newObject.GetComponent<Field>();

			if (previousField != null)
			{
				newObject.GetComponent<Field>().Units = previousField.Units;
				Destroy(previousField.gameObject);
			}
		}
	}
}

