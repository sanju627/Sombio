using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    Animator anim;
	NavMeshAgent agent;
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

    public PlayfabManager database;
    [Header("Stats")]
    public Transform[] movePoints;
    public GameObject[] Weapons;
    public NPC_Gun[] guns;
	public Zombie zombie;
	public bool walkPointSet;
    public Vector3 walkPoint;
    public Vector3 ditanceToWalkPoint;
    public float distanceToWalkMagnitude;
    public float checkRadius;
    public float weaponCheckRadius;
    public float radius;
    public float playerCheckRadius;
    public float withinMeleeRadius;
    public float fleeDist = 1f;
    [Space]
    public float attackRate;
    public float damage;
    public LayerMask TargetLayer;
    public LayerMask playerLayer;
    public Transform attackPoint;
    [Space]
    public float currentHealth;
    public float maxHealth;
    public float healHealth;
    public float healTime;
    public bool isHealing;
    [Space]
    public GameObject GunOBJ;
    public GameObject MeleeOBJ;

    [Header("Stats")]
    public bool stateSet;
    [Space]
    public bool isResting;
    public bool homeBase;
    public bool civil;
    public bool patrol;
    public bool follow;
    public bool stand;
    public bool melee;
    public bool gun;
    public bool withinEnemy;
    public bool withinPlayer;
    public bool withinMelee;
    public bool canFire;
    public bool dead;
    public int weaponNum;
    [Space]
    public bool madScientist;
    public bool redFox;
    public bool romanCaeser;
    public bool romanLegion;
    public bool Biden;
    public bool Trump;
    public bool BlueSoldier;
    public bool RedSoldier;
    public bool STPMale;
    public bool STPFemale;
    public bool FrenchFries;
    public bool Pickle;
    public bool Cowboy;
    public bool Pirate;
    public bool SWAT;
    

    [Header("SFX")]
    public AudioClip[] hurtSFX;
    public AudioClip[] swingSFX;
    public AudioClip[] footsSFX;

    [Header("UI")]
    public Slider healthSlider;
    public Slider energySlider;
    public GameObject Canvas;
    public GameObject statsBTN;
    public GameObject statsInfo;
    public TextMeshProUGUI statTXT;
    public GameObject InteractBTN;

    [Header("VFX")]
    public ParticleSystem bloodVFX;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (homeBase)
            _homeBase = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        poptext = GameObject.FindGameObjectWithTag("MSG").GetComponent<popTXT>();

        if(!civil)
        {
            statTXT.text = "Unequipped";
            statTXT.fontSize = 15f;
        }    
        else
        {
            statTXT.text = "Follow";
        }
            

        

        ToggleRagdoll(false);

        //GetWeaponData();

        if (gun)
            Switch(weaponNum);

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (civil)
        {
            statsInfo.SetActive(false);

            statsBTN.SetActive(true);

            if (!homeBase)
            {
                ep_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();
                movePoints = ep_Manager.civilPositions;
            }
            else
            {
                home = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();
                movePoints = home.campFirePos;

                statsInfo.SetActive(false);
                statsBTN.SetActive(false);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Game"))
            {
                ep_Manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();

                patrol = false;
                follow = true;

                ClickPick();

                movePoints = ep_Manager.civilPositions;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
            {
                home = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();

                movePoints = home.campFirePos;
            }
        }

        Collider[] col = GetComponentsInChildren<Collider>();
        foreach (Collider c in col)
        {
            c.isTrigger = true;
        }

        if (!civil)
        SendStats();


        poptext = GameObject.FindGameObjectWithTag("MSG").GetComponent<popTXT>();
        //poptext.PopText("Not Enough Slots");
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)return;

        if(!withinEnemy)
        anim.SetFloat("speed", agent.velocity.sqrMagnitude);

        if (currentHealth <= 0)
        {
            Dead();
        }


        withinEnemy = Physics.CheckSphere(transform.position, weaponCheckRadius, TargetLayer);

        withinPlayer = Physics.CheckSphere(transform.position, playerCheckRadius, playerLayer);

        withinMelee = Physics.CheckSphere(transform.position, withinMeleeRadius, TargetLayer);

        if (!follow)
        {
            StartCoroutine(EnableSkin(0));
        }

        //--------------------------------------------------Patrolling---------------------------------------//
        if (patrol && !withinEnemy && !homeBase && !civil)
        	Patroling();

        if(follow)
        {
            if (player.inCar || player.inHelicopter)
            {
                transform.position = player.transform.position;
                agent.enabled = false;
                DisableSkin();
                canFire = false;
            }
            else
            {

                agent.enabled = true;

                if(player.isRunning)
                {
                    agent.speed = 4f;
                    anim.SetBool("run", true);
                }else
                {
                    agent.speed = 2f;
                    anim.SetBool("run", false);
                }
                agent.SetDestination(player.transform.position);

                StartCoroutine(EnableSkin(0.1f));

                //Debug.Log("Chasing");

                canFire = true;
            }
        }

        //--------------------------------------------------Follow Player---------------------------------------//
        if(follow && !withinEnemy)
        {
            if(!withinPlayer)
            {
                if (player.inCar || player.inAirship || player.inHelicopter)
                {
                    transform.position = player.transform.position;

                    DisableSkin();

                    agent.enabled = false;
                    canFire = false;
                }
                else
                {

                    agent.enabled = true;
                    agent.SetDestination(player.transform.position);

                    StartCoroutine(EnableSkin(0.1f));

                    //Debug.Log("Chasing");

                    canFire = true;
                }
            }
        }else if(!follow)
        {
            Patroling();
            //StartCoroutine(EnableSkin(0));
        }

        //-------------------------------Civilians-------------------------------//

        
        if(civil && !homeBase && !player.inCar)
        {
            if (withinPlayer)
            {
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("CityBase"))
                {
                    Canvas.SetActive(true);
                }
                
            }else if(!withinPlayer && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CityBase"))
            {
                Canvas.SetActive(false);
            }
        }else if(player.inCar)
        {
            Canvas.SetActive(false);
        }

        if(homeBase)
        {
            Canvas.SetActive(false);
        }

        if(homeBase && !withinEnemy && civil && !setPos)
        {
            Transform tSpawn = _homeBase.campFirePos[Random.Range(0, _homeBase.campFirePos.Length)];
            float dist = Vector3.Distance(tSpawn.position, transform.position);

            agent.SetDestination(tSpawn.position);

            setPos = true;
        }

        if (withinEnemy && !isHealing)
        {
            FindClosesteEnemy();

            anim.SetFloat("speed", 0f);

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
                        anim.SetTrigger("attack1");
                    }
                    else
                    {
                        anim.SetTrigger("attack2");
                    }

                    nextAttackTime = Time.time + 1f / attackRate;
                }

                if (distance < fleeDist)
                {
                    Vector3 DistToEnemy = transform.position - zombie.transform.position;

                    Vector3 newPos = transform.position + DistToEnemy;

                    anim.SetFloat("speed", -1f);

                    agent.SetDestination(newPos);
                }
            }else
            {
                GunOBJ.SetActive(true);
                MeleeOBJ.SetActive(false);

                if(!player.inCar)
                canFire = true;
            }

            setPos = false;
        }
        else
        {
            GunOBJ.SetActive(true);
            MeleeOBJ.SetActive(false);

            canFire = false;
        }


        if (stand)
        {
            agent.enabled = false;
        }
        else if (!stand)
        {
            agent.enabled = true;
        }

        //------------------------------------------Healing---------------------------------//
        if (currentHealth <= healHealth && !isHealing)
        {
            //StartCoroutine(Heal());
        }

        healthSlider.value = currentHealth;
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

    void Patroling()
    {
        if(!walkPointSet)SearchWalkPoint();

        agent.SetDestination(walkPoint);

        ditanceToWalkPoint = transform.position - walkPoint;
        distanceToWalkMagnitude = ditanceToWalkPoint.magnitude;

        if(ditanceToWalkPoint.magnitude < 12f)
        walkPointSet = false;
    }


    void SearchWalkPoint()
    {	
    	if(!withinEnemy)
    		tSpawn = movePoints[Random.Range(0, movePoints.Length)];
    	else
    		tSpawn = anim.transform;

        walkPoint = new Vector3(tSpawn.position.x, tSpawn.position.y, tSpawn.position.z);

       // if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    IEnumerator Heal()
    {
        isHealing = true;

        anim.SetBool("heal", true);

        yield return new WaitForSeconds(healTime);

        anim.SetBool("heal", false);

        currentHealth = maxHealth;

        isHealing = false;
    }

    void OnDrawGizmosSelected()
    {
    	Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponCheckRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void TakeDamage(float amount)
    {
    	currentHealth -= amount;

        healthSlider.value = currentHealth;

    	audio.PlayOneShot(hurtSFX[Random.Range(0, hurtSFX.Length)]);

    	bloodVFX.Play();

    	anim.SetTrigger("hit");

    	if(currentHealth <= 0)
    	{
    		Dead();
    	}
    }

    public void Dead()
    {
        Collider[] col = GetComponentsInChildren<Collider>();

        
        agent.enabled = false;
        Destroy(this.GetComponent<ZombiesTarget>());
        Destroy(this.GetComponent<NPC_Target>());
        Canvas.SetActive(false);

        if (!civil)
        {
            //StartCoroutine(database.UpdateItem("Survivals", database.survivals - 1));
            if(madScientist)
            {
                database.srvAmount -= 1;
                database.srvRest += 1;
                //database.SendData("SRV Survival", database.srvAmount.ToString());
                database.SendData("SRV Rest", database.srvRest.ToString());
            }

            if (redFox)
            {
                //database.srvRedFox = 0;
                database.srvRedFox_Rest += 1;
                database.SendStats();
            }

            if (romanLegion)
            {
                //database.srvRomanLegion = 0;
                database.srvRomanLegion_Rest += 1;
                database.SendStats();
            }

            if (romanCaeser)
            {
                //database.srvRomanCaeser = 0;
                database.srvRomanCaeser_Rest += 1;
                database.SendStats();
            }

            if (Biden)
            {
                database.srvBiden -= 1;
                database.srvBiden_Rest += 1;
                database.SendStats();
            }

            if (Trump)
            {
                //database.srvTrump = 0;
                database.srvTrump_Rest += 1;
                database.SendStats();
            }

            if (RedSoldier)
            {
                //database.srvSoldierRed = 0;
                database.srvSoldierRed_Rest += 1;
                database.SendStats();
            }

            if (BlueSoldier)
            {
                //database.srvSoldierBlue = 0;
                database.srvSoldierBlue_Rest += 1;
                database.SendStats();
            }

            if (STPMale)
            {
                //database.srvSTPMale = 0;
                database.srvSTPMale_Rest += 1;
                database.SendStats();
            }

            if (STPFemale)
            {
                //database.srvSTPFemale = 0;
                database.srvSTPFemale_Rest += 1;
                database.SendStats();
            }

            if (FrenchFries)
            {
                //database.srvFrenchFry = 0;
                database.srvFrenchFry_Rest += 1;
                database.SendStats();
            }

            if (Pickle)
            {
                //database.srvPickle = 0;
                database.srvPickle_Rest += 1;
                database.SendStats();
            }


            if(Cowboy)
            {
                //database.SendData("SRV CowBoy", "0");
                database.srvCowboy_Rest += 1;
                database.SendData("SRV CowBoy_Rest", database.srvCowboy_Rest.ToString());
            }

            if (Pirate)
            {
                //database.SendData("SRV Pirate", "0");
                //database.srvPirate_Rest += 1;
                database.SendData("SRV Pirate_Rest", database.srvPirate_Rest.ToString());
            }

            if (SWAT)
            {
                //database.SendData("SRV SWAT", "0");
                database.srvSWAT_Rest += 1;
                database.SendData("SRV SWAT_Rest", database.srvSWAT_Rest.ToString());
            }

        }
        else if(civil && follow)
        {
            database.civilAmount -= 1;
            database.SendData("SRV Civils", database.civilAmount.ToString());
        }

        foreach (Collider c in col)
        {
            c.isTrigger = false;
        }

        ToggleRagdoll(true);

        gameObject.layer = 0;

        dead = true;

        Canvas.SetActive(false);
    }

    public void ClickPick()
    {
        

        if (civil)
        {
            if(database.civilAmount < 10)
            {
                statsInfo.SetActive(true);
                statsBTN.SetActive(false);

                //StartCoroutine(database.UpdateItem("Civils", database.civils + 1));

                database.civilAmount += 1;
                database.SendData("SRV Civils", database.civilAmount.ToString());

                follow = true;
                patrol = false;
            }else if(database.civilAmount >= 10)
            {
                poptext.PopText("Not Enough Slots");
            }
            

        }
        else if(!civil)
        {
            if (!stateSet && player.currentSurvivars < player.maxSurvivars)
            {
                follow = true;
                patrol = false;

                

                player.currentSurvivars += 1;


                SendStats();

                stateSet = true;
            }
            else if (stateSet)
            {
                follow = false;
                patrol = true;


                player.currentSurvivars -= 1;

                SendStats();

                stateSet = false;
            }

            if (player.currentSurvivars >= player.maxSurvivars)
            {
                poptext.PopText("Not Enough Slots");
            }
        }
    }

    public void SendStats()
    {
        //statsBTN.SetActive(true);
        //statsBTN.transform.GetComponent<RectTransform>().Rotate(0,0,0);
        //statsBTN.transform.SetParent(player.ui_Inventory.survivalSlot);
        //statsBTN.transform.position = new Vector3(0,0,0);
        //statsBTN.transform.localScale = new Vector3(1,1,1);
        //statsBTN.transform.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,0);

        //InteractBTN.SetActive(false);

        if (isResting) return;
        equippedStats();
    }

    public void equippedStats()
    {
        statsBTN.SetActive(true);
        statsBTN.transform.GetComponent<RectTransform>().Rotate(0, 0, 0);
        statsBTN.transform.SetParent(player.ui_Inventory.equippedSlot);
        statsBTN.transform.position = new Vector3(0, 0, 0);
        statsBTN.transform.localScale = new Vector3(1, 1, 1);
        statsBTN.transform.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

        InteractBTN.SetActive(true);
    }

    public void FootSFX()
    {
    	audio.PlayOneShot(footsSFX[Random.Range(0, footsSFX.Length)]);
    }

    void ToggleRagdoll(bool state)
    {
    	anim.enabled = !state;


    	foreach(Rigidbody rb in ragdollBodies)
    	{
    		rb.isKinematic = !state;
    	}
    }

    void FindClosesteEnemy()
    {
    	float distanceToClosesteEnemy = Mathf.Infinity;
    	zombie = null;

    	Zombie[] allZombies = GameObject.FindObjectsOfType<Zombie>();

    	foreach(Zombie currentZombie in allZombies)
    	{
    		float distToEnemy = (currentZombie.transform.position - this.transform.position).sqrMagnitude;
    		if(distToEnemy < distanceToClosesteEnemy)
    		{
    			distanceToClosesteEnemy = distToEnemy;
    			zombie = currentZombie;
    		}
    	}

    	var targetT = zombie.transform.position;
        targetT.y = transform.position.y;
        transform.LookAt(targetT);
    }

    public void Attack()
    {
        Collider[] enemyCol = Physics.OverlapSphere(attackPoint.position, radius, TargetLayer);

        resetWeapon();

        audio.PlayOneShot(swingSFX[Random.Range(0, swingSFX.Length)]);

        foreach(Collider t in enemyCol)
        {
            Zombie_BP z = t.GetComponent<Zombie_BP>();
            if(z != null)
            {
                z.TakeDamage(damage);
            }

            Bandit_BP b = t.GetComponent<Bandit_BP>();
            if(b != null)
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

    public void Switch(int choice)
    {
        for(int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }

        //SetWeaponNum(choice);

        

        for(int i = 0; i < guns.Length; i++)
        {
            guns[i].isReloading = false;
        }

        if(!civil)
        {
            Weapons[choice].SetActive(true);
        }
        else if(civil)
        {
            int rand = Random.Range(0, 3);
            if(rand == 0 || rand == 1)
            {
                Weapons[0].SetActive(true);
                melee = true;
                gun = false;
            }else
            {
                Weapons[1].SetActive(true);
                melee = false;
                gun = true;
            }
        }        
    }

    public void resetWeapon()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].isReloading = false;
        }
    }

    public void ClickFollow()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Game"))
        {
            if (!stand)
            {
                stand = true;

                statTXT.text = "Equip";
            }
            else if (stand)
            {
                stand = false;

                if (!civil)
                    statTXT.text = "Unequip";
                else if (civil)
                    statTXT.text = "Pick";
            }
        }else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            if (!stand)
            {
               // Equip();
                
                

                stand = true;
            }
            else if (stand)
            {
               // UnEquip();
                statTXT.text = "Unequipped";

                stand = false;
            }
        }
        
    }

    public void Equip()
    {
        

        PlayfabManager.database.UpdateSRV();
    }

    public void UnEquip()
    {

        PlayfabManager.database.UpdateSRV();
    }

    public void WeaponClick()
    {
        player.ui_Inventory.Click_SurvivalWeaponPanel();
        //player.ui_Inventory.SetSurvivalShop(this.GetComponent<NPC>());
    }

    void SetWeaponNum(int choice)
    {
        if(madScientist)
        {
            PlayerPrefs.SetInt("SRV madScientist Weapon", choice);
        }

        if (redFox)
        {
            PlayerPrefs.SetInt("SRV redFox Weapon", choice);
        }

        if (romanCaeser)
        {
            PlayerPrefs.SetInt("SRV romanCaeser Weapon", choice);
        }

        if (romanLegion)
        {
            PlayerPrefs.SetInt("SRV romanLegion Weapon", choice);
        }

        if (Biden)
        {
            PlayerPrefs.SetInt("SRV Biden Weapon", choice);
        }

        if (Trump)
        {
            PlayerPrefs.SetInt("SRV Trump Weapon", choice);
        }

        if (BlueSoldier)
        {
            PlayerPrefs.SetInt("SRV BlueSoldier Weapon", choice);
        }

        if (RedSoldier)
        {
            PlayerPrefs.SetInt("SRV RedSoldier Weapon", choice);
        }

        if (STPMale)
        {
            PlayerPrefs.SetInt("SRV STPMale Weapon", choice);
        }

        if (STPFemale)
        {
            PlayerPrefs.SetInt("SRV STPFemale Weapon", choice);
        }

        if (FrenchFries)
        {
            PlayerPrefs.SetInt("SRV FrenchFries Weapon", choice);
        }

        if (Pickle)
        {
            PlayerPrefs.SetInt("SRV Pickle Weapon", choice);
        }

        if (Cowboy)
        {
            PlayerPrefs.SetInt("SRV Cowboy Weapon", choice);
        }

        if (SWAT)
        {
            PlayerPrefs.SetInt("SRV SWAT Weapon", choice);
        }

        if (Pirate)
        {
            PlayerPrefs.SetInt("SRV Pirate Weapon", choice);
        }
    }

    void GetWeaponData()
    {
        if (madScientist)
        {
            weaponNum = PlayerPrefs.GetInt("SRV madScientist Weapon");
        }

        if (redFox)
        {
            weaponNum = PlayerPrefs.GetInt("SRV redFox Weapon");
        }

        if (romanCaeser)
        {
            weaponNum = PlayerPrefs.GetInt("SRV romanCaeser Weapon");
        }

        if (romanLegion)
        {
            weaponNum = PlayerPrefs.GetInt("SRV romanLegion Weapon");
        }

        if (Biden)
        {
            weaponNum = PlayerPrefs.GetInt("SRV Biden Weapon");
        }

        if (Trump)
        {
            weaponNum = PlayerPrefs.GetInt("SRV Trump Weapon");
        }

        if (BlueSoldier)
        {
            weaponNum = PlayerPrefs.GetInt("SRV BlueSoldier Weapon");
        }

        if (RedSoldier)
        {
            weaponNum = PlayerPrefs.GetInt("SRV RedSoldier Weapon");
        }

        if (STPMale)
        {
            weaponNum = PlayerPrefs.GetInt("SRV STPMale Weapon");
        }

        if (STPFemale)
        {
            weaponNum = PlayerPrefs.GetInt("SRV STPFemale Weapon");
        }

        if (FrenchFries)
        {
            weaponNum = PlayerPrefs.GetInt("SRV FrenchFries Weapon");
        }

        if (Pickle)
        {
            weaponNum = PlayerPrefs.GetInt("SRV Pickle Weapon");
        }

        if (Cowboy)
        {
            weaponNum = PlayerPrefs.GetInt("SRV Cowboy Weapon");
        }

        if (SWAT)
        {
            weaponNum = PlayerPrefs.GetInt("SRV SWAT Weapon");
        }

        if (Pirate)
        {
            weaponNum = PlayerPrefs.GetInt("SRV Pirate Weapon");
        }
    }
}
