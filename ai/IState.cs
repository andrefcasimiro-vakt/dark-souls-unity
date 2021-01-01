using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }

}
