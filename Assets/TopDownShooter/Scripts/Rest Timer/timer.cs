using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    public TMP_Text Time;
    public float msToWait;
    public Button ClickButton;
    private ulong lastTimeClicked;
    public bool isNotProssecing;

    private void Start()
    {

        lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));

        if (!Ready())
            isNotProssecing = false;
    }

    private void Update()
    {
        if (!isNotProssecing)
        {
            if (Ready())
            {
                isNotProssecing = true;
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
    }


    public void Click()
    {
        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        isNotProssecing = false;


        PlayerPrefs.SetInt("BidenRest", 1);

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
        if(PlayerPrefs.GetInt("BidenRest") == 1)
        {

        }
    }
}



