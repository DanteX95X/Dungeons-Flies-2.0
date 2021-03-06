﻿using System;
using UnityEngine;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Actors;
using Assets.Scripts.Game.Map;


namespace Assets.Scripts.LevelEditor
{
	public class PlayerBrush : Brush
	{
		protected override void UpdateLevel(GameObject newObject)
		{
			Field field = null;
			if (!Game.Game.Instance.CurrentLevel.Grid.TryGetValue(newObject.transform.position, out field))
			{
				Debug.Log("Player can be placed only on existing field");
				Destroy(newObject);
				return;
			}

			Player currentPlayer = Game.Game.Instance.CurrentLevel.ActivePlayer;
			if (currentPlayer)
				Destroy(currentPlayer.gameObject);

			Game.Game.Instance.CurrentLevel.ActivePlayer = newObject.GetComponent<Player>();
			field.Units.Add(newObject);
		}
	}
}

