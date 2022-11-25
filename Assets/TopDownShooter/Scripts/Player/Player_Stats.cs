using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skills
{
    public int strength;
    public int stamina;
    public int endurance;
    public int accuracy;
    public int speed;


    public Player_Skills(int strength, int stamina, int endurance, int accuracy, int speed)
    {
        this.strength = strength;
        this.stamina = stamina;
        this.endurance = endurance;
        this.accuracy = accuracy;
        this.speed = speed;
    }
}

public class Player_Stats : MonoBehaviour
{
    public int strength;
    public int stamina;
    public int endurance;
    public int accuracy;
    public int speed;

    public Player_Skills ReturnClass()
    {
        return new Player_Skills(strength, stamina, endurance, accuracy, speed);
    }

    public void SetUI(Player_Skills skill)
    {
        strength = skill.strength;
        stamina = skill.stamina;
        endurance = skill.endurance;
        accuracy = skill.accuracy;
        speed = skill.speed;
    }
}
