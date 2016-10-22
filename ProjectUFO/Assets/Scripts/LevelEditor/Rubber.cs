using System;
using UnityEngine;
using Assets.Scripts.Game.Map;
using Assets.Scripts.Game.Actors;

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
				foreach (GameObject unit in field.Units)
				{
					Player playerComponent = unit.GetComponent<Player>();
					Enemy enemyComponent = unit.GetComponent<Enemy>();
					if (playerComponent)
					{
						Game.Game.Instance.CurrentLevel.ActivePlayer = null;
						Debug.Log("Removed player from grid");
					}
					else if (enemyComponent)
					{
						Game.Game.Instance.CurrentLevel.Enemies.Remove(enemyComponent);
						Debug.Log("Removed enemy from grid");
					}

					Destroy(unit);
				}
				field.Units.Clear();

				Destroy(field.gameObject);
				Game.Game.Instance.CurrentLevel.Grid.Remove(newObject.transform.position);
			}
			Destroy(newObject);
		}
	}
}

