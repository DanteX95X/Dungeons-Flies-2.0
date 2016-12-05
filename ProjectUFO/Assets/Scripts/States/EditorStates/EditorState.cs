using System;
using UnityEngine;

namespace Assets.Scripts.States
{
	public class EditorState : State
	{
		#region variables

		[SerializeField]
		GameObject brushObject = null;

		#endregion


		#region methods

		public override void Init()
		{
			brushObject.SetActive(true);
		}

		public override void UpdateLoop()
		{
			if(Input.GetKeyDown(KeyCode.Return))
				ChangeState<SaveLevelState>();
		}

		public override void CleanUp()
		{
			if(brushObject != null)
				brushObject.SetActive(false);
		}

		#endregion
	}
}

