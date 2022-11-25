using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExploreManager : MonoBehaviour
{
    public GameObject player;
    public Transform[] wanderPositions;
    public Transform[] airshipPositions;

    [Header("Animal")]
    public int totalAnimals;
    public GameObject[] Animal;
    public Transform[] animalPositions;

    [Header("Bandit")]
    public int totalBandit;
    public GameObject[] bandit;
    public Transform[] banditPos;

    [Header("Enemies")]
    public int totalZombies;
    public GameObject[] zombies;
    public GameObject miniBeasts;
    public GameObject[] zombieBosses;
    public Transform[] zombieSpawnPositions;
    public Transform[] beastSpawnPos;
    public Transform zombieBossSpawnPos;
    [Space]
    public int zombiesSpawned;
    public int maxSpawned;
    public bool isLimited;
    [Space]
    public int currentEnemies;
    public Zombie[] enemyList;

    [Header("Civilians")]
    public int totalCivivls;
    public GameObject[] civilians;
    public Transform[] civilPositions;
    public GameObject[] civilians_Stand;
    public Transform[] civilPositions_Stand;

    [Header("Guards")]
    public GameObject[] guards;
    public GameObject[] standingGuards;
    public Transform[] guardsPos;
    public Transform[] sguardsPos;

    [Header("Survival")]
    public Transform[] srv_spawnPositions;

    [Header("Vehicles")]
    public GameObject CurrentVeh;
    public GameObject[] Vehicles;
    public int vehicleIndex;
    public Transform vehSpawnPos;
    
    [Header("Dogs")]
    public GameObject[] Dogs;
    public int dogIndex;
    public Transform dog_spawnPositions;

    PlayfabManager database;


    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(player, spawnPos.position, spawnPos.rotation);

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        StartCoroutine(LoadData());

        
    }

    IEnumerator LoadData()
    {
        yield return new WaitForSeconds(0.5f);

        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        enemyList = GameObject.FindObjectsOfType<Zombie>();

        currentEnemies = enemyList.Length;

        if (currentEnemies < totalZombies)
        {
            Spawn();
        }

    }

    void GetData()
    {
        dogIndex = PlayerPrefs.GetInt("DogIndex");
        if (dogIndex > 0)
        {
            Instantiate(Dogs[dogIndex], dog_spawnPositions.position, dog_spawnPositions.rotation);
        }

        //--------------------------------------------------------Mini beast spawn----------------------------------//
        Transform tSpawn = beastSpawnPos[Random.Range(0, beastSpawnPos.Length)];
        Instantiate(miniBeasts, beastSpawnPos[0].position, beastSpawnPos[0].rotation);
        Instantiate(miniBeasts, beastSpawnPos[1].position, beastSpawnPos[1].rotation);

        for (int i = 0; i < totalAnimals; i++)
        {
            AnimalSpawn();
        }

        int rand = Random.Range(0, zombieBosses.Length);
        Instantiate(zombieBosses[rand], zombieBossSpawnPos.position, zombieBossSpawnPos.rotation);

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Junkyard"))
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Town"))
            {
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Village"))
                {
                    SpawnVeh();
                }
            }
        }

        for (int i = 0; i < totalZombies; i++)
        {
            Spawn();
        }

        for (int i = 0; i < totalCivivls; i++)
        {
            CivilSpawn();
        }

        for (int i = 0; i < totalBandit; i++)
        {
            BanditSpawn();
        }

        

        guardSpawn();
    }

    void Spawn()
    {
        if (isLimited && zombiesSpawned > maxSpawned) return;

        Transform tSpawn = zombieSpawnPositions[Random.Range(0, zombieSpawnPositions.Length)];
        Instantiate(zombies[Random.Range(0, zombies.Length)], tSpawn.position, tSpawn.rotation);

        zombiesSpawned++;
    }

    void AnimalSpawn()
    {
        Transform tSpawn = animalPositions[Random.Range(0, animalPositions.Length)];
        Instantiate(Animal[Random.Range(0, Animal.Length)], tSpawn.position, tSpawn.rotation);
    }

    void CivilSpawn()
    {
        Transform tSpawn = civilPositions[Random.Range(0, civilPositions.Length)];
        Instantiate(civilians[Random.Range(0, civilians.Length)], tSpawn.position, tSpawn.rotation);

        /*Transform tSpawnST = civilPositions_Stand[Random.Range(0, civilPositions_Stand.Length)];
        Instantiate(civilians_Stand[Random.Range(0, civilians_Stand.Length)], tSpawnST.position, tSpawnST.rotation);*/
    }

    void SpawnVeh()
    {
        vehicleIndex = PlayerPrefs.GetInt("VehicleIndex");
        if (vehicleIndex > 0)
        {
            CurrentVeh = Instantiate(Vehicles[vehicleIndex], vehSpawnPos.position, vehSpawnPos.rotation);
        }
    }

    void BanditSpawn()
    {
        Transform tSpawn = banditPos[Random.Range(0, banditPos.Length)];
        Instantiate(bandit[Random.Range(0, bandit.Length)], tSpawn.position, tSpawn.rotation);
    }
    
    void guardSpawn()
    {
        for (int i = 0; i < guardsPos.Length; i++)
        {
            Instantiate(guards[Random.Range(0, guards.Length)], guardsPos[i].position, guardsPos[i].rotation);
        }

        for (int i = 0; i < sguardsPos.Length; i++)
        {
            Instantiate(standingGuards[Random.Range(0, standingGuards.Length)], sguardsPos[i].position, sguardsPos[i].rotation);
        }
    }
}
