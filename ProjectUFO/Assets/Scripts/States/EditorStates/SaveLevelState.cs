using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.LevelEditor;

namespace Assets.Scripts.States
{
	public class SaveLevelState : State
	{
		#region variables

		[SerializeField]
		InputField pathInput = null;
		[SerializeField]
		Button cancelButton = null;
		[SerializeField]
		Button saveButton = null;


		#endregion


		#region methods

		public override void Init()
		{
			pathInput.gameObject.SetActive(true);
			cancelButton.gameObject.SetActive(true);
			saveButton.gameObject.SetActive(true);
		}

		public override void UpdateLoop()
		{
			
		}

		public override void CleanUp ()
		{
			pathInput.gameObject.SetActive(false);
			cancelButton.gameObject.SetActive(false);
			saveButton.gameObject.SetActive(false);
		}

		public void HideGUI()
		{
			ChangeState<EditorState>();
		}

		public void SaveLevelToFile()
		{
			Game.Game.Instance.CurrentLevel.LevelName = pathInput.text;
			System.IO.File.WriteAllText(pathInput.text + ".level", (new LevelInfo(Game.Game.Instance.CurrentLevel)).ToString());
			ChangeState<EditorState>();
			Debug.Log("Level saved");
		}

		#endregion
	}
}

