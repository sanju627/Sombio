using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cannibal : MonoBehaviour
{
    public bool MainCannibal;
    Animator anim;
    public Cannibal[] cannibals;
    public NPC_Target target;
    public bool canFire;
    public bool dead;
    public float currentHealth;
    public float MaxHealth;
    public GameObject checkObj;
    public GameObject checkObj2;
    public Zombie checkZombie;
    [Space]
    public float distance;

    [Header("AudioClips")]
    public AudioClip[] footStepSFX;
    public AudioClip[] BodyFallSFX;
    public AudioClip[] hurtSFX;

    [Header("Attack")]
    public Transform attackPoint;
    public float radius;
    public float checkRadius;
    public LayerMask TargetLayer;
    public float damage;
    public float flameDamage;
    public bool burning;
    public float burnDuration;
    [Space]
    public bool withinTarget;

    [Header("WalkPoints")]
    public Vector3 walkPoint;
    public Vector3 ditanceToWalkPoint;
    public float distanceToWalkMagnitude;
    public bool walkPointSet;

    [Header("VFX")]
    public ParticleSystem flameVFX;
    public ParticleSystem bloodVFX;
    public GameObject[] Bodies;
    public GameObject[] Heads;
    public GameObject[] Legs;
    public GameObject[] Extra;
    [Space]
    public Transform spawnPos;
    public GameObject[] weapons;
    [Space]
    public GameObject slogan;

    [Header("SFX")]
    public AudioClip[] SwingSFX;

    Rigidbody[] ragdollBodies;
    Collider[] colliders;
    NavMeshAgent agent;
    Player player;
    AudioSource audio;
    ExploreManager exp_Manager;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = MaxHealth;
        audio = GetComponent<AudioSource>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        exp_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();

        agent = GetComponent<NavMeshAgent>();

        ToggleRagdoll(false);

        rand = Random.Range(0, weapons.Length);
        SelectWeapon(rand);

        foreach (Collider col in colliders)
        {
            col.isTrigger = true;
        }

        Customize();
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            currentHealth -= Time.time * flameDamage;
        }

        if (currentHealth <= 0)
        {
            Dead();
        }

        anim.SetFloat("speed", agent.velocity.sqrMagnitude);

        distance = Vector3.Distance(transform.position, player.transform.position);
        withinTarget = Physics.CheckSphere(transform.position, checkRadius, TargetLayer);

        if (withinTarget)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            distance = Vector3.Distance(transform.position, player.transform.position);
        }

        if (withinTarget)
        {
            //Find Closest Target
            FindClosesteEnemy();
            canFire = true;
        }
        else
        {
            //Debug.Log("Patrolling");
            canFire = false;
        }

        if (MainCannibal)
        {
            if (distance <= 15f)
            {
                slogan.SetActive(true);
            }
            else
            {
                slogan.SetActive(false);
            }
        }
        
    }


    void SearchWalkPoint()
    {
        Transform tSpawn = exp_Manager.banditPos[Random.Range(0, exp_Manager.banditPos.Length)];
        walkPoint = new Vector3(tSpawn.position.x, transform.position.y, tSpawn.position.z);

        //if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    void SelectWeapon(int choice)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[choice].SetActive(true);
    }

    void FindClosesteEnemy()
    {
        float distanceToClosesteTarget = Mathf.Infinity;
        target = null;

        NPC_Target[] allTarget = GameObject.FindObjectsOfType<NPC_Target>();

        foreach (NPC_Target currentTarget in allTarget)
        {
            float distToTarget = (currentTarget.transform.position - this.transform.position).sqrMagnitude;
            if (distToTarget < distanceToClosesteTarget)
            {
                distanceToClosesteTarget = distToTarget;

                target = currentTarget;


                if (target.isPlayer)
                {
                    agent.SetDestination(target.transform.position);

                    for (int i = 0; i < cannibals.Length; i++)
                    {
                        cannibals[i].checkRadius = 100f;
                        cannibals[i].target = target;
                    }

                    var targetT = target.transform.position;
                    targetT.y = transform.position.y;
                    transform.LookAt(targetT);
                }else
                {
                    var targetT = target.transform.position;
                    targetT.y = transform.position.y;
                    transform.LookAt(targetT);
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (dead) return;


        currentHealth -= amount;
        Hurt();

        if (amount >= 10)
            anim.SetTrigger("Hit");

        bloodVFX.Play();

        if (currentHealth < 0)
        {
            anim.SetTrigger("Dead");
            //GetComponent<CapsuleCollider>().enabled = false;
            //ToggleRagdoll(false);
            GetComponent<NavMeshAgent>().enabled = false;
            checkObj.layer = 0;

            Destroy(checkZombie);

            foreach (Collider col in colliders)
            {
                col.isTrigger = false;
            }

            ToggleRagdoll(true);


            //dead = true;
            Destroy(this);
        }
    }

    public void Burn(float amount)
    {
        if (dead) return;

        flameDamage = amount;
        StartCoroutine(Burn());

        if (currentHealth < 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        if (dead) return;

        anim.SetTrigger("Dead");
        ToggleRagdoll(true);
        //GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        Hurt();

        anim.SetLayerWeight(anim.GetLayerIndex("Rifle"), 0);

        Destroy(this.GetComponent<ZombiesTarget>());

        checkObj.layer = 0;
        checkObj2.layer = 0;


        ToggleRagdoll(true);



        dead = true;
        Destroy(this);
    }

    void ToggleRagdoll(bool state)
    {
        anim.enabled = !state;

        foreach (Collider col in colliders)
        {
            col.isTrigger = !state;
        }

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
    }

    IEnumerator Burn()
    {
        burning = true;

        flameVFX.Play();

        yield return new WaitForSeconds(burnDuration);

        flameVFX.Stop();

        burning = false;
    }

    public void Foots()
    {
        audio.PlayOneShot(footStepSFX[Random.Range(0, footStepSFX.Length)]);
    }

    public void Hurt()
    {
        audio.PlayOneShot(hurtSFX[Random.Range(0, hurtSFX.Length)]);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void Customize()
    {
        for (int i = 0; i < Heads.Length; i++)
        {
            Heads[i].SetActive(false);
        }

        for (int i = 0; i < Bodies.Length; i++)
        {
            Bodies[i].SetActive(false);
        }

        for (int i = 0; i < Legs.Length; i++)
        {
            Legs[i].SetActive(false);
        }

        for (int i = 0; i < Extra.Length; i++)
        {
            Extra[i].SetActive(false);
        }


        Heads[Random.Range(0, Heads.Length)].SetActive(true);
        Bodies[Random.Range(0, Bodies.Length)].SetActive(true);
        Legs[Random.Range(0, Legs.Length)].SetActive(true);
        Extra[Random.Range(0, Extra.Length)].SetActive(true);
    }

    public void Attack()
    {
        Collider[] col = Physics.OverlapSphere(attackPoint.position, radius, TargetLayer);

        audio.PlayOneShot(SwingSFX[Random.Range(0, SwingSFX.Length)]);

        foreach (Collider c in col)
        {
            Zombie_BP z = c.GetComponent<Zombie_BP>();
            if (z != null)
            {
                z.TakeDamage(damage);
            }

            Bandit_BP b = c.GetComponent<Bandit_BP>();
            if (b != null)
            {
                b.TakeDamage(damage);
            }

            NPC npc = c.GetComponent<NPC>();
            if (npc != null)
            {
                npc.TakeDamage(damage);
            }

            Player player = c.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            OBJ o = c.GetComponent<OBJ>();
            if (o != null)
            {
                o.TakeDamage(damage);
            }

            Animal a = c.GetComponent<Animal>();
            if (a != null)
            {
                a.TakeDamage(damage);
            }
        }
    }

}
