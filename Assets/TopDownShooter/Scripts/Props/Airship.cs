using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Airship : MonoBehaviour
{
    public float speed;
    public float Gravity;
    [Header("WalkPoints")]
    public Vector3 walkPoint;
    public Vector3 ditanceToWalkPoint;
    public float distanceToWalkMagnitude;
    public bool walkPointSet;
    public bool homeBaseAirship;
    public Transform seatPos;
    public Transform outPos;

    [Header("GFX")]
    public GameObject camera;
    public GameObject canvas;

    [Header("UI")]
    public FixedJoystick joystick;

    NavMeshAgent agent;
    ExploreManager exp_Manager;
    CharacterController controller;
    HomeBase homeBase;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera.SetActive(false);
        controller = GetComponent<CharacterController>();

        if (!homeBaseAirship)
        {
            exp_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();
        }

        if(homeBaseAirship)
        {
            homeBase = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();
        }

        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 moveDir = new Vector3(x, 0, z);
        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        controller.Move(moveDir * speed * Time.deltaTime);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            Vector3 lookDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }


    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        agent.SetDestination(walkPoint);

        ditanceToWalkPoint = transform.position - walkPoint;
        distanceToWalkMagnitude = ditanceToWalkPoint.magnitude;

        if (ditanceToWalkPoint.magnitude < 5f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        if (!homeBaseAirship)
        {
            Transform tSpawn = exp_Manager.wanderPositions[Random.Range(0, exp_Manager.wanderPositions.Length)];
            walkPoint = new Vector3(tSpawn.position.x, transform.position.y, tSpawn.position.z);
        }
        else if (homeBaseAirship)
        {
            Transform tSpawn = homeBase.airshipPos[Random.Range(0, homeBase.airshipPos.Length)];
            walkPoint = new Vector3(tSpawn.position.x, transform.position.y, tSpawn.position.z);
        }


        // if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }
}
