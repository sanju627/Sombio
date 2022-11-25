using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SRV_RestTime : MonoBehaviour
{
    public bool inProgress;
    private DateTime TimeStart;
    private DateTime TimeEnd;
    private Coroutine lastTimer;
    private Coroutine lastDisplay;

    [Header("Pruduction Time")]
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    [Header("UI")]
    public GameObject window;
    public TMP_Text startTimeTXT;
    public TMP_Text endTimeTXT;
    public GameObject timeLeftOBJ;
    public TMP_Text timeLeftTXT;
    public Slider timeLeftSlider;
    public Button skipBTN;
    public Button startBTN;

    #region UnityMethods

    private void Awake()
    {
        //SaveData.current = (SaveData) Serialization.Load(Application.persistentDataPath + "/saves/save1.save");
    }

    private void Start()
    {
        startBTN.onClick.AddListener(StartTimer);
        skipBTN.onClick.AddListener(Skip);
    }

    #endregion

    #region UIMethod

    void Initializewindow()
    {
        if(inProgress)
        {
            startTimeTXT.text = "Start Time: \n" + TimeStart;
            endTimeTXT.text = "End Time: \n" + TimeEnd;

            timeLeftOBJ.SetActive(true);
            lastDisplay = StartCoroutine(DisplayTime());

            startBTN.gameObject.SetActive(false);
            skipBTN.gameObject.SetActive(true);

        }
        else
        {
            startTimeTXT.text = "Start Time: ";
            endTimeTXT.text = "End Time: ";

            timeLeftOBJ.SetActive(false);
        }
        

    }

    IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimeEnd - start;
        double totalSecLeft = timeLeft.TotalSeconds;
        double totalSec = (TimeEnd - TimeStart).TotalSeconds;
        string text;

        while (window.activeSelf && timeLeftOBJ.activeSelf)
        {
            text = "";
            timeLeftSlider.value = 1 - Convert.ToSingle((TimeEnd - DateTime.Now).TotalSeconds / totalSec);

            if(totalSecLeft > 1)
            {
                if(timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if(timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Minutes + "s ";
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecLeft) + "s";
                }

                timeLeftTXT.text = text;

                totalSecLeft -= Time.deltaTime;
                yield return null;
            }else
            {
                timeLeftTXT.text = "Finished";
                skipBTN.gameObject.SetActive(false);
                inProgress = false;
                timeLeftSlider.value = 0f;
                break;
            }
        }

        yield return null;
    }

    public void openWindow()
    {
        window.SetActive(true);
        Initializewindow();
    }

    public void closeWindow()
    {
        window.SetActive(false);
    }

    #endregion

    #region TimedEvent

    void StartTimer()
    {
        TimeStart = DateTime.Now;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimeEnd = TimeStart.Add(time);
        inProgress = true;

        lastTimer = StartCoroutine(Timer());

        Initializewindow();
    }

    IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secToFinish = (TimeEnd - start).TotalSeconds;

        yield return new WaitForSeconds(Convert.ToSingle(secToFinish));

        inProgress = false;
        Debug.Log("Finished");
    }

    void Skip()
    {
        TimeEnd = DateTime.Now;
        inProgress = false;
        StopCoroutine(lastTimer);

        timeLeftTXT.text = "Finished";
        timeLeftSlider.value = 1;

        StopCoroutine(lastDisplay);
        skipBTN.gameObject.SetActive(false);
        startBTN.gameObject.SetActive(true);
    }

    #endregion
}
