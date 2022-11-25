using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SRV_Inventory_Item : MonoBehaviour
{
    public int amount;
    [Space]
    public bool medkit;
    public bool bandage;
    public bool chicken;
    public bool ak12;
    public bool ak74;
    public bool Flamethrower;
    public bool G36C;
    public bool Kriss;
    public bool MP5;
    public bool MP7;
    public bool Parafal;
    public bool Tec9;
    public bool UMP;
    public bool UZI;

    [Header("UI")]
    public TMP_Text amountTXT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        amountTXT.text = amount.ToString();
    }
}
