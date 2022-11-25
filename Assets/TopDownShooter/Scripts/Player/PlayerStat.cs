using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    public ParticleSystem upgradeVFX;
    public AudioSource sfx;
    public AudioClip upgradeSFX;
    public int strength;
    public int stamina;
    public int endurance;
    public int accuracy;
    public int speed;
    [Space]
    public int[] minMaxPrize;

    [Header("UI")]
    public Slider strengthSlider;
    public Slider staminaSlider;
    public Slider enduranceSlider;
    public Slider accuracySlider;
    public Slider speedSlider;
    [Space]
    public TMP_Text strengthAmountTXT;
    public TMP_Text staminaAmountTXT;
    public TMP_Text enduranceAmountTXT;
    public TMP_Text accuracyAmountTXT;
    public TMP_Text speedAmountTXT;
    [Space]
    public TMP_Text strengthTXT;
    public TMP_Text staminaTXT;
    public TMP_Text enduranceTXT;
    public TMP_Text accuracyTXT;
    public TMP_Text speedTXT;

    PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        strength = database.playerStat[0].strength;
        stamina = database.playerStat[0].stamina;
        endurance = database.playerStat[0].endurance;
        accuracy = database.playerStat[0].accuracy;
        speed = database.playerStat[0].speed;
    }

    // Update is called once per frame
    void Update()
    {
        strengthSlider.value = strength;
        staminaSlider.value = stamina;
        enduranceSlider.value = endurance;
        accuracySlider.value = accuracy;
        speedSlider.value = speed;

        strengthAmountTXT.text = SetPrizeMoney(strength).ToString();
        staminaAmountTXT.text = SetPrizeMoney(stamina).ToString();
        enduranceAmountTXT.text = SetPrizeMoney(endurance).ToString();
        accuracyAmountTXT.text = SetPrizeMoney(accuracy).ToString();
        speedAmountTXT.text = SetPrizeMoney(speed).ToString();

        /*strengthTXT.text = "Strength : " + 
        staminaTXT.text = "Stamina : " + 
        enduranceTXT.text = "Endurance : " + 
        accuracyTXT.text = "Accuracy : " + 
        speedTXT.text = "Speed : " + */

        strengthTXT.text = "Strength : " + strength.ToString() + "/20";
        staminaTXT.text = "Stamina : " + stamina.ToString() + "/20";
        enduranceTXT.text = "Endurance : " + endurance.ToString() + "/20";
        accuracyTXT.text = "Accuracy : " + accuracy.ToString() + "/20";
        speedTXT.text = "Speed : " + speed.ToString() + "/20";
    }

    public void Click_Strength(int value)
    {
        strength += value;

        upgradeVFX.Play();
        sfx.PlayOneShot(upgradeSFX);

        database.playerStat[0].strength = strength;

        database.coins -= SetPrizeMoney(strength);

        database.SaveSRV();
        database.SendData("Coins", database.coins.ToString());
        
    }

    public void Click_Stamina(int value)
    {
        stamina += value;

        upgradeVFX.Play();
        sfx.PlayOneShot(upgradeSFX);

        database.playerStat[0].stamina = stamina;

        database.coins -= SetPrizeMoney(strength);

        database.SaveSRV();
        database.SendData("Coins", database.coins.ToString());
    }

    public void Click_Endurance(int value)
    {
        endurance += value;
        upgradeVFX.Play();
        sfx.PlayOneShot(upgradeSFX);

        database.playerStat[0].endurance = endurance;

        database.coins -= SetPrizeMoney(strength);

        database.SaveSRV();
        database.SendData("Coins", database.coins.ToString());
    }

    public void Click_Accuracy(int value)
    {
        accuracy += value;
        upgradeVFX.Play();
        sfx.PlayOneShot(upgradeSFX);

        database.coins -= SetPrizeMoney(strength);

        database.playerStat[0].accuracy = accuracy;

        database.SaveSRV();
        database.SendData("Coins", database.coins.ToString());
    }

    public void Click_Speed(int value)
    {
        speed += value;
        upgradeVFX.Play();
        sfx.PlayOneShot(upgradeSFX);

        database.coins -= SetPrizeMoney(strength);

        database.playerStat[0].speed = speed;

        database.SaveSRV();
        database.SendData("Coins", database.coins.ToString());
    }

    public int SetPrizeMoney(int upgrade)
    {

        return minMaxPrize[upgrade];
    }


}
