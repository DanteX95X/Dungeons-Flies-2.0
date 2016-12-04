using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.States
{
    class DummyState : State
    {
        public override void Init()
        {
			Debug.Log("Dummy State" + Game.Game.Instance.CurrentLevel.LevelName);
        }

        public override void UpdateLoop()
        {

        }
    }
}
