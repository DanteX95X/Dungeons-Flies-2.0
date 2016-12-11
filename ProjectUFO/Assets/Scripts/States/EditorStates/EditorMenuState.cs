using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.States
{
	public class EditorMenuState : State
	{
		#region variables

		[SerializeField]
		Button newLevelButton = null;

		[SerializeField]
		Button loadLevelButton = null;

		#endregion



		#region methods

		public override void Init()
		{
			newLevelButton.gameObject.SetActive(true);
			loadLevelButton.gameObject.SetActive(true);
		}

		public override void UpdateLoop()
		{
		}

		public override void CleanUp()
		{
			newLevelButton.gameObject.SetActive(false);
			loadLevelButton.gameObject.SetActive(false);
		}

		public void LoadLevel()
		{
			ChangeState<LoadLevelState>();
		}

		public void NewLevel()
		{
			ChangeState<EditorState>();
		}

		#endregion
	}
}

