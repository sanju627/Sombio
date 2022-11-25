using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using TMPro;

public enum ControlType{KeyBoard = 1, Touch = 2};

public class InputSystem : MonoBehaviour
{
	public ControlType control;
	public float Accel;
	public float Steer;
	public float Brake;
    [Space]
	public float reverseSpeed;
	public GameObject UI;
    public FixedJoystick joyStick;
	public void AccelInput(float input){Accel = input;}
	public void SteerInput(float input){Steer = input;}
	public void BrakeInput(float input){Brake = input;}

    [Header("Fuel")]
    public float maxFuel;
    public float currentFuel;
    public float fuelUsing;
    public int carIndex;

    [Header("CarTypes")]
    public bool C_1940;
    public bool C_BubbleCar;
    public bool Hotrod;
    public bool IceCreamTruck;
    public bool MiniVan;
    public bool MonsterTruck;
    public bool MuscleTruck;
    public bool PickupTruck;
    public bool PoopTruck;
    public bool PorkTruck;
    public bool PrisonTruck;
    public bool WaterTruck;
    public bool WienerTruck;
    public bool Semi;

    [Header("UI")]
    public TMP_Text fuelTXT;

    CarController car;
    PlayfabManager database;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<CarController>();

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        GetFuelData();

        //currentFuel = maxFuel;        

