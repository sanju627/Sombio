
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DataImporter : MonoBehaviour
{
    //public AuthManager authManager;
    EnergySystem energySystem;
    public Shop shop;
    public Storage srvShop;
    public SurvivalShop survivalWeapon;
    public int woodAmount;
    public bool dataLoaded;
    [Space]
    public GameObject[] civils;
    public NPC[] allSRV;


    [Header("Survival")]
    public GameObject[] survivals;
    [Space(20f)]
    public TMP_Text moneyAmountTXT;
    public TMP_Text fuelAmountTXT;

    HomeBase homeBase;
    ExploreManager expManager;
    PlayfabManager database;
    Player player;
    ItemAssets itemAssets;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        energySystem = GetComponent<EnergySystem>();
        itemAssets = GetComponent<ItemAssets>();

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        //authManager.LoadData();
        Debug.Log("Loading All Data");
        StartCoroutine(LoadData());

        inventory = player.inventory;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            homeBase = GameObject.FindGameObjectWithTag("HomeBase").GetComponent<HomeBase>();
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Game"))
        {
            expManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ExploreManager>();
        }

        if(database.loaded)
        {
            //GetData();
        }
    }

    IEnumerator LoadData()
    {
        database.GetData();
        database.GetSRVS();

        yield return new WaitForSeconds(0.5f);

        GetData();
    }

    private void Update()
    {
        allSRV = GameObject.FindObjectsOfType<NPC>();

        
    }

    // Update is called once per frame
    void GetData()
    {
        energySystem.energy = database.maxEnergy;
        shop.money = database.coins;
        moneyAmountTXT.text = ": " + database.coins.ToString();
        fuelAmountTXT.text = database.fuelAmount.ToString();

        player.SwitchSkin(PlayerPrefs.GetInt("Selected Costume"));

        if (database.ammoAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.ammo, amount = database.ammoAmount });
        }

        if (database.medkitAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.medkit, amount = database.medkitAmount });
        }

        if (database.bandageAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.bandage, amount = database.bandageAmount });
        }

        if (database.grenadeAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.grenade, amount = database.grenadeAmount });
        }

        if (database.molotovAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.molotov, amount = database.molotovAmount });
        }

        if (database.smokeAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.smoke, amount = database.smokeAmount });
        }

        if (database.landmineAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.landmine, amount = database.landmineAmount });
        }

        if (database.chickenAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.chicken, amount = database.chickenAmount });
        }

        if (database.woodAmount > 0)
        {
            woodAmount = database.woodAmount;
            player.inventory.AddItem(new Item { itemType = Item.ItemType.wood, amount = database.woodAmount });
        }

        if (database.stoneAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.stone, amount = database.stoneAmount });
        }

        if (database.woodWallAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.wall, amount = database.woodWallAmount });
        }

        if (database.metalWallAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.metalWall, amount = database.metalWallAmount });
        }

        if (database.woodDoorAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.woodDoor, amount = database.woodDoorAmount });
        }

        if (database.metalDoorAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.metalDoor, amount = database.metalDoorAmount });
        }

        if (database.fuelAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.fuel, amount = database.fuelAmount });
        }

        //-------------------------------------------Weapons------------------------------------//

        if (database.krissAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.kriss, amount = database.krissAmount });

            //srvShop.EnableWeapon(1);
        }

        if (database.MP7Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.mp7, amount = database.MP7Amount });
            //srvShop.EnableWeapon(2);
        }

        if (database.MP5Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.mp5, amount = database.MP5Amount });
            //srvShop.EnableWeapon(3);
        }

        if (database.UMPAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.ump45, amount = database.UMPAmount });
            //srvShop.EnableWeapon(4);
        }

        if (database.Tec9Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.tec9, amount = database.Tec9Amount });
            //srvShop.EnableWeapon(5);
        }

        if (database.UZIAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.uzi, amount = database.UZIAmount });
            //srvShop.EnableWeapon(6);
        }

        if (database.ak12Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.ak12, amount = database.ak12Amount });
            //srvShop.EnableWeapon(7);
        }

        if (database.ak74Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.ak74, amount = database.ak74Amount });
            //srvShop.EnableWeapon(8);
        }

        if (database.G3A4Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.g3a4, amount = database.G3A4Amount });
            //srvShop.EnableWeapon(9);
        }

        if (database.G36CAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.g36c, amount = database.G36CAmount });
            //srvShop.EnableWeapon(10);
        }

        if (database.flamethrowerAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.flamethrower, amount = database.flamethrowerAmount });
        }

        if (database.Glock17Amount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.glock17, amount = database.Glock17Amount });
        }

        if (database.MinigunAmount > 0)
        {
            player.inventory.AddItem(new Item { itemType = Item.ItemType.minigun, amount = database.MinigunAmount });
        }

        //---------------------------------------------------Survival Weapon------------------------------------------------//

        

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            LoadSRV();

            //--------------------------------------Rest-------------------------------------------------------------------//

            homeBase.bedController.SetBed();
            homeBase.Check();
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Game"))
        {
            if(database.srvEXP1 == 1)
            {
                Transform tSpawn = expManager.srv_spawnPositions[Random.Range(0, expManager.srv_spawnPositions.Length)];

                Instantiate(survivals[0], tSpawn.position, tSpawn.rotation);
            }

            if (database.srvEXP2 == 1)
            {
                Transform tSpawn = expManager.srv_spawnPositions[Random.Range(0, expManager.srv_spawnPositions.Length)];

                Instantiate(survivals[1], tSpawn.position, tSpawn.rotation);
            }

            if (database.srvEXP3 == 1)
            {
                Transform tSpawn = expManager.srv_spawnPositions[Random.Range(0, expManager.srv_spawnPositions.Length)];

                Instantiate(survivals[2], tSpawn.position, tSpawn.rotation);
            }

            if (database.srvEXP4 == 1)
            {
                Transform tSpawn = expManager.srv_spawnPositions[Random.Range(0, expManager.srv_spawnPositions.Length)];

                Instantiate(survivals[3], tSpawn.position, tSpawn.rotation);
            }

            if (database.srvEXP5 == 1)
            {
                Transform tSpawn = expManager.srv_spawnPositions[Random.Range(0, expManager.srv_spawnPositions.Length)];

                Instantiate(survivals[4], tSpawn.position, tSpawn.rotation);
            }
        }

        dataLoaded = true;
    }

    public void LoadSRV()
    {
        foreach (NPC srv in allSRV)
        {
            Destroy(srv.statsBTN.gameObject);
            Destroy(srv.gameObject);
        }

        for (int i = 0; i < survivals.Length; i++)
        {
            Transform tSpawn = homeBase.campFirePos[Random.Range(0, homeBase.campFirePos.Length)];

            Instantiate(survivals[i], tSpawn.position, tSpawn.rotation);
        }

    }

    public void SendInventoryData()
    {
        database.SendData("Item Ammo", inventory.ammoAmount.ToString());
        database.SendData("Item Bandage", inventory.bandageAmount.ToString());
        database.SendData("Item Medkit", inventory.medkitAmount.ToString());
        database.SendData("Item Grenade", inventory.grenadeAmount.ToString());
        database.SendData("Item Chicken", inventory.chickenAmount.ToString());
        database.SendData("Item Landmine", inventory.landmineAmount.ToString());
        database.SendData("Item MetalWall", inventory.metalWallAmount.ToString());
        database.SendData("Item MetalDoor", inventory.metalDoorAmount.ToString());
        database.SendData("Item WoodDoor", inventory.woodDoorAmount.ToString());
        database.SendData("Item WoodWall", inventory.wallAmount.ToString());
        database.SendData("Item Molotov", inventory.molotovAmount.ToString());
        database.SendData("Item Smoke", inventory.smokeAmount.ToString());
        database.SendData("Item Stone", inventory.stoneAmount.ToString());
        database.SendData("Item Wood", itemAssets.woodAmount.ToString());
        //Debug.Log(inventory.woodAmount);

        database.SendData("Weapon AK12", inventory.ak12Amount.ToString());
        database.SendData("Weapon AK74", inventory.ak74Amount.ToString());
        database.SendData("Weapon Kriss", inventory.krissAmount.ToString());
        database.SendData("Weapon Flamethrower", inventory.flamethrowerAmount.ToString());
        database.SendData("Weapon G36C", inventory.g36cAmount.ToString());
        database.SendData("Weapon G3A4", inventory.g3a4Amount.ToString());
        database.SendData("Weapon Glock17", inventory.glock17Amount.ToString());
        database.SendData("Weapon MP5", inventory.mp5Amount.ToString());
        database.SendData("Weapon MP7", inventory.mp7Amount.ToString());
        database.SendData("Weapon Tec9", inventory.tec9Amount.ToString());
        database.SendData("Weapon UMP", inventory.ump45Amount.ToString());
        database.SendData("Weapon UZI", inventory.uziAmount.ToString());
    }

}
