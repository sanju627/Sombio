using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RestingTime : MonoBehaviour
{
    public float restingTime;
    public float currentTime;
    public float restingTimeMax;
    public TMP_Text restingTimeTXT;
    public Slider restSlider;

    bool completed;
    public PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("RestingTimeLoaded") != 1)
        {
            restingTime = restingTimeMax;
            
        }else
        {
            restingTime = PlayerPrefs.GetFloat("RestingTime");
        }

        restSlider.maxValue = restingTimeMax;
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) return;

        restingTime -= Time.deltaTime;

        restSlider.value = restingTime;

        restingTimeTXT.text = "Resting";

        currentTime = PlayerPrefs.GetInt("RestingTime");

        PlayerPrefs.SetFloat("RestingTime", restingTime);
        PlayerPrefs.SetInt("RestingTimeLoaded", 1);

        if (restingTime <= 0)
        {
            restingTimeTXT.text = "Rested";

            PlayerPrefs.DeleteAll();

            restSlider.gameObject.SetActive(false);

            database.srvAmount += 1;
            database.srvRest -= 1;
            database.SendData("SRV Survival", database.srvAmount.ToString());
            database.SendData("SRV Rest", database.srvRest.ToString());

            completed = true;
        }
    }
}
