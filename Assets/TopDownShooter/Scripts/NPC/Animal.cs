using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Animal : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Transform dropPos;
    public GameObject chicken;

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

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            homeBase = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();

            movePoints = homeBase.animalPos;
        }
        else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Game"))
        {
            exp_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();

            movePoints = exp_Manager.animalPositions;
        }



        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;

        

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

    public void TakeDamage(float amount)
    {
        if (dead) return;

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            anim.SetTrigger("dead");

            Instantiate(chicken, dropPos.position, dropPos.rotation);

            dead = true;

            agent.enabled = false;
        }
    }
}
