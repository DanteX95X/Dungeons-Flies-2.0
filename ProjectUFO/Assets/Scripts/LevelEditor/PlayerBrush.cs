using System;
using UnityEngine;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Actors;


namespace Assets.Scripts.LevelEditor
{
	public class PlayerBrush : Brush
	{
		protected override void UpdateLevel(GameObject newObject)
		{
			Player currentPlayer = Game.Game.Instance.CurrentLevel.ActivePlayer;
			if (currentPlayer)
				Destroy(currentPlayer.gameObject);

			Game.Game.Instance.CurrentLevel.ActivePlayer = newObject.GetComponent<Player>();
		}
	}
}

