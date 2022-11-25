using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    public Transform[] campFirePos;
    public Transform[] animalPos;
    public Transform[] airshipPos;
    public GameObject civils;
    public BedController bedController;

    [Header("Vehicles")]
    public GameObject CurrentVeh;
    public GameObject[] Vehicles;
    public int vehicleIndex;
    public Transform vehSpawnPos;

    [Header("Dog")]
    public GameObject CurrentDog;
    public GameObject[] Dogs;
    public int dogIndex;
    public Transform dogSpawnPos;

    [Header("Survivals")]
    public Transform[] srv_spawnPositions;

    [Header("Audio")]
    public AudioSource BG_Music;

    [Header("SRV Rest")]
    public Biden biden;

    [Header("Raid")]
    public float raidTimer;
    public float maxRaidTimer;
    public bool isRaiding;
    public GameObject[] zombies;
    public Transform[] spawnPoses;
    public int totalZombieCount;

    PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        //BG_Music = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        BG_Music.volume = PlayerPrefs.GetFloat("MusicVolume");


        raidTimer = maxRaidTimer;

        database.srvEXP1 = 0;
        database.srvEXP2 = 0;
        database.srvEXP3 = 0;
        database.srvEXP4 = 0;
        database.srvEXP5 = 0;

        PlayfabManager.database.UpdateSRV();

        SpawnVeh();
        SpawnDog();

        
    }

    public void Check()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(raidTimer > 0)
        {
            if (raidTimer > 0)
            {
                raidTimer -= Time.deltaTime;
            }
            else
            {
                raidTimer = 0;
            }
        }

        if(raidTimer <= 0)
        {
            Raid();
        }
    }

    void Raid()
    {
        for (int i = 0; i < totalZombieCount; i++)
        {
            GameObject zombie = zombies[Random.Range(0, zombies.Length)];
            Transform tSPawn = spawnPoses[Random.Range(0, spawnPoses.Length)];
            Instantiate(zombie, tSPawn.position, tSPawn.rotation);
        }

        raidTimer = maxRaidTimer;
    }



    void SpawnVeh()
    {
        vehicleIndex = PlayerPrefs.GetInt("VehicleIndex");
        if (vehicleIndex > 0)
        {
            CurrentVeh = Instantiate(Vehicles[vehicleIndex], vehSpawnPos.position, vehSpawnPos.rotation);
        }
    }

    void SpawnDog()
    {
        dogIndex = PlayerPrefs.GetInt("DogIndex");
        if (dogIndex > 0)
        {
            CurrentDog = Instantiate(Dogs[dogIndex], dogSpawnPos.position, dogSpawnPos.rotation);
        }
    }

    public void DesVeh()
    {
        Destroy(CurrentVeh);
    }

    public void DesDog()
    {
        Destroy(CurrentDog);
    }
}
