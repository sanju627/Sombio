using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SRV_Select : MonoBehaviour
{
    [Header("Slot")]
    public int slotIndex;

    [Header("Username")]
    public TMP_Text usernameTXT;

    [Header("Avatar")]
    public Image avatar;
    public Sprite[] allAvatars;

    PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();


    }

    // Update is called once per frame
    void Update()
    {
        GetData();
    }

    public void GetData()
    {
        avatar.sprite = allAvatars[database.srvs_Boxes[slotIndex].clothesIndex];
        //usernameTXT.text = database.srvs_Boxes[slotIndex].nameInput;
    }
}
