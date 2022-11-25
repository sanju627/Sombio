using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemAssets : MonoBehaviour
{
	public static ItemAssets Instance {get; private set;}

	// ammo,
	// medkit,
	// bandage,
	// grenade,
	// molotov,
	// smoke,
	// landmine,

	void Awake()
	{
		Instance = this;
        //database = GameObject.FindGameObjectWithTag("Database").GetComponent<AuthManager>();
        //database.LoadData();
    }

    public List<Item> itemList;
    [Space]
    public Transform itemWorldPrefab;
	[Space]

    public Sprite ammoSprite; 
    public Sprite medkitSprite;
    public Sprite bandageSprite;
    public Sprite grenadeSprite;
    public Sprite molotovSprite;
    public Sprite smokeSprite;
    public Sprite landmineSprite;
    public Sprite chickenSprite;
    public Sprite woodSprite;
    public Sprite stoneSprite;
    public Sprite fuelSprite;
    [Space]
    //----------------------------------------------------------Weapon---------------------------------------------//
    public Sprite krissSprite;
    public Sprite mp7Sprite;
    public Sprite mp5Sprite;
    public Sprite tec9Sprite;
    public Sprite umpSprite;
    public Sprite uziSprite;
    public Sprite ak12Sprite;
    public Sprite ak74Sprite;
    public Sprite g3a4Sprite;
    public Sprite g36cSprite;
    public Sprite flamethrowerSprite;
    public Sprite glock17Sprite;
    public Sprite minigunSprite;
	public Sprite wallSprite;
    public Sprite metalSprite;
    public Sprite woodDoorSprite;
    public Sprite metalDoorSprite;

	[Header("Amounts")]
	public int ammoAmount;
    public int medkitAmount;
    public int bandageAmount;
    [Space]
    public int grenadeAmount;
    public int molotovAmount;
    public int smokeAmount;
    public int landmineAmount;
    public int chickenAmount;
    public int fuelAmount;
    [Space]
    public int woodAmount;
    public int stoneAmount;
    public int wallAmount;
    public int metalWallAmount;
    public int woodDoorAmount;
    public int metalDoorAmount;
    [Space]
    public int krissAmount;
    public int mp7Amount;
    public int mp5Amount;
    public int ump45Amount;
    public int tec9Amount;
    public int uziAmount;
    public int ak12Amount;
    public int ak74Amount;
    public int g3a4Amount;
    public int g36cAmount;
    public int flamethrowerAmount;
    public int glock17Amount;
    [Space]
    public int civils;
    public int survivals;
    public int vehiclesIndex;
    public int dogIndex;

    Inventory inventory;
    PlayfabManager database;
    DataImporter data;
    Player player;

    void Start()
    {
        //civils = database.civils;
        //vehiclesIndex = database.vehiclesIndex;
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        data = GetComponent<DataImporter>();
        //database.GetData();
    }

    void Update()
    {
        inventory = GetComponent<Player>().inventory;
        player = GetComponent<Player>();
        
        if(data.dataLoaded)
        CheckItem();
    }

	void CheckItem()
    {
        itemList = GetComponent<Player>().inventory.itemList;

        ammoAmount = inventory.ammoAmount;
        medkitAmount = inventory.medkitAmount;
        bandageAmount = inventory.bandageAmount;
        grenadeAmount = inventory.grenadeAmount;
        molotovAmount = inventory.molotovAmount;
        smokeAmount = inventory.smokeAmount;
        landmineAmount = inventory.landmineAmount;
        chickenAmount = inventory.chickenAmount;
        woodAmount = inventory.woodAmount;
        stoneAmount = inventory.stoneAmount;
        wallAmount = inventory.wallAmount;
        metalWallAmount = inventory.metalWallAmount;
        woodDoorAmount = inventory.woodDoorAmount;
        metalDoorAmount = inventory.metalDoorAmount;
        krissAmount = inventory.krissAmount;
        mp7Amount = inventory.mp7Amount;
        mp5Amount = inventory.mp5Amount;
        ump45Amount = inventory.ump45Amount;
        tec9Amount = inventory.tec9Amount;
        uziAmount = inventory.uziAmount;
        ak12Amount = inventory.ak12Amount;
        ak74Amount = inventory.ak74Amount;
        g3a4Amount = inventory.g3a4Amount;
        g36cAmount = inventory.g36cAmount;
        flamethrowerAmount = inventory.flamethrowerAmount;
        glock17Amount = inventory.glock17Amount;
    }

    public void RemoveItems()
    {
        database.SendData("Item Ammo", 0.ToString());
        database.SendData("Item Medkit", 0.ToString());
        database.SendData("Item Bandage", 0.ToString());
        database.SendData("Item Grenade", 0.ToString());
        database.SendData("Item Molotov", 0.ToString());
        database.SendData("Item Smoke", 0.ToString());
        database.SendData("Item Landmine", 0.ToString());
        database.SendData("Item Chicken", 0.ToString());
        database.SendData("Item Stone", 0.ToString());
        database.SendData("Item Wood", 0.ToString());
        database.SendData("Item WoodWall", 0.ToString());
        database.SendData("Item MetalWall", 0.ToString());
        database.SendData("Item WoodDoor", 0.ToString());
        database.SendData("Item MetalDoor", 0.ToString());
        database.SendData("Item Fuel", 0.ToString());
        database.SendData("Weapon Kriss", 0.ToString());
        database.SendData("Weapon MP7", 0.ToString());
        database.SendData("Weapon MP5", 0.ToString());
        database.SendData("Weapon UMP", 0.ToString());
        database.SendData("Weapon Tec9", 0.ToString());
        database.SendData("Weapon UZI", 0.ToString());
        database.SendData("Weapon AK12", 0.ToString());
        database.SendData("Weapon AK74", 0.ToString());
        database.SendData("Weapon G3A4", 0.ToString());
        database.SendData("Weapon G36C", 0.ToString());
        database.SendData("Weapon Flamethrower", 0.ToString());
        database.SendData("Weapon Glock17", 0.ToString());
        database.SendData("Weapon Minigun", 0.ToString());
    }
}
