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
            Debug.Log("Dummy State");
        }

        public override void UpdateLoop()
        {

        }
    }
}
