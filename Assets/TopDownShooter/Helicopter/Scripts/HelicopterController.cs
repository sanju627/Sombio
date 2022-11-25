using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
public class HelicopterController : MonoBehaviour
{
    public AudioSource HelicopterSound;
    public ControlPanel ControlPanel;
    public Rigidbody HelicopterModel;
    public HeliRotorController MainRotorController;
    public HeliRotorController SubRotorController;
    public Transform seat;
    public Transform outPos;
    public GameObject followCamera;
    public GameObject Canvas;

    [Header("Controls")]
    public float liftSpeed;
    public float downSpeed;
    public float TurnForce = 3f;
    public float ForwardForce = 10f;
    public float ForwardTiltForce = 20f;
    public float TurnTiltForce = 30f;
    public float EffectiveHeight = 100f;

    public float turnTiltForcePercent = 1.5f;
    public float turnForcePercent = 1.3f;

    [Header("UI")]
    public FixedJoystick joyStick;

    private float _engineForce;
    public float EngineForce
    {
        get { return _engineForce; }
        set
        {
            MainRotorController.RotarSpeed = value * 80;
            SubRotorController.RotarSpeed = value * 40;
            HelicopterSound.pitch = Mathf.Clamp(value / 40, 0, 1.2f);
            // if (UIGameController.runtime.EngineForceView != null)
                // UIGameController.runtime.EngineForceView.text = string.Format("Engine value [ {0} ] ", (int)value);

            _engineForce = value;
        }
    }

    public Vector2 hMove = Vector2.zero;
    public Vector2 hTilt = Vector2.zero;
    public float hTurn = 0f;
    public bool IsOnGround = true;
    public float tempY = 0;
    public float tempX = 0;

    [Space]
    public Sprite Icon;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    public Material wreckedMaterial;
    public GameObject[] GFX;
    public AudioClip DestroyedSFX;
    public ParticleSystem fire;
    public bool destroyed;

    [Header("Fuel")]
    public float maxFuel;
    public float currentFuel;
    public float fuelUsing;
    public int carIndex;

    [Header("UI")]
    public TMP_Text fuelTXT;

    AudioSource audio;
    PlayfabManager database;

    // Use this for initialization
    void Start ()
	{
        ControlPanel.KeyPressed += OnKeyPressed;

        Canvas.SetActive(false);

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();

        GetFuelData();

        audio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
	}

	void Update () 
    {
        if(CrossPlatformInputManager.GetButton("Fire1"))
        {
            EngineForce += 0.1f;
        }

        if (CrossPlatformInputManager.GetButton("Fire2"))
        {
            EngineForce -= 0.12f;
            if (EngineForce < 0) EngineForce = 0;
        }

        //fuelTXT.text = database.fuelAmount.ToString();

        

        if (joyStick.Horizontal != 0f || joyStick.Vertical != 0f)
        {
            tempY = 0;
            tempX = 0;

            if (currentFuel > 0)
            {
                currentFuel -= fuelUsing;

                SendFuelData();
            }

            // stable forward
            if (hMove.y > 0)
                tempY = -Time.fixedDeltaTime;
            else
                if (hMove.y < 0)
                tempY = Time.fixedDeltaTime;

            // stable lurn
            if (hMove.x > 0)
                tempX = -Time.fixedDeltaTime;
            else
                if (hMove.x < 0)
                tempX = Time.fixedDeltaTime;

            //Forward
            if(joyStick.Vertical > 0f)
            {
                if (!IsOnGround)
                tempY = Time.fixedDeltaTime;
            }

            //Backward
            if (joyStick.Vertical < 0f)
            {
                if (!IsOnGround)
                    tempY = -Time.fixedDeltaTime;
            }

            //Right
            if (joyStick.Horizontal > 0f)
            {
                if (!IsOnGround)
                    tempX = Time.fixedDeltaTime;
            }

            //Left
            if (joyStick.Horizontal < 0f)
            {
                if (!IsOnGround)
                    tempX = -Time.fixedDeltaTime;
            }

            hMove.x += tempX;
            hMove.x = Mathf.Clamp(hMove.x, -1, 1);

            hMove.y += tempY;
            hMove.y = Mathf.Clamp(hMove.y, -1, 1);
        }

        if (currentFuel <= 0 && database.fuelAmount > 0)
        {
            database.fuelAmount -= 1;
            database.SendData("Item Fuel", database.fuelAmount.ToString());
        }
    }
  
