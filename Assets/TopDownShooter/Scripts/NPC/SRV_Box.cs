using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character
{
    public string name;
    public float health;
    public float hunger;
    public float armor;
    public int weapon;
    public int clothesIndex;


    public Character(string name, float health, float hunger, float armor, int weapon, int clothes)
    {
        this.name = name;
        this.health = health;
        this.hunger = hunger;
        this.armor = armor;
        this.weapon = weapon;
        this.clothesIndex = clothes;
    }
}

public class SRV_Box : MonoBehaviour
{
    public string nameInput;
    public float healthSlider;
    public float hungerSlider;
    public float armorSlider;
    public int weaponIndex;
    public int clothesIndex;
    public int ammo;

    public Character ReturnClass()
    {
        return new Character(nameInput, healthSlider, hungerSlider, armorSlider, weaponIndex, clothesIndex);
    }

    public void SetUI(Character character)
    {
        nameInput = character.name;
        hungerSlider = character.hunger;
        healthSlider = character.health;
        armorSlider = character.armor;
        weaponIndex = character.weapon;
        clothesIndex = character.clothesIndex;
    }
}
