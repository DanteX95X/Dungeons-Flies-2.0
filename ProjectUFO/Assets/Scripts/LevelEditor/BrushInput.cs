using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Scripts.LevelEditor
{
	public class BrushInput : MonoBehaviour
	{
		#region variables

		[SerializeField]
		Camera mainCamera = null;

		static List<Vector3> displacements = new List<Vector3> { new Vector3(-1,0), new Vector3(1,0), new Vector3(0,-1), new Vector3(0,1) };

		List<Brush> brushes;
		int currentBrushIndex = 0;

		#endregion

		#region methods

		void Start()
		{
			brushes = new List<Brush>(GetComponents<Brush>());
			currentBrushIndex = 0;
			brushes[currentBrushIndex].SpawnBrushIndicator();
			mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				Translate(displacements[0]);
			else if (Input.GetKeyDown(KeyCode.RightArrow))
				Translate(displacements[1]);
			else if (Input.GetKeyDown (KeyCode.DownArrow))
				Translate(displacements [2]);
			else if (Input.GetKeyDown (KeyCode.UpArrow))
				Translate(displacements [3]);
			else if (Input.GetKeyDown (KeyCode.Space))
				brushes [currentBrushIndex].Paint ();
			else if (Input.GetKeyDown (KeyCode.Tab))
				NextBrush ();
			else if (Input.GetKeyDown (KeyCode.Q))
				NextColor ();
			//else if (Input.GetKeyDown(KeyCode.Return))
				//SaveLevel();
				//ShowInputField();
		}

		void Translate(Vector3 displacement)
		{
			transform.position += displacement;
			mainCamera.transform.position += displacement;
		}

		void NextColor()
		{
			brushes[currentBrushIndex].DestroyIndicator();
			brushes [currentBrushIndex].NextColor ();
		}

		void NextBrush()
		{
			brushes[currentBrushIndex].DestroyIndicator();
			currentBrushIndex = (currentBrushIndex + 1) % brushes.Count;
			brushes[currentBrushIndex].SpawnBrushIndicator();
		}

		#endregion
	}
}

