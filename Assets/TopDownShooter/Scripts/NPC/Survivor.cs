using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Survivor : MonoBehaviour
{
    [Header("Data")]
    public SRV_Box srvBox;
    public int srvIndex;
    public Player player;
    public int index;
    public Zombie zombie;
    [Space]
    public GameObject[] skins;
    public GameObject[] weapons;
    [Space]
    public bool ishomebase;

    [Header("Stats")]
    public Animator[] anim;
    public bool withinEnemy;
    public bool dead;
    public bool withinPlayer;
    public bool canFire;
    public bool withinMelee;
    public bool melee;
    public float distanceToWalkMagnitude;
    [Space]
    public float weaponCheckRadius;
    public float playerCheckRadius;
    public float withinMeleeRadius;

    [Header("Melee")]
    public Transform atkPos;
    public float attackRate;
    public float radius;
    public LayerMask TargetLayer;
    public LayerMask playerLayer;
    public float damage;
    float nextAttackTime = 0f;

    [Header("GFX")]
    public GameObject GunOBJ;
    public GameObject MeleeOBJ;

    [Header("SFX")]
    public AudioClip[] swingSFX;

    [Header("Patrol")]
    public Transform[] movePoints;
    public bool walkPointSet;
    public Vector3 walkPoint;
    public Vector3 ditanceToWalkPoint;
    public float fleeDist;

    PlayfabManager database;
    HomeBase homeBase;
    float distance;
    NavMeshAgent agent;
    SurvivalShop shop;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();

        if (ishomebase)
            homeBase = FindObjectOfType<HomeBase>();

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shop = FindObjectOfType<SurvivalShop>();

        shop.EnableSRV(srvIndex);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            ishomebase = true;
        }else
        {
            ishomebase = false;
        }

        srvBox = database.srvs_Boxes[index];

        Switch();

    }

    // Update is called once per frame
    void Update()
    {
        if (ishomebase)
        {
            movePoints = homeBase.campFirePos;
            Patroling();
        }

        withinEnemy = Physics.CheckSphere(transform.position, weaponCheckRadius, TargetLayer);

        withinPlayer = Physics.CheckSphere(transform.position, playerCheckRadius, playerLayer);

        withinMelee = Physics.CheckSphere(transform.position, withinMeleeRadius, TargetLayer);

        //--------------------------------------------------Follow Player---------------------------------------//
        if (!ishomebase && !withinEnemy)
        {
            if (!withinPlayer)
            {
                if (player.inCar || player.inAirship || player.inHelicopter)
                {
                    transform.position = player.transform.position;

                    canFire = false;

                    DisableSkin();

                    agent.enabled = false;
                }
                else
                {

                    agent.enabled = true;
                    agent.SetDestination(player.transform.position);

                    

                    StartCoroutine(EnableSkin(0.1f));

                    //Debug.Log("Chasing");
                }
            }
        }

        SetFloat("speed", agent.velocity.sqrMagnitude);

        if (withinEnemy)
        {
            FindClosesteEnemy();

            agent.SetDestination(transform.position);

            distance = Vector3.Distance(transform.position, zombie.transform.position);


            if (withinMelee)
            {
                GunOBJ.SetActive(false);
                MeleeOBJ.SetActive(true);

                canFire = false;

                if (melee && Time.time >= nextAttackTime)
                {
                    int rand = Random.Range(0, 2);

                    if (rand == 0)
                    {
                        SetTrigger("attack1");
                    }
                    else
                    {
                        SetTrigger("attack2");
                    }

                    nextAttackTime = Time.time + 1f / attackRate;
                }

                if (distance < fleeDist)
                {
                    Vector3 DistToEnemy = transform.position - zombie.transform.position;

                    Vector3 newPos = transform.position + DistToEnemy;

                    SetFloat("speed", -1f);

                    agent.SetDestination(newPos);
                }
            }
            else
            {
                GunOBJ.SetActive(true);
                MeleeOBJ.SetActive(false);

                if (!player.inCar || player.inAirship || player.inHelicopter)
                    canFire = true;
            }
        }else
        {
            canFire = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(atkPos.position, weaponCheckRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
    }

    public void Attack()
    {
        Collider[] enemyCol = Physics.OverlapSphere(atkPos.position, radius, TargetLayer);

        audio.PlayOneShot(swingSFX[Random.Range(0, swingSFX.Length)]);

        foreach (Collider t in enemyCol)
        {
            Zombie_BP z = t.GetComponent<Zombie_BP>();
            if (z != null)
            {
                z.TakeDamage(damage);
            }

            Bandit_BP b = t.GetComponent<Bandit_BP>();
            if (b != null)
            {
                b.TakeDamage(damage);
            }

            Cannibal cannibal = t.GetComponent<Cannibal>();
            if (cannibal != null)
            {
                cannibal.TakeDamage(damage);
            }

        }
    }

    public void Switch()
    {
        SwitchSkin(srvBox.clothesIndex);
        SwitchWeapon(srvBox.weaponIndex);
    }

    public void SwitchSkin(int index)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }

        skins[index].SetActive(true);
    }

    public void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[index].SetActive(true);
    }

    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        agent.SetDestination(walkPoint);
        SetFloat("speed", agent.velocity.sqrMagnitude);

        ditanceToWalkPoint = transform.position - walkPoint;
        distanceToWalkMagnitude = ditanceToWalkPoint.magnitude;

        if (ditanceToWalkPoint.magnitude < 12f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        Transform tSpawn = movePoints[Random.Range(0, movePoints.Length)];

        walkPoint = new Vector3(tSpawn.position.x, tSpawn.position.y, tSpawn.position.z);

        walkPointSet = true;
    }
    void FindClosesteEnemy()
    {
        float distanceToClosesteEnemy = Mathf.Infinity;
        zombie = null;

        Zombie[] allZombies = GameObject.FindObjectsOfType<Zombie>();

        foreach (Zombie currentZombie in allZombies)
        {
            float distToEnemy = (currentZombie.transform.position - this.transform.position).sqrMagnitude;
            if (distToEnemy < distanceToClosesteEnemy)
            {
                distanceToClosesteEnemy = distToEnemy;
                zombie = currentZombie;
            }
        }

        var targetT = zombie.transform.position;
        targetT.y = transform.position.y;
        transform.LookAt(targetT);
    }

    public void DisableSkin()
    {
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();


        foreach (SkinnedMeshRenderer c in skin)
        {
            c.enabled = false;
        }

        foreach (MeshRenderer m in mesh)
        {
            m.enabled = false;
        }
    }

    public IEnumerator EnableSkin(float delay)
    {
        yield return new WaitForSeconds(delay);

        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        Collider[] col = GetComponentsInChildren<Collider>();

        foreach (SkinnedMeshRenderer c in skin)
        {
            c.enabled = true;
        }

        foreach (MeshRenderer m in mesh)
        {
            m.enabled = true;
        }
    }

    public void SetFloat(string name, float value)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetFloat(name, value);
        }
    }

    public void SetBool(string name, bool value)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool(name, value);
        }
    }

    public void SetTrigger(string name)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetTrigger(name);
        }
    }

    public void SetLayer(string name, int value)
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetLayerWeight(anim[i].GetLayerIndex(name), value);
        }
    }
}
