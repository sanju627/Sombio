using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurvivalShop : MonoBehaviour
{
	public TMP_Text[] survivorsTXT;
	public GameObject[] borders;
    public GameObject[] Inventory;
    public Image[] InventoryItems;
	public int selectedSkin;
	public int selectedWeapon;
    public int index;
    public SRV_Box srvBox;
    [Space]
    public GameObject weaponPanel;
    public GameObject customizePanel;
    [Space]
    public TMP_Text buy_equip_TXT;
    [Space]
    public GameObject BuyBTN;
    public GameObject EquipBTN;
    [Space]
    public Sprite disableSprite;
    public Sprite enableSprite;

    [Header("UI")]
    public TMP_Text coinsTXT;
    public GameObject[] srvSlot;
    public GameObject[] srvCustomPanel;
    [Space]
    public Sprite selectedSprite;
    public Sprite disabledSprite;

    PlayfabManager database;
    Player player;
    Inventory inventory;
    ItemAssets itemAssets;
    popTXT msgTXT;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = player.inventory;
        itemAssets = player.GetComponent<ItemAssets>();

        msgTXT = FindObjectOfType<popTXT>();

        weaponPanel.SetActive(false);
        customizePanel.SetActive(false);

        for (int i = 0; i < survivorsTXT.Length; i++)
        {
            survivorsTXT[i].text = "";
        }

        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].SetActive(false);
        }

        for (int i = 0; i < InventoryItems.Length; i++)
        {
            InventoryItems[i].sprite = disabledSprite;
        }

        /*for (int i = 0; i < srvSlot.Length; i++)
        {
            srvSlot[i].SetActive(false);
        }

        for (int i = 0; i < srvCustomPanel.Length; i++)
        {
            srvCustomPanel[i].SetActive(false);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        coinsTXT.text = database.coins.ToString("0");

        if (itemAssets.krissAmount > 0)
        {
            EnableWeapon(1);
        }
        else if (itemAssets.krissAmount <= 0)
        {
            DisableWeapon(1);
        }

        if (itemAssets.mp7Amount > 0)
        {
            EnableWeapon(2); 
        }
        else if (itemAssets.mp7Amount <= 0)
        {
            DisableWeapon(2);
        }

        if (itemAssets.mp5Amount > 0)
        {
            EnableWeapon(3);
        }
        else if (itemAssets.mp5Amount <= 0)
        {
            DisableWeapon(3);
        }

        if (itemAssets.tec9Amount > 0)
        {
            EnableWeapon(4);
        }
        else if (itemAssets.tec9Amount <= 0)
        {
            DisableWeapon(4);
        }

        if (itemAssets.ump45Amount > 0)
        {
            EnableWeapon(5);
        }
        else if (itemAssets.ump45Amount <= 0)
        {
            DisableWeapon(5);
        }

        if (itemAssets.uziAmount > 0)
        {
            EnableWeapon(6);
        }
        else if (itemAssets.uziAmount <= 0)
        {
            DisableWeapon(6);
        }

        if (itemAssets.ak12Amount > 0)
        {
            EnableWeapon(7);
        }
        else if (itemAssets.ak12Amount <= 0)
        {
            DisableWeapon(7);
        }

        if (itemAssets.ak74Amount > 0)
        {
            EnableWeapon(8);
        }
        else if (itemAssets.ak74Amount <= 0)
        {
            DisableWeapon(8);
        }        

        if (itemAssets.g3a4Amount > 0)
        {
            EnableWeapon(9);
        }
        else if (itemAssets.g3a4Amount <= 0)
        {
            DisableWeapon(9);
        }

        if (itemAssets.g36cAmount > 0)
        {
            EnableWeapon(10);
        }
        else if (itemAssets.g36cAmount <= 0)
        {
            DisableWeapon(10);
        }

        if (itemAssets.flamethrowerAmount > 0)
        {
            EnableWeapon(11);
        }
        else if (itemAssets.flamethrowerAmount <= 0)
        {
            DisableWeapon(11);
        }
    }

    public void EnableSRV(int index)
    {
        srvSlot[index].SetActive(true);
        srvCustomPanel[index].SetActive(true);
    }

    public void SelectSRV_Skin(int choice)
    {
    	for(int i = 0; i < survivorsTXT.Length; i++)
    	{
            survivorsTXT[i].text = "";
    	}

        survivorsTXT[choice].text = "Selected";

        selectedSkin = choice;

        SwitchBTN(choice);
    }

    public void SelectWeapon(int choice)
    {
        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].SetActive(false);
        }
        selectedWeapon = choice;
        borders[choice].SetActive(true);
    }

    public void SelectInventoryItem(int choice)
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            InventoryItems[i].sprite = disabledSprite;
        }
        selectedWeapon = choice;
        InventoryItems[choice].sprite = enableSprite;
    }

    public void EnableWeapon(int choice)
    {
        Inventory[choice].SetActive(true);        
    }

    public void SaveSRV()
    {
        database.SaveSRV();
    }
    
    public void Equip()
    {
        srvBox = database.srvs_Boxes[index];

        SendToInventory(srvBox.weaponIndex, true);

        database.srvs_Boxes[index].weaponIndex = selectedWeapon;

        SendToInventory(srvBox.weaponIndex, false);

    }

    public void Equip_Skin()
    {
        database.srvs_Boxes[index].clothesIndex = selectedSkin;

    }

    public void SendToInventory(int choice, bool add)
    {
        if (choice == 0)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.glock17, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.glock17, amount = 1 });

            database.SendData("Weapon Glock17", itemAssets.glock17Amount.ToString());
        }

        if (choice == 1)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.kriss, amount = 1 });
            else if (!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.kriss, amount = 1 });

            database.SendData("Weapon Kriss", itemAssets.krissAmount.ToString());
        }

        if (choice == 2)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.mp7, amount = 1 });
            else if (!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.mp7, amount = 1 });

            database.SendData("Weapon MP7", itemAssets.mp7Amount.ToString());
        }

        if (choice == 3)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.mp5, amount = 1 });
            else if (!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.mp5, amount = 1 });

            database.SendData("Weapon MP5", itemAssets.mp5Amount.ToString());
        }

        if (choice == 4)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.ump45, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ump45, amount = 1 });

            database.SendData("Weapon UMP", itemAssets.ump45Amount.ToString());
        }

        if (choice == 5)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.tec9, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.tec9, amount = 1 });

            database.SendData("Weapon Tec9", itemAssets.tec9Amount.ToString());
        }

        if (choice == 6)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.uzi, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.uzi, amount = 1 });

            database.SendData("Weapon UZI", itemAssets.uziAmount.ToString());
        }

        if (choice == 7)
        {
            if (add)
                inventory.AddItem(new Item { itemType = Item.ItemType.ak12, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ak12, amount = 1 });

            database.SendData("Weapon AK12", itemAssets.ak12Amount.ToString());
        }

        if (choice == 8)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.ak74, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ak74, amount = 1 });

            database.SendData("Weapon AK74", itemAssets.ak74Amount.ToString());
        }

        if (choice == 9)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.g3a4, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.g3a4, amount = 1 });

            database.SendData("Weapon G3A4", itemAssets.g3a4Amount.ToString());
        }

        if (choice == 10)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.g36c, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.g36c, amount = 1});

            database.SendData("Weapon G36C", itemAssets.g36cAmount.ToString());
        }

        if (choice == 11)
        {
            if(add)
                inventory.AddItem(new Item { itemType = Item.ItemType.flamethrower, amount = 1 });
            else if(!add)
                inventory.RemoveItem(new Item { itemType = Item.ItemType.flamethrower, amount = 1 });

            database.SendData("Weapon Flamethrower", itemAssets.flamethrowerAmount.ToString());
        }

        Debug.Log("Switched");
    }

    public void DisableWeapon(int choice)
    {
        Inventory[choice].SetActive(false);
    }

    public void ClickWeapon(int index)
    {
        weaponPanel.SetActive(true);
        customizePanel.SetActive(false);
        this.index = index;
    }

    public void ClickSRV_Skin(int index)
    {
        weaponPanel.SetActive(false);
        customizePanel.SetActive(true);
        this.index = index;
    }

    public void DisableCustom()
    {
        weaponPanel.SetActive(false);
        customizePanel.SetActive(false);
    }

    public void SwitchBTN(int choice)
    {
        if (choice == 0)
        {
            if (database.srvs_items[0].srv_Biden > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 1)
        {
            if (database.srvs_items[0].srv_Cowboy > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 2)
        {
            if (database.srvs_items[0].srv_Fox > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 3)
        {
            if (database.srvs_items[0].srv_FrenchFries > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 4)
        {
            if (database.srvs_items[0].srv_MadScientist > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 5)
        {
            if (database.srvs_items[0].srv_Pickle > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 6)
        {
            if (database.srvs_items[0].srv_Pirate > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 7)
        {
            if (database.srvs_items[0].srv_RomanCaeser > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 8)
        {
            if (database.srvs_items[0].srv_SoldierBlue > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 9)
        {
            if (database.srvs_items[0].srv_SoldierRed > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 10)
        {
            if (database.srvs_items[0].srv_STPFemale > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 11)
        {
            if (database.srvs_items[0].srv_STPMale > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 12)
        {
            if (database.srvs_items[0].srv_SWAT > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }

        if (choice == 13)
        {
            if (database.srvs_items[0].srv_Trump > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else
            {
                EquipBTN.SetActive(false);
                BuyBTN.SetActive(true);
            }
        }
    }

    public void PurchaseBTN()
    {
        int choice = selectedSkin;

        if (selectedSkin == 0)
        {
            if (database.srvs_items[0].srv_Biden > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if(database.srvs_items[0].srv_Biden <= 0)
            {
                if(database.coins >= 800)
                {
                    database.srvs_items[0].srv_Biden = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 1)
        {
            if (database.srvs_items[0].srv_Cowboy > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_Cowboy <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_Cowboy = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 2)
        {
            if (database.srvs_items[0].srv_Fox > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_Fox <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_Fox = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 3)
        {
            if (database.srvs_items[0].srv_FrenchFries > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_FrenchFries <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_FrenchFries = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 4)
        {
            if (database.srvs_items[0].srv_MadScientist > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_MadScientist <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_MadScientist = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 5)
        {
            if (database.srvs_items[0].srv_Pickle > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_MadScientist <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_MadScientist = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 6)
        {
            if (database.srvs_items[0].srv_Pirate > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_Pirate <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_Pirate = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 7)
        {
            if (database.srvs_items[0].srv_RomanCaeser > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_RomanCaeser <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_RomanCaeser = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 8)
        {
            if (database.srvs_items[0].srv_SoldierBlue > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_SoldierBlue <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_SoldierBlue = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 9)
        {
            if (database.srvs_items[0].srv_SoldierRed > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_SoldierRed <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_SoldierRed = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 10)
        {
            if (database.srvs_items[0].srv_STPFemale > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_STPFemale <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_STPFemale = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 11)
        {
            if (database.srvs_items[0].srv_STPMale > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_STPMale <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_STPMale = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (selectedSkin == 12)
        {
            if (database.srvs_items[0].srv_SWAT > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_SWAT <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_SWAT = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }

        if (choice == 13)
        {
            if (database.srvs_items[0].srv_Trump > 0)
            {
                EquipBTN.SetActive(true);
                BuyBTN.SetActive(false);
            }
            else if (database.srvs_items[0].srv_Trump <= 0)
            {
                if (database.coins >= 800)
                {
                    database.srvs_items[0].srv_Trump = 1;
                    database.coins -= 800;

                    EquipBTN.SetActive(true);
                    BuyBTN.SetActive(false);

                    database.SaveSRV();
                }
                else
                {
                    msgTXT.PopText("Not Enough Coins");
                }
            }
        }
    }
}