    void FixedUpdate()
    {
        LiftProcess();
        MoveProcess();
        TiltProcess();
    }

    private void MoveProcess()
    {
        var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * (turnTiltForcePercent - Mathf.Abs(hMove.y)), Mathf.Max(0f, hMove.y));
        hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
        HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
        HelicopterModel.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));
    }

    private void LiftProcess()
    {
        var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0f, EngineForce, upForce) * HelicopterModel.mass;
        HelicopterModel.AddRelativeForce(Vector3.up * upForce);
    }

    private void TiltProcess()
    {
        hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
        hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
        HelicopterModel.transform.localRotation = Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);
    }

    public void LiftBTN()
    {
        EngineForce += liftSpeed;
    }

    public void downBTN()
    {
        EngineForce -= downSpeed;
        if (EngineForce < 0) EngineForce = 0;
    }

    void GetFuelData()
    {
        if (PlayerPrefs.GetFloat("Heli Fuel Set") != 1)
        {
            currentFuel = 100;
        }
        else if (PlayerPrefs.GetFloat("Heli Fuel Set") == 1)
        {
            currentFuel = PlayerPrefs.GetFloat("Heli Fuel");
        }
    }

    void SendFuelData()
    {
        PlayerPrefs.SetFloat("Heli Fuel", currentFuel);
        PlayerPrefs.SetFloat("Heli Fuel Set", 1);
    }

    private void OnKeyPressed(PressedKeyCode[] obj)
    {
        


        foreach (var pressedKeyCode in obj)
        {
            switch (pressedKeyCode)
            {
                    case PressedKeyCode.ForwardPressed:

                    
                    break;
                    case PressedKeyCode.BackPressed:

                    if (IsOnGround) break;
                    
                    break;
                    case PressedKeyCode.LeftPressed:

                    if (IsOnGround) break;
                    
                    break;
                    case PressedKeyCode.RightPressed:

                    if (IsOnGround) break;
                    
                    break;
                    case PressedKeyCode.TurnRightPressed:
                    {
                        if (IsOnGround) break;
                        var force = (turnForcePercent - Mathf.Abs(hMove.y))*HelicopterModel.mass;
                        HelicopterModel.AddRelativeTorque(0f, force, 0);
                    }
                    break;
                    case PressedKeyCode.TurnLeftPressed:
                    {
                        if (IsOnGround) break;
                        
                        var force = -(turnForcePercent - Mathf.Abs(hMove.y))*HelicopterModel.mass;
                        HelicopterModel.AddRelativeTorque(0f, force, 0);
                    }
                    break;

            }
        }

        

    }

    private void OnCollisionEnter()
    {
        IsOnGround = true;
    }

    private void OnCollisionExit()
    {
        IsOnGround = false;
    }

    public void TakeDamage(float amount)
        {
            currentHealth -= amount;

            if(currentHealth <= 0)
            {
                for(int i = 0; i < GFX.Length; i++)
                {
                    GFX[i].GetComponent<MeshRenderer>().material = wreckedMaterial;
                    audio.PlayOneShot(DestroyedSFX);
                    fire.Play();
                    gameObject.layer = 0;
                    destroyed = true;

                    foreach(var comp in gameObject.GetComponents<AudioSource>())
                    {
                        Destroy(comp);
                    }
                    //Destroy(this);
                }
            }
        }
}