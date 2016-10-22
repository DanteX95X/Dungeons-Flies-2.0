using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.LevelEditor
{
	public class Brush : MonoBehaviour
	{
		#region variables

		[SerializeField]
		List<GameObject> colors = new List<GameObject>();

		[SerializeField]
		int depth = 0;

		int currentColorIndex;

		GameObject indicator = null;

		#endregion


		#region methods

		void Start()
		{
			currentColorIndex = 0;
			SpawnBrushIndicator();
		}

		void PreviousField()
		{
			currentColorIndex = (currentColorIndex - 1) % colors.Count;
			SpawnBrushIndicator();
		}

		void NextField()
		{
			currentColorIndex = (currentColorIndex + 1) % colors.Count;
			SpawnBrushIndicator();
		}
			
		protected void SpawnBrushIndicator()
		{
			if (indicator != null)
				Destroy(indicator);
			indicator = Instantiate(colors[currentColorIndex], new Vector3(transform.position.x, transform.position.y, depth), Quaternion.identity) as GameObject;
			indicator.transform.parent = transform;
		}

		public void Paint()
		{
			Instantiate(colors[currentColorIndex], indicator.transform.position, Quaternion.identity);
		}

		#endregion

	}
}

