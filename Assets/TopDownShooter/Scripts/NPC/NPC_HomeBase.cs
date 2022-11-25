using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_HomeBase : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;


    [Header("WalkPoints")]
    public Vector3 walkPoint;
    public Vector3 ditanceToWalkPoint;
    public float distanceToWalkMagnitude;
    public bool walkPointSet;

    ExploreManager exp_Manager;
    public Transform[] movePoints;
    HomeBase homeBase;
    Animator anim;
    NavMeshAgent agent;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Rifle"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("Pistol"), 0);

        Patroling();
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        agent.SetDestination(walkPoint);

        anim.SetFloat("speed", agent.velocity.sqrMagnitude);

        ditanceToWalkPoint = transform.position - walkPoint;
        distanceToWalkMagnitude = ditanceToWalkPoint.magnitude;

        if (ditanceToWalkPoint.magnitude < 5f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        Transform tSpawn = movePoints[Random.Range(0, movePoints.Length)];
        walkPoint = new Vector3(tSpawn.position.x, transform.position.y, tSpawn.position.z);


        // if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }
}
