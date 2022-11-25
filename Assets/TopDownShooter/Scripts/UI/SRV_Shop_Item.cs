using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SRV_Shop_Item : MonoBehaviour
{
    public SurvivalShop srvShop;
    [Space]
    public bool kriss;
    public bool mp7;
    public bool mp5;
    public bool ump45;
    public bool tec9;
    public bool uzi;
    public bool ak12;
    public bool ak74;
    public bool g3a4;
    public bool g36c;
    public bool flamethrower;
    public bool glock17;

    public TMP_Text amountTXT;

    PlayfabManager database;
    Player player;
    Inventory inventory;
    ItemAssets itemAssets;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = player.inventory;
        itemAssets = player.GetComponent<ItemAssets>();

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(kriss)
        {
            amountTXT.text = itemAssets.krissAmount.ToString();

            if (itemAssets.krissAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (mp7)
        {
            amountTXT.text = itemAssets.mp7Amount.ToString();

            if (itemAssets.mp7Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (mp5)
        {
            amountTXT.text = itemAssets.mp5Amount.ToString();

            if (itemAssets.mp5Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (ump45)
        {
            amountTXT.text = itemAssets.ump45Amount.ToString();

            if (itemAssets.ump45Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (tec9)
        {
            amountTXT.text = itemAssets.tec9Amount.ToString();

            if (itemAssets.tec9Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (uzi)
        {
            amountTXT.text = itemAssets.uziAmount.ToString();

            if (itemAssets.uziAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (ak12)
        {
            amountTXT.text = itemAssets.ak12Amount.ToString();

            if (itemAssets.ak12Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (ak74)
        {
            amountTXT.text = itemAssets.ak74Amount.ToString();

            if (itemAssets.ak74Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (g3a4)
        {
            amountTXT.text = itemAssets.g3a4Amount.ToString();

            if (itemAssets.g3a4Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (g36c)
        {
            amountTXT.text = itemAssets.g36cAmount.ToString();

            if (itemAssets.g36cAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (flamethrower)
        {
            amountTXT.text = itemAssets.flamethrowerAmount.ToString();

            if (itemAssets.flamethrowerAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        if (glock17)
        {
            amountTXT.text = itemAssets.glock17Amount.ToString();

            if (itemAssets.glock17Amount <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
