using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SRV_Customize : MonoBehaviour
{
    PlayfabManager database;
    DataImporter data;
    public Survivor srv;
    public bool equipped;
    public Survivor[] srvs;

    [Header("Slot")]
    public int slotIndex;

    [Header("Username")]
    public string name;
    public TMP_Text usernameTXT;
    public TMP_InputField usernameInput;

    [Header("Avatar")]
    public Image avatar;
    public Sprite[] allAvatars;
    public int avatarIndex;

    [Header("UI")]
    public TMP_Text equipTXT;
    public CanvasGroup[] CanvasGRP;
    public TMP_Text ammoTXT;
    public TMP_Text grenadeTXT;
    public TMP_Text moloTXT;
    public TMP_Text medkitTXT;
    public TMP_Text bandageTXT;

    [Header("Weapon")]
    public Image weaponIMG;
    public Sprite[] allWeapons;
    public int weaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        data = FindObjectOfType<DataImporter>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            equipTXT.text = "Unequipped";

            if (slotIndex == 0)
            {
                database.srvEXP1 = 0;
            }

            if (slotIndex == 1)
            {
                database.srvEXP2 = 0;
            }

            if (slotIndex == 2)
            {
                database.srvEXP3 = 0;
            }

            if (slotIndex == 3)
            {
                database.srvEXP4 = 0;
            }

            if (slotIndex == 4)
            {
                database.srvEXP5 = 0;
            }

            equipped = false;
        }

        for (int i = 0; i < CanvasGRP.Length; i++)
        {
            CanvasGRP[i].alpha = 0f;
            CanvasGRP[i].blocksRaycasts = false;
        }

        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        weaponIndex = database.srvs_Boxes[slotIndex].weaponIndex;
        avatarIndex = database.srvs_Boxes[slotIndex].clothesIndex;
        EnableWeapon(weaponIndex);
        EnableAvatar(avatarIndex);

        if(data.dataLoaded)
        {
            srvs = FindObjectsOfType<Survivor>();

            for (int i = 0; i < srvs.Length; i++)
            {
                if(srvs[i].index == slotIndex)
                {
                    srv = srvs[i];
                }
            }
        }

        if(srv != null)
        {
            for (int i = 0; i < CanvasGRP.Length; i++)
            {
                CanvasGRP[i].alpha = 1f;
                CanvasGRP[i].blocksRaycasts = true;
            }
        }else
        {
            for (int i = 0; i < CanvasGRP.Length; i++)
            {
                CanvasGRP[i].alpha = 0f;
                CanvasGRP[i].blocksRaycasts = false;
            }
        }
    }


    public void GetData()
    {
        avatarIndex = database.srvs_Boxes[slotIndex].clothesIndex;
        weaponIndex = database.srvs_Boxes[slotIndex].weaponIndex;

        EnableAvatar(avatarIndex);
        EnableWeapon(weaponIndex);

        usernameTXT.text = database.srvs_Boxes[slotIndex].nameInput;
        name = database.srvs_Boxes[slotIndex].nameInput;

    }

    public void SendData()
    {
        database.srvs_Boxes[slotIndex].clothesIndex = avatarIndex;
        database.srvs_Boxes[slotIndex].weaponIndex = weaponIndex;

        database.srvs_Boxes[slotIndex].nameInput = usernameInput.text;

        srv.Switch();

        database.SaveSRV();
    }

    public void EnableAvatar(int index)
    {
        avatar.sprite = allAvatars[index];
    }

    public void EnableWeapon(int index)
    {
        weaponIMG.sprite = allWeapons[index];
    }

    public void Equip()
    {
        if(!equipped)
        {
            equipTXT.text = "Equipped";

            if(slotIndex == 0)
            {
                database.srvEXP1 = 1;
            }

            if (slotIndex == 1)
            {
                database.srvEXP2 = 1;
            }

            if (slotIndex == 2)
            {
                database.srvEXP3 = 1;
            }

            if (slotIndex == 3)
            {
                database.srvEXP4 = 1;
            }

            if (slotIndex == 4)
            {
                database.srvEXP5 = 1;
            }

            equipped = true;
        }else
        {
            equipTXT.text = "Unequipped";

            if (slotIndex == 0)
            {
                database.srvEXP1 = 0;
            }

            if (slotIndex == 1)
            {
                database.srvEXP2 = 0;
            }

            if (slotIndex == 2)
            {
                database.srvEXP3 = 0;
            }

            if (slotIndex == 3)
            {
                database.srvEXP4 = 0;
            }

            if (slotIndex == 4)
            {
                database.srvEXP5 = 0;
            }

            equipped = false;
        }
    }
}
