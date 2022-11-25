using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrenchGuy : MonoBehaviour
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

        lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("FrenchClicked"));

        ClaimButton.interactable = false;

        if (!Ready())
            ClickButton.interactable = false;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("FrenchRest") == 1 && Ready())
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

        if (database.srvFrenchFry_Rest > 0)
        {
            Name.text = "French Guy " + " X " + database.srvFrenchFry_Rest.ToString();
        }
        else
        {
            Name.text = "French Guy : ";
        }
    }


    public void Click()
    {
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("FrenchClicked", lastTimeClicked.ToString());
        ClickButton.interactable = false;


        PlayerPrefs.SetInt("FrenchRest", 1);

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
        if (PlayerPrefs.GetInt("FrenchRest") == 1)
        {
            database.srvFrenchFry += database.srvFrenchFry_Rest;
            database.srvFrenchFry_Rest = 0;

            database.SendStats();

            dataImporter.LoadSRV();

            PlayerPrefs.SetInt("FrenchRest", 0);

            gameObject.SetActive(false);
        }
    }
}
