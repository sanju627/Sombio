using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayfabManager : MonoBehaviour
{
    public TMP_Text infoTXT;
    public bool loaded;

    [Header("PlayerPrefs")]
    public string name;
    public int password;
    public SRV_Box[] srvs_Boxes;
    public SRV_Items[] srvs_items;
    public Player_Stats[] playerStat;

    [Header("UI")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button[] btn;

    [Header("Register")]
    public TMP_InputField usernameInput;
    public TMP_InputField RegisterEmailInput;
    public TMP_InputField RegisterPasswordInput;

    [Header("TXT")]
    public string username;

    [Header("Energy")]
    public int maxEnergy;
    public int coins;

    [Header("Costumes")]
    public int c_Chicken;
    public int c_Dino;
    public int c_Fry;
    public int c_GreenAlien;
    public int c_Poop;
    public int c_Bacon;
    public int c_Mad;
    public int c_RedFox;
    public int c_RomanCaeser;
    public int c_RomanLegion;
    public int c_Donald;
    public int c_Biden;
    public int c_RomanCenturian;
    public int c_RomanPraetorian;
    public int c_RomanScout;
    public int c_Burger;
    public int c_Donut;
    public int c_Fries;
    public int c_HotDog;
    public int c_MilitaryCamo;
    public int c_Military;
    public int c_MilitaryGreen;
    public int c_MilkShake;
    public int c_Pickle;
    public int c_StormTrooper;
    public int c_STPFemale;
    public int c_STPMale;
    public int c_SoldierBlue;
    public int c_SoldierRed;
    public int c_SoldierYellow;
    public int c_Cowboy;
    public int c_SWAT;
    public int c_Pirate;

    [Header("Cars")]
    public int car1940;
    public int carBlackHawk;
    public int carBubble;
    public int carHotrod;
    public int carIceCream;
    public int carMinivan;
    public int carMonsterTruck;
    public int carMuscle;
    public int carPickup;
    public int carPoop;
    public int carPork;
    public int carPrison;
    public int carSemi;
    public int carWater;
    public int carWiener;
    public int carAirship;

    [Header("Dog")]
    public int akira;
    public int husky;
    public int dogIndex;

    [Header("Item Value")]
    public int ammoAmount;
    public int bandageAmount;
    public int chickenAmount;
    public int grenadeAmount;
    public int landmineAmount;
    public int medkitAmount;
    public int metalDoorAmount;
    public int metalWallAmount;
    public int molotovAmount;
    public int woodDoorAmount;
    public int woodWallAmount;
    public int smokeAmount;
    public int stoneAmount;
    public int woodAmount;
    public int fuelAmount;

    [Header("SRV")]
    public int srvAmount;
    public int srvRedFox;
    public int srvRomanCaeser;
    public int srvRomanLegion;
    public int srvSTPMale;
    public int srvSTPFemale;
    public int srvSoldierRed;
    public int srvSoldierBlue;
    public int srvTrump;
    public int srvBiden;
    public int srvPickle;
    public int srvFrenchFry;
    public int srvCowboy;
    public int srvSWAT;
    public int srvPirate;
    [Space]
    public int srvRest;
    public int srvRedFox_Rest;
    public int srvRomanCaeser_Rest;
    public int srvRomanLegion_Rest;
    public int srvSTPMale_Rest;
    public int srvSTPFemale_Rest;
    public int srvSoldierRed_Rest;
    public int srvSoldierBlue_Rest;
    public int srvTrump_Rest;
    public int srvBiden_Rest;
    public int srvPickle_Rest;
    public int srvFrenchFry_Rest;
    public int srvCowboy_Rest;
    public int srvSWAT_Rest;
    public int srvPirate_Rest;
    [Space]
    public int civilAmount;
    [Space]
    public int srvEXP1;
    public int srvEXP2;
    public int srvEXP3;
    public int srvEXP4;
    public int srvEXP5;

    [Header("Weapons")]
    public int ak12Amount;
    public int ak74Amount;
    public int krissAmount;
    public int flamethrowerAmount;
    public int G36CAmount;
    public int G3A4Amount;
    public int Glock17Amount;
    public int MP5Amount;
    public int MP7Amount;
    public int Tec9Amount;
    public int UMPAmount;
    public int UZIAmount;
    public int MinigunAmount;
    public int AxeAmount;
    public int BasebatAmount;
    public int KatanaAmount;


    Menu menu;

    public static PlayfabManager database;

    private void Awake()
    {
        if(database == null)
        {
            database = this;
        }else
        {
            Destroy(this);
        }

        //LoadData();

        DontDestroyOnLoad(gameObject);


        if(PlayerPrefs.GetInt("SetID") == 1)
        {
            
            //DirectLogin();
        }

        

    }

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Menu>();
        //PlayerPrefs.DeleteAll();
        LoadSRV();
    }


    private void Update()
    {

    }

    #region login
    public void LoadData()
    {
        name = PlayerPrefs.GetString("Username");
        password = PlayerPrefs.GetInt("Password");
    }

    public void SaveID(string name, int pw)
    {
        PlayerPrefs.SetString("Username", name);
        PlayerPrefs.SetInt("Password", pw);
        PlayerPrefs.SetInt("SetID", 1);
    }

    public void RegisterBTN()
    {
        if(RegisterPasswordInput.text.Length < 6)
        {
            infoTXT.text = "Password is short";
            return;
        }

        if (usernameInput.text.Length <= 0)
        {
            infoTXT.text = "Missing Username";
            return;
        }

        if (RegisterEmailInput.text.Length <= 0)
        {
            infoTXT.text = "Missing Email";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = RegisterEmailInput.text,
            Password = RegisterPasswordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void LoginButton()
    {
        if (passwordInput.text.Length < 6)
        {
            infoTXT.text = "Password is short";
            return;
        }

        if (emailInput.text.Length <= 0)
        {
            infoTXT.text = "Missing Email";
            return;
        }

        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
                

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void DirectLogin()
    {
        LoadData();

        var request = new LoginWithEmailAddressRequest
        {
            Email = name,
            Password = password.ToString()
        };

        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = false;
        }


        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        infoTXT.text = "Logged In";

        GetSRVS();

        //SendData("Car Airship", "0");

        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = false;
        }

        int.TryParse(passwordInput.text, out password);
        SaveID(emailInput.text, password);

        name = PlayerPrefs.GetString("Username");
        password = PlayerPrefs.GetInt("Password");

        //SendData("Cloth CowBoy_Rest", "0");
        //SendData("Weapon Minigun", "0");

        //SendStats();
        GetData();
        //GetStatistics();
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        infoTXT.text = "Your Account Registered Successfully please login to continue";
        StartCoroutine(CreateData());

        SaveSRV();


        PlayerPrefs.DeleteAll();
        //GetStatistics();
    }

    public void ResetPasswordButton()
    {
        if (emailInput.text.Length <= 0)
        {
            infoTXT.text = "Missing Email";
            return;
        }

        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "5D42F"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        infoTXT.text = "Password Reset Mail Sended!!!";
    }

    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {
        //Debug.Log("Recieved");

        GetStatistics();
        if (result.Data != null)
        {
            //infoTXT.text = "Recieved";

            username = result.Data["Username"].Value;

            int.TryParse(result.Data["Coins"].Value, out coins);

            //---------------------------------------Clothes-----------------------------------------------//

            int.TryParse(result.Data["Cloth Chicken"].Value, out c_Chicken);
            int.TryParse(result.Data["Cloth Dino"].Value, out c_Dino);
            int.TryParse(result.Data["Cloth Fry"].Value, out c_Fry);
            int.TryParse(result.Data["Cloth GreenAlien"].Value, out c_GreenAlien);
            int.TryParse(result.Data["Cloth Poop"].Value, out c_Poop);
            int.TryParse(result.Data["Cloth Bacon"].Value, out c_Bacon);
            int.TryParse(result.Data["Cloth MadScientist"].Value, out c_Mad);
            int.TryParse(result.Data["Cloth RedFox"].Value, out c_RedFox);
            int.TryParse(result.Data["Cloth RomanCaeser"].Value, out c_RomanCaeser);
            int.TryParse(result.Data["Cloth RomanLegion"].Value, out c_RomanLegion);
            int.TryParse(result.Data["Cloth DonaldTrump"].Value, out c_Donald);
            int.TryParse(result.Data["Cloth JoeBidon"].Value, out c_Biden);
            int.TryParse(result.Data["Cloth RomanCenturion"].Value, out c_RomanCenturian);
            int.TryParse(result.Data["Cloth RomanPraetorion"].Value, out c_RomanPraetorian);
            int.TryParse(result.Data["Cloth RomanScout"].Value, out c_RomanScout);
            int.TryParse(result.Data["Cloth Burger"].Value, out c_Burger);
            int.TryParse(result.Data["Cloth Donut"].Value, out c_Donut);
            int.TryParse(result.Data["Cloth Fries"].Value, out c_Fries);
            int.TryParse(result.Data["Cloth Hotdog"].Value, out c_HotDog);
            int.TryParse(result.Data["Cloth MilitaryCamo"].Value, out c_MilitaryCamo);
            int.TryParse(result.Data["Cloth MilkShake"].Value, out c_MilkShake);
            int.TryParse(result.Data["Cloth Pickle"].Value, out c_Pickle);
            int.TryParse(result.Data["Cloth StormTrooper"].Value, out c_StormTrooper);
            int.TryParse(result.Data["Cloth STPFemale"].Value, out c_STPFemale);
            int.TryParse(result.Data["Cloth STPMale"].Value, out c_STPMale);
            int.TryParse(result.Data["Cloth SoldierBlue"].Value, out c_SoldierBlue);
            int.TryParse(result.Data["Cloth SoldierRed"].Value, out c_SoldierRed);
            int.TryParse(result.Data["Cloth SoldierYellow"].Value, out c_SoldierYellow);


            //---------------------------------------Car--------------------------------------//

            int.TryParse(result.Data["Car 1940"].Value, out car1940);
            int.TryParse(result.Data["Car BlackHawk"].Value, out carBlackHawk);
            int.TryParse(result.Data["Car Bubble"].Value, out carBubble);
            int.TryParse(result.Data["Car Hotrod"].Value, out carHotrod);
            int.TryParse(result.Data["Car IceCream"].Value, out carIceCream);
            int.TryParse(result.Data["Car Minivan"].Value, out carMinivan);
            int.TryParse(result.Data["Car MonsterTruck"].Value, out carMonsterTruck);
            int.TryParse(result.Data["Car Muscle"].Value, out carMuscle);
            int.TryParse(result.Data["Car Pickup"].Value, out carPickup);
            int.TryParse(result.Data["Car Semi"].Value, out carSemi);
            int.TryParse(result.Data["Car Airship"].Value, out carAirship);

            int.TryParse(result.Data["Item Ammo"].Value, out ammoAmount);
            int.TryParse(result.Data["Item Medkit"].Value, out medkitAmount);
            int.TryParse(result.Data["Item Bandage"].Value, out bandageAmount);
            int.TryParse(result.Data["Item Grenade"].Value, out grenadeAmount);
            int.TryParse(result.Data["Item Chicken"].Value, out chickenAmount);
            int.TryParse(result.Data["Item Landmine"].Value, out landmineAmount);
            int.TryParse(result.Data["Item MetalDoor"].Value, out metalDoorAmount);
            int.TryParse(result.Data["Item MetalWall"].Value, out metalWallAmount);
            int.TryParse(result.Data["Item WoodDoor"].Value, out woodDoorAmount);
            int.TryParse(result.Data["Item WoodWall"].Value, out woodWallAmount);
            int.TryParse(result.Data["Item Molotov"].Value, out molotovAmount);
            int.TryParse(result.Data["Item Smoke"].Value, out smokeAmount);
            int.TryParse(result.Data["Item Stone"].Value, out stoneAmount);
            int.TryParse(result.Data["Item Wood"].Value, out woodAmount);
            int.TryParse(result.Data["Item Fuel"].Value, out fuelAmount);

            //Vehicle Index, energy, dog Index
            


            int.TryParse(result.Data["Weapon AK12"].Value, out ak12Amount);
            int.TryParse(result.Data["Weapon AK74"].Value, out ak74Amount);
            int.TryParse(result.Data["Weapon Kriss"].Value, out krissAmount);
            int.TryParse(result.Data["Weapon Flamethrower"].Value, out flamethrowerAmount);
            int.TryParse(result.Data["Weapon G36C"].Value, out G36CAmount);
            int.TryParse(result.Data["Weapon G3A4"].Value, out G3A4Amount);
            int.TryParse(result.Data["Weapon Glock17"].Value, out Glock17Amount);
            int.TryParse(result.Data["Weapon MP5"].Value, out MP5Amount);
            int.TryParse(result.Data["Weapon MP7"].Value, out MP7Amount);
            int.TryParse(result.Data["Weapon Tec9"].Value, out Tec9Amount);
            int.TryParse(result.Data["Weapon UMP"].Value, out UMPAmount);
            int.TryParse(result.Data["Weapon UZI"].Value, out UZIAmount);
            int.TryParse(result.Data["Weapon Minigun"].Value, out MinigunAmount);

            int.TryParse(result.Data["Weapon melee_Axe"].Value, out AxeAmount);
            int.TryParse(result.Data["Weapon melee_Katana"].Value, out KatanaAmount);
            int.TryParse(result.Data["Weapon melee_basebat"].Value, out BasebatAmount);


            //SendStats();

            loaded = true;

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Login"))
                SceneManager.LoadScene("MenuEnvironment");
        }
        else
        {
            infoTXT.text = "Recieved but not loaded";
        }
    }

    IEnumerator CreateData()
    {
        SendData("Username", usernameInput.text);

        yield return new WaitForSeconds(0.01f);

        SendData("Item Ammo", "300");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Medkit", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Bandage", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Grenade", "1");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Chicken", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Landmine", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item MetalDoor", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item MetalWall", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item WoodDoor", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item WoodWall", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Molotov", "1");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Smoke", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Stone", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Wood", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Item Fuel", "0");


        SendData("Cloth Chicken", "1");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Dino", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Fry", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth GreenAlien", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Poop", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Bacon", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth MadScientist", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RedFox", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RomanCaeser", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RomanLegion", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth DonaldTrump", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth JoeBidon", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RomanCenturion", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RomanPraetorion", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth RomanScout", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Burger", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Donut", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Fries", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Hotdog", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth MilitaryCamo", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth MilkShake", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth Pickle", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth StormTrooper", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth STPFemale", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth STPMale", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth SoldierBlue", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth SoldierRed", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Cloth SoldierYellow", "0");


        SendData("Car 1940", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car BlackHawk", "0"); 
        yield return new WaitForSeconds(0.01f);
        SendData("Car Bubble", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Hotrod", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car IceCream", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Minivan", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car MonsterTruck", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Muscle", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Pickup", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Poop", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Pork", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Prison", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Water", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Wiener", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Semi", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Car Airship", "0");



        yield return new WaitForSeconds(0.01f);
        SendData("SRV Civils", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("SRV Survival", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("SRV Rest", "0");
        yield return new WaitForSeconds(0.01f);

        yield return new WaitForSeconds(0.01f);
        SendData("Weapon AK12", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon AK74", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon Kriss", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon Flamethrower", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon G36C", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon G3A4", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon Glock17", "1");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon MP5", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon MP7", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon Tec9", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon UMP", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon UZI", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon melee_Axe", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon melee_Katana", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon melee_basebat", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Weapon Minigun", "0");
        yield return new WaitForSeconds(0.01f);




        SendData("Dog Akira", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Dog Husky", "0");
        yield return new WaitForSeconds(0.01f);
        SendData("Coins", "200");

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, Reload);
    }

    public void rawDataSend(string name, string amount)
    {
        SendData(name, amount);
    }

    public void SendData(string name, string amount)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {name, amount}
            }            
        };
        Debug.Log("Sending");
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void SaveSRV()
    {
        List<Character> characters = new List<Character>();
        foreach (var item in srvs_Boxes)
        {
            characters.Add(item.ReturnClass());
        }

        List<SRV_Inventory> SRV_Inventory = new List<SRV_Inventory>();
        foreach (var srv_items in srvs_items)
        {
            SRV_Inventory.Add(srv_items.ReturnClass());
        }

        List<Player_Skills> player_skills = new List<Player_Skills>();
        foreach (var Player_Stats in playerStat)
        {
            player_skills.Add(Player_Stats.ReturnClass());
        }

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
               {"SRVS", JsonConvert.SerializeObject(characters)}
            }
        };

        var request_SRV_Item = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
               {"SRV_Inventory", JsonConvert.SerializeObject(SRV_Inventory)}
            }
        };

        var request_Player_Stat = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
               {"Player_Stat", JsonConvert.SerializeObject(player_skills)}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        PlayFabClientAPI.UpdateUserData(request_SRV_Item, OnDataSend, OnError);
        PlayFabClientAPI.UpdateUserData(request_Player_Stat, OnDataSend, OnError);
    }

    public void GetSRVS()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharacterDataRecieved, OnError);
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnSRVInventoryRecieved, OnError);
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnPlayerStatRecieved, OnError);
    }

    void OnCharacterDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved SRV Data");

        if(result.Data != null && result.Data.ContainsKey("SRVS"))
        {
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result.Data["SRVS"].Value);

            for (int i = 0; i < srvs_Boxes.Length; i++)
            {
                srvs_Boxes[i].SetUI(characters[i]);
            }
        }
    }

    void OnSRVInventoryRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved SRV Inventory");

        if (result.Data != null && result.Data.ContainsKey("SRV_Inventory"))
        {
            List<SRV_Inventory> SRV_Inventory = JsonConvert.DeserializeObject<List<SRV_Inventory>>(result.Data["SRV_Inventory"].Value);

            for (int i = 0; i < srvs_items.Length; i++)
            {
                srvs_items[i].SetUI(SRV_Inventory[i]);
            }
        }
    }

    void OnPlayerStatRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved Player Stat");

        if (result.Data != null && result.Data.ContainsKey("Player_Stat"))
        {
            List<Player_Skills> Player_Skills = JsonConvert.DeserializeObject<List<Player_Skills>>(result.Data["Player_Stat"].Value);

            for (int i = 0; i < srvs_items.Length; i++)
            {
                playerStat[i].SetUI(Player_Skills[i]);
            }
        }
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("User Data Send");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
        infoTXT.text = error.ErrorMessage;
    }

    void Reload(PlayFabError error)
    {
        SendData("Username", usernameInput.text);

        SendData("Item Ammo", "300");
        SendData("Item Medkit", "0");
        SendData("Item Bandage", "0");
        SendData("Item Grenade", "1");
        SendData("Item Chicken", "0");
        SendData("Item Landmine", "0");
        SendData("Item MetalDoor", "0");
        SendData("Item MetalWall", "0");
        SendData("Item WoodDoor", "0");
        SendData("Item WoodWall", "0");
        SendData("Item Molotov", "1");
        SendData("Item Smoke", "0");
        SendData("Item Stone", "0");
        SendData("Item Wood", "0");
        SendData("Item Fuel", "0");

        SendData("Cloth Chicken", "1");
        SendData("Cloth Dino", "0");
        SendData("Cloth Fry", "0");
        SendData("Cloth GreenAlien", "0");
        SendData("Cloth Poop", "0");
        SendData("Cloth Bacon", "0");
        SendData("Cloth MadScientist", "0");
        SendData("Cloth RedFox", "0");
        SendData("Cloth RomanCaeser", "0");
        SendData("Cloth RomanLegion", "0");
        SendData("Cloth DonaldTrump", "0");
        SendData("Cloth JoeBidon", "0");
        SendData("Cloth RomanCenturion", "0");
        SendData("Cloth RomanPraetorion", "0");
        SendData("Cloth RomanScout", "0");
        SendData("Cloth Burger", "0");
        SendData("Cloth Donut", "0");
        SendData("Cloth Fries", "0");
        SendData("Cloth Hotdog", "0");
        SendData("Cloth MilitaryCamo", "0");
        SendData("Cloth MilkShake", "0");
        SendData("Cloth Pickle", "0");
        SendData("Cloth StormTrooper", "0");
        SendData("Cloth STPFemale", "0");
        SendData("Cloth STPMale", "0");
        SendData("Cloth SoldierBlue", "0");
        SendData("Cloth SoldierRed", "0");
        SendData("Cloth SoldierYellow", "0");
        SendData("Cloth SoldierYellow", "0");
        SendData("Cloth Pirate", "0");
        SendData("Cloth SWAT", "0");
        SendData("Cloth CowBoy", "0");

        SendData("Car 1940", "0");
        SendData("Car BlackHawk", "0");
        SendData("Car Bubble", "0");
        SendData("Car Hotrod", "0");
        SendData("Car IceCream", "0");
        SendData("Car Minivan", "0");
        SendData("Car MonsterTruck", "0");
        SendData("Car Muscle", "0");
        SendData("Car Pickup", "0");
        SendData("Car Pork", "0");
        SendData("Car Prison", "0");
        SendData("Car Water", "0");
        SendData("Car Wiener", "0");
        SendData("Car Semi", "0");
        SendData("Car Airship", "0");


        SendData("Coins", "200");

        SendData("Weapon AK12", "0");
        SendData("Weapon AK74", "0");
        SendData("Weapon Kriss", "0");
        SendData("Weapon Flamethrower", "0");
        SendData("Weapon G36C", "0");
        SendData("Weapon G3A4", "0");
        SendData("Weapon Glock17", "1");
        SendData("Weapon MP5", "0");
        SendData("Weapon MP7", "0");
        SendData("Weapon Tec9", "0");
        SendData("Weapon UMP", "0");
        SendData("Weapon UZI", "0");
        SendData("Weapon Minigun", "0");

        SendData("Weapon melee_Axe", "1");
        SendData("Weapon melee_Katana", "0");
        SendData("Weapon melee_basebat", "0");


        SendData("Dog Akira", "0");
        SendData("Dog Husky", "0");

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, Reload);
    }
    #endregion login

    [Space]
    public int SRVCount;
    public int playerLevel;

    #region PlayerStats

    public void SendStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                

                
            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);

            switch(eachStat.StatisticName)
            {

            }
        }
            
    }

    #endregion PlayerStats

    public void UpdateSRV()
    {
        PlayerPrefs.SetInt("SRV1", srvEXP1);
        PlayerPrefs.SetInt("SRV2", srvEXP2);
        PlayerPrefs.SetInt("SRV3", srvEXP3);
        PlayerPrefs.SetInt("SRV4", srvEXP4);
        PlayerPrefs.SetInt("SRV5", srvEXP5);
    }

    public void LoadSRV()
    {
        srvEXP1 = PlayerPrefs.GetInt("SRV1");
        srvEXP2 = PlayerPrefs.GetInt("SRV2");
        srvEXP3 = PlayerPrefs.GetInt("SRV3");
        srvEXP4 = PlayerPrefs.GetInt("SRV4");
        srvEXP5 = PlayerPrefs.GetInt("SRV5");
    }

}
