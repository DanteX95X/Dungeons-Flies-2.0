using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.LevelEditor
{
	public abstract class Brush : MonoBehaviour
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

		void PreviousColor()
		{
			currentColorIndex = (currentColorIndex - 1) % colors.Count;
			SpawnBrushIndicator();
		}

		void NextColor()
		{
			currentColorIndex = (currentColorIndex + 1) % colors.Count;
			SpawnBrushIndicator();
		}
			
		public void SpawnBrushIndicator()
		{
			if (indicator != null)
				Destroy(indicator);
			indicator = Instantiate(colors[currentColorIndex], new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity) as GameObject;
			indicator.transform.parent = transform;
		}

		public void DestroyIndicator()
		{
			if (indicator != null)
				Destroy(indicator);
		}

		public void Paint()
		{
			GameObject paint = Instantiate(colors[currentColorIndex], new Vector3(indicator.transform.position.x, indicator.transform.position.y, depth), Quaternion.identity) as GameObject;
			UpdateLevel(paint);
		}

		protected abstract void UpdateLevel(GameObject newObject);

		#endregion

	}
}

