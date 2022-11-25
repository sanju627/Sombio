using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    
    NavMeshAgent agent;
    Zombie zombie;
    Transform tSpawn;
    HomeBase _homeBase;
    Rigidbody[] ragdollBodies;
    Collider[] colliders;
    float nextAttackTime = 0f;
    AudioSource audio;
    Player player;
    float distance;
    bool setPos;
    popTXT poptext;
    ExploreManager ep_Manager;
    HomeBase home;

    public bool withinEnemy;
    public bool canFire;
    public bool dead;
    [Space]
    public GameObject GunOBJ;
    [Space]
    public float withinMeleeRadius;
    public float weaponCheckRadius;
    public float fleeDist;
    public int rand;
    [Space]
    public LayerMask TargetLayer;

    [Header("Stats")]
    public Animator anim;
    public float currentHealth;
    public float maxHealth;

    [Header("SFX")]
    public AudioClip[] hurtSFX;
    public AudioClip[] swingSFX;

    [Header("VFX")]
    public ParticleSystem bloodVFX;
    public GameObject[] weapons;
    public GameObject slogan;

    [Header("Melee")]
    public float attackRate;
    public float damage;
    public float radius;
    public LayerMask playerLayer;
    public Transform attackPoint;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;

        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ToggleRagdoll(false);

        rand = Random.Range(0, weapons.Length);
        SelectWeapon(rand);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;

        withinEnemy = Physics.CheckSphere(transform.position, weaponCheckRadius, TargetLayer);

        if (currentHealth <= 0)
        {
            Dead();
        }

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if(dist <= 10f)
        {
            slogan.SetActive(true);
        }else
        {
            slogan.SetActive(false);
        }


        if (withinEnemy)
        {
            FindClosesteEnemy();

            anim.SetFloat("speed", 0f);

            agent.SetDestination(transform.position);

            distance = Vector3.Distance(transform.position, zombie.transform.position);


            canFire = true;
        }else
        {
            canFire = false;
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, weaponCheckRadius);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;


        audio.PlayOneShot(hurtSFX[Random.Range(0, hurtSFX.Length)]);

        bloodVFX.Play();

        anim.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Collider[] col = GetComponentsInChildren<Collider>();

        ToggleRagdoll(true);
        agent.enabled = false;
        Destroy(this.GetComponent<ZombiesTarget>());
        Destroy(this.GetComponent<NPC_Target>());

        anim.SetTrigger("dead");

        foreach (Collider c in col)
        {
            c.isTrigger = false;
        }

        gameObject.layer = 0;

        dead = true;

    }

    void ToggleRagdoll(bool state)
    {
        anim.enabled = !state;

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
    }

    void SelectWeapon(int choice)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[choice].SetActive(true);
    }

    public void Attack()
    {
        Collider[] enemyCol = Physics.OverlapSphere(attackPoint.position, radius, TargetLayer);

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

        }
    }
}
