using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuImporter : MonoBehaviour
{
    PlayfabManager database;
    public TMP_Text moneyTXT;
    public int money;

    public bool loaded;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        database.GetData();

        yield return new WaitForSeconds(0.5f);

        GetData();
    }

    void GetData()
    {
        loaded = true;
    }

    private void Update()
    {
        if(loaded)
        moneyTXT.text = ": " + database.coins.ToString("0");
    }

}