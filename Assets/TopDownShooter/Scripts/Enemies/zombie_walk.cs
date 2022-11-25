using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie_walk : StateMachineBehaviour
{
    [Header("Variety")]
    public bool Normal;
    public bool Explode;
    public bool Jump;
    public bool Scream;
    Transform player;
    NavMeshAgent agent;

    [Header("Scream")]
    public float screamRange;

    [Header("Jump")]
    public float jumpDist;

    [Header("Explode")]
    public float throwRange;
    public float explodeRange;

    [Header("Normal")]
    public float attackRange;
    public float stoppingDist_V;
    public float stoppingDist;
    public float attackRate;

    float nextTimeToATK = 0f;

    float distance;
    float defaultRange;
    Zombie zombie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();
       zombie = animator.GetComponent<Zombie>();

       defaultRange = attackRange;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!zombie.homeBase)
        {
            if (!zombie.withinTarget)
            {
                //distance = Vector3.Distance(player.position, agent.transform.position);
                agent.SetDestination(zombie.walkPoint);
            }
            else if (zombie.withinTarget)
            {
                distance = Vector3.Distance(zombie.target.transform.position, agent.transform.position);
                agent.SetDestination(zombie.target.transform.position);

                if(Normal)
                {
                    if (distance <= attackRange && Time.time >= nextTimeToATK)
                    {
                        int Rand = Random.Range(0, 3);

                        if (Rand == 0)
                        {
                            animator.SetTrigger("Attack");
                        }

                        if (Rand == 1)
                        {
                            animator.SetTrigger("Attack2");
                        }

                        if (Rand == 2)
                        {
                            animator.SetTrigger("Attack3");
                        }

                        nextTimeToATK = Time.time + 1f / attackRate;
                    }
                }

                if(Explode)
                {
                    if(distance <= throwRange && distance > attackRange && Time.time >= nextTimeToATK)
                    {
                        animator.SetTrigger("Throw");

                        nextTimeToATK = Time.time + 1f / attackRate;
                    }

                    if (distance <= attackRange && Time.time >= nextTimeToATK)
                    {
                        int Rand = Random.Range(0, 3);

                        if (Rand == 0)
                        {
                            animator.SetTrigger("Attack");
                        }

                        if (Rand == 1)
                        {
                            animator.SetTrigger("Attack2");
                        }

                        if (Rand == 2)
                        {
                            animator.SetTrigger("Attack3");
                        }

                        nextTimeToATK = Time.time + 1f / attackRate;
                    }
                }

                if (Jump)
                {
                    if (distance <= jumpDist && !player.GetComponent<Player>().OnZombie && zombie.target.player) 
                    {
                        if(!player.GetComponent<Player>().inCar && !player.GetComponent<Player>().inHelicopter)
                            animator.SetTrigger("Jump");
                        else
                        {
                            int Rand = Random.Range(0, 3);

                            if (Rand == 0)
                            {
                                animator.SetTrigger("Attack");
                            }

                            if (Rand == 1)
                            {
                                animator.SetTrigger("Attack2");
                            }

                            if (Rand == 2)
                            {
                                animator.SetTrigger("Attack3");
                            }
                        }
                    }

                    if(distance <= attackRange && !player.GetComponent<Player>().OnZombie && Time.time >= nextTimeToATK)
                    {
                        int Rand = Random.Range(0, 3);

                        if (Rand == 0)
                        {
                            animator.SetTrigger("Attack");
                        }

                        if (Rand == 1)
                        {
                            animator.SetTrigger("Attack2");
                        }

                        if (Rand == 2)
                        {
                            animator.SetTrigger("Attack3");
                        }

                        nextTimeToATK = Time.time + 1f / attackRate;
                    }

                }

                if (Scream)
                {
                    if (distance <= screamRange && Time.time >= nextTimeToATK && !player.GetComponent<Player>().inCar && !player.GetComponent<Player>().inAirship && !player.GetComponent<Player>().inHelicopter)
                    {
                        animator.SetTrigger("Scream");

                        nextTimeToATK = Time.time + 1f / attackRate;
                    }
                }

            }

            if (player.GetComponent<Player>().inCar)
            {
                attackRange = stoppingDist_V;
            }
            else
            {
                attackRange = defaultRange;
            }

        }else
        {
            distance = Vector3.Distance(zombie.target.transform.position, agent.transform.position);
            agent.SetDestination(zombie.target.transform.position);

            if (distance <= attackRange && Time.time >= nextTimeToATK)
            {
                int Rand = Random.Range(0, 3);

                if (Rand == 0)
                {
                    animator.SetTrigger("Attack");
                }

                if (Rand == 1)
                {
                    animator.SetTrigger("Attack2");
                }

                if (Rand == 2)
                {
                    animator.SetTrigger("Attack3");
                }

                nextTimeToATK = Time.time + 1f / attackRate;
            }

            if (player.GetComponent<Player>().inCar)
            {
                attackRange = stoppingDist_V;
            }
            else
            {
                attackRange = defaultRange;
            }
        }

       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
       animator.ResetTrigger("Attack2");
       animator.ResetTrigger("Attack3");
       animator.ResetTrigger("Throw");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
