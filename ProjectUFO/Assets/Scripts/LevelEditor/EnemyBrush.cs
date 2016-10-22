using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game.Actors;
using Assets.Scripts.Game;

namespace Assets.Scripts.LevelEditor
{
	public class EnemyBrush : Brush
	{
		protected override void UpdateLevel(GameObject newObject)
		{
			Field field = null;
			if (!Game.Game.Instance.CurrentLevel.Grid.TryGetValue(newObject.transform.position, out field))
			{
				Debug.Log("Enemy can be placed only on existing field");
				Destroy(newObject);
				return;
			}


			Game.Game.Instance.CurrentLevel.Enemies.Add(newObject.GetComponent<Enemy>());
			field.Units.Add(newObject);

			foreach (var ufo in field.Units)
			{
				Debug.Log(ufo.name);
			}
		}
	}
}

