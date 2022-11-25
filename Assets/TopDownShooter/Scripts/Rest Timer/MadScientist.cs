using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MadScientist : MonoBehaviour
{
    public TMP_Text Time;
    public TMP_Text Name;
    public float msToWait;
    public Button ClickButton;
    public Button ClaimButton;
    private ulong lastTimeClicked;
    public PlayfabManager database;

    DataImporter dataImporter;

    private void Start()
    {
        database = PlayfabManager.database;
        dataImporter = FindObjectOfType<DataImporter>();

        lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("MadClicked"));

        ClaimButton.interactable = false;

        if (!Ready())
            ClickButton.interactable = false;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("MadRest") == 1 && Ready())
        {
            ClaimButton.interactable = true;
        }
        else
        {
            ClaimButton.interactable = false;
        }

        if (!ClickButton.IsInteractable())
        {
            if (Ready())
            {
                ClickButton.interactable = true;
                Time.text = "Ready!";
                return;
            }
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            //HOURS
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //MINUTES
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //SECONDS
            r += (secondsLeft % 60).ToString("00") + "s";
            Time.text = r;
        }

        if (database.srvRest > 0)
        {
            Name.text = "Madscientist " + " X " + database.srvRest.ToString();
        }
        else
        {
            Name.text = "Madscientist : ";
        }
    }


    public void Click()
    {
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("MadClicked", lastTimeClicked.ToString());
        ClickButton.interactable = false;


        PlayerPrefs.SetInt("MadRest", 1);

    }
    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            //DO SOMETHING WHEN TIMER IS FINISHED
            return true;
        }

        return false;
    }

    public void Claim()
    {
        if (PlayerPrefs.GetInt("MadRest") == 1)
        {
            database.srvAmount += database.srvRest;
            database.srvRest = 0;

            database.SendData("SRV Survival", database.srvAmount.ToString());
            database.SendData("SRV Rest", "0");

            dataImporter.LoadSRV();

            PlayerPrefs.SetInt("MadRest", 0);

            gameObject.SetActive(false);
        }
    }
}
