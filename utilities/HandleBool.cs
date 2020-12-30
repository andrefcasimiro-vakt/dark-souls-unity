using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBool : StateMachineBehaviour
{
    public string boolName = "canMove";
    public bool valueOnEnteringState = true;
    public bool valueOnExitingState = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, valueOnEnteringState);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, valueOnEnteringState);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, valueOnExitingState);
    }

}
