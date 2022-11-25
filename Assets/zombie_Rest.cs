using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_Rest : StateMachineBehaviour
{
    public float currentTime;
    public float maxWaitTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;
        if(currentTime >= maxWaitTime)
        {
            animator.SetTrigger("Scream");

            currentTime = 0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
