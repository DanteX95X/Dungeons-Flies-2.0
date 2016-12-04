using UnityEngine;

namespace Assets.Scripts.States
{
	public abstract class State : MonoBehaviour
	{
		#region variables

		public static State currentState = null;

		#endregion


		#region methods

		public void Start()
		{
			currentState = this;
			Init();
		}

		public void Update()
		{
			UpdateLoop();

			/*if(currentState != this)
			{
				Destroy(this);
			}*/
		}

		public abstract void Init();

		public abstract void UpdateLoop();

		public void ChangeState<T>() where T : State
		{
			currentState = GetComponent<T>();
			currentState.enabled = true;
			enabled = false;
			//Destroy(this);
			//gameObject.AddComponent<T>();
		}

		#endregion
	}
}