        //currentFuel = PlayerPrefs.GetFloat("Semi Fuel");
    }

    // Update is called once per frame
    void Update()
    {
        if(control == ControlType.KeyBoard)
        {
        	// Accel = Input.GetAxis("Vertical");
        	// Steer = Input.GetAxis("Horizontal");
        	// Brake = Input.GetAxis("Jump");
        	//UI.SetActive(false);
        }else
        {
        	//UI.SetActive(true);
        }

        //fuelTXT.text = database.fuelAmount.ToString();

        if(currentFuel <= 0 && database.fuelAmount > 0)
        {
            //Refill();
        }

        if(currentFuel >= maxFuel)
        {
            currentFuel = maxFuel;
        }
    }


    void FixedUpdate()
    {
        if(currentFuel > 0)
        {
            car.Move(Steer, Accel, Brake, Brake);
        }

        if(Accel != 0f || Brake != 0f)
        {
            if (currentFuel > 0)
            {
                currentFuel -= fuelUsing;

                SendFuelData();
            }
        }
    }

    void GetFuelData()
    {
        if(C_1940)
        {
            if(PlayerPrefs.GetFloat("C_1940 Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if(PlayerPrefs.GetFloat("C_1940 Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("C_1940 Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("C_1940 Health");
            }
        }

        if (C_BubbleCar)
        {
            if (PlayerPrefs.GetFloat("C_BubbleCar Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("C_BubbleCar Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("C_BubbleCar Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("C_BubbleCar Health");
            }

        }

        if (Hotrod)
        {
            if (PlayerPrefs.GetFloat("Hotrod Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("Hotrod Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("Hotrod Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("Hotrod Health");
            }
        }

        if (IceCreamTruck)
        {
            if (PlayerPrefs.GetFloat("IceCreamTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("IceCreamTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("IceCreamTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("IceCreamTruck Health");
            }
        }

        if (MiniVan)
        {
            if (PlayerPrefs.GetFloat("MiniVan Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("MiniVan Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("MiniVan Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("MiniVan Health");
            }
        }

        if (MonsterTruck)
        {
            if (PlayerPrefs.GetFloat("MonsterTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("MonsterTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("MonsterTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("MonsterTruck Health");
            }
        }


        if (MuscleTruck)
        {
            if (PlayerPrefs.GetFloat("MuscleTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("MuscleTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("MuscleTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("MuscleTruck Health");
            }
        }

        if (PickupTruck)
        {
            if (PlayerPrefs.GetFloat("PickupTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("PickupTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("PickupTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("PickupTruck Health");
            }
        }

        if (PoopTruck)
        {
            if (PlayerPrefs.GetFloat("PoopTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("PoopTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("PoopTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("PoopTruck Health");
            }
        }

        if (PorkTruck)
        {
            if (PlayerPrefs.GetFloat("PorkTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("PorkTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("PorkTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("PorkTruck Health");
            }
        }

        if (PrisonTruck)
        {
            if (PlayerPrefs.GetFloat("PrisonTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("PrisonTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("PrisonTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("PrisonTruck Health");
            }
        }

        if (WaterTruck)
        {
            if (PlayerPrefs.GetFloat("WaterTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("WaterTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("WaterTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("WaterTruck Health");
            }
        }

        if (WienerTruck)
        {
            if (PlayerPrefs.GetFloat("WienerTruck Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("WienerTruck Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("WienerTruck Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("WienerTruck Health");
            }
        }

        if (Semi)
        {
            if (PlayerPrefs.GetFloat("Semi Fuel Set") != 1)
            {
                currentFuel = 100;
                car.currentHealth = car.maxHealth;
            }
            else if (PlayerPrefs.GetFloat("Semi Fuel Set") == 1)
            {
                currentFuel = PlayerPrefs.GetFloat("Semi Fuel");
                car.currentHealth = PlayerPrefs.GetFloat("Semi Fuel Health");
            }
        }
    }

    void SendFuelData()
    {
        if (C_1940)
        {
            PlayerPrefs.SetFloat("C_1940 Fuel", currentFuel);
            PlayerPrefs.SetFloat("C_1940 Fuel Set", 1);

            PlayerPrefs.SetFloat("C_1940 Health", car.currentHealth);
        }

        if (C_BubbleCar)
        {
            PlayerPrefs.SetFloat("C_BubbleCar Fuel", currentFuel);
            PlayerPrefs.SetFloat("C_BubbleCar Fuel Set", 1);

            PlayerPrefs.SetFloat("C_BubbleCar Health", car.currentHealth);
        }

        if (Hotrod)
        {
            PlayerPrefs.SetFloat("Hotrod Fuel", currentFuel);
            PlayerPrefs.SetFloat("Hotrod Fuel Set", 1);

            PlayerPrefs.SetFloat("Hotrod Health", car.currentHealth);
        }

        if (IceCreamTruck)
        {
            PlayerPrefs.SetFloat("IceCreamTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("IceCreamTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("IceCreamTruck Health", car.currentHealth);
        }

        if (MiniVan)
        {
            PlayerPrefs.SetFloat("MiniVan Fuel", currentFuel);
            PlayerPrefs.SetFloat("MiniVan Fuel Set", 1);

            PlayerPrefs.SetFloat("MiniVan Health", car.currentHealth);
        }

        if (MonsterTruck)
        {
            PlayerPrefs.SetFloat("MonsterTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("MonsterTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("MonsterTruck Health", car.currentHealth);
        }

        if (MuscleTruck)
        {
            PlayerPrefs.SetFloat("MuscleTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("MuscleTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("MuscleTruck Health", car.currentHealth);
        }

        if (PickupTruck)
        {
            PlayerPrefs.SetFloat("PickupTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("PickupTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("PickupTruck Health", car.currentHealth);
        }

        if (PoopTruck)
        {
            PlayerPrefs.SetFloat("PoopTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("PoopTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("PoopTruck Health", car.currentHealth);
        }

        if (PorkTruck)
        {
            PlayerPrefs.SetFloat("PorkTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("PorkTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("PorkTruck Health", car.currentHealth);
        }

        if (PrisonTruck)
        {
            PlayerPrefs.SetFloat("PrisonTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("PrisonTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("PrisonTruck Health", car.currentHealth);
        }

        if (WaterTruck)
        {
            PlayerPrefs.SetFloat("WaterTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("WaterTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("WaterTruck Health", car.currentHealth);
        }

        if (WienerTruck)
        {
            PlayerPrefs.SetFloat("WienerTruck Fuel", currentFuel);
            PlayerPrefs.SetFloat("WienerTruck Fuel Set", 1);

            PlayerPrefs.SetFloat("WienerTruck Health", car.currentHealth);
        }

        if (Semi)
        {
            PlayerPrefs.SetFloat("Semi Fuel", currentFuel);
            PlayerPrefs.SetFloat("Semi Fuel Set", 1);

            PlayerPrefs.SetFloat("Semi Fuel Health", car.currentHealth);
        }
    }

    public void Refill()
    {
        currentFuel += 70f;


        database.fuelAmount -= 1;
        player.inventory.RemoveItem(new Item { itemType = Item.ItemType.fuel, amount = database.fuelAmount });
        database.SendData("Item Fuel", database.fuelAmount.ToString());

        SendFuelData();
    }
}
