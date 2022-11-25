using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Walk : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float attackRange;
    public float player_V_AttackRange;
    public float speed;
    Brute boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();
        boss = animator.GetComponent<Brute>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(boss.withinTarget)
       {
            agent.SetDestination(boss.target.transform.position);
            agent.speed = speed;
       }
       

       float distance = Vector3.Distance(boss.target.transform.position, agent.transform.position);

       if(player.GetComponent<Player>().inCar)
       {
          if(distance <= player_V_AttackRange)
         {
              animator.SetTrigger("Attack1");
         }
       }else
       {
          if(distance <= attackRange)
         {
              animator.SetTrigger("Attack1");
         }
       }

       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
       animator.ResetTrigger("Attack1");
    }

}
