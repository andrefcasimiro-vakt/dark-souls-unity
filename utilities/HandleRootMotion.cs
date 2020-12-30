using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRootMotion : StateMachineBehaviour
{
    public bool rootMotionEnabled = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = rootMotionEnabled;    
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = !rootMotionEnabled;
    }

}
