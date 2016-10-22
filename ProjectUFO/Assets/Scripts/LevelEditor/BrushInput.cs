using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.LevelEditor
{
	public class BrushInput : MonoBehaviour
	{
		static List<Vector3> displacements = new List<Vector3> { new Vector3(-1,0), new Vector3(1,0), new Vector3(0,-1), new Vector3(0,1) };

		List<Brush> brushes;
		int currentBrushIndex = 0;

		void Start()
		{
			brushes = new List<Brush>(GetComponents<Brush>());
			currentBrushIndex = 0;
			brushes[currentBrushIndex].SpawnBrushIndicator();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				transform.position += displacements[0];
			else if (Input.GetKeyDown(KeyCode.RightArrow))
				transform.position += displacements[1];
			else if (Input.GetKeyDown(KeyCode.DownArrow))
				transform.position += displacements[2];
			else if (Input.GetKeyDown(KeyCode.UpArrow))
				transform.position += displacements[3];
			else if (Input.GetKeyDown(KeyCode.Space))
				brushes[currentBrushIndex].Paint();
			else if (Input.GetKeyDown(KeyCode.Tab))
				NextBrush();
			else if (Input.GetKeyDown(KeyCode.Return))
				SaveLevel();
		}

		void NextBrush()
		{
			brushes[currentBrushIndex].DestroyIndicator();
			currentBrushIndex = (currentBrushIndex + 1) % brushes.Count;
			brushes[currentBrushIndex].SpawnBrushIndicator();
		}

		void SaveLevel()
		{
			System.IO.File.WriteAllText(Game.Game.Instance.LevelPath, (new LevelInfo(Game.Game.Instance.CurrentLevel)).ToString());
			Debug.Log("Level saved");
		}
	}
}

