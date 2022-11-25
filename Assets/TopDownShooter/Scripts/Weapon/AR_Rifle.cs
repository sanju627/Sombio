using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class AR_Rifle : MonoBehaviour
{
    [Header("Player")]
    public Player player;
    public WeaponManger weapon;
    public bool Pistol;
    public bool Rifle;
    public bool Minigun;
    public bool flamethrower;
    public float x;
    public float z;
    [Space]
    public float radius;
    public LayerMask enemyLayer;

    [Header("Stats")]
    public Transform muzzle;
    public Transform gunMuzzle;
    public Transform gunMuzzle2;
    public float fireRate;
    public float Damage;
    public float range;
    public int magazine;
    public int magSize;
    public int totalAmmo;
    public float reloadTime;
    public bool isReloading;
    public float Recoil;

    [Header("VFX")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem FlameVFX;
    public GameObject sparkEffect;
    public GameObject wallEffect;
    public GameObject bloodEffect;
    public GameObject vehicleEffect;
    public GameObject OBJEffect;
    public TrailRenderer BulletTrail;

    [Header("AudioClips")]
    public AudioClip fireSFX;
    public AudioClip reloadSFX;
    public AudioClip[] bloodHitSFX;
    public AudioClip[] metalHitSFX;
    public AudioClip[] wallHitSFX;

    [Header("UI")]
    public TextMeshProUGUI ammoTXT;
    public Image weaponSlot;
    public Sprite weaponImage;

    float nextTimeToFire = 0f;
    float lastAmount;
    float minusAmmoCount;
    DataImporter dataImporter;

    int magazineTamp;
    bool isAudioPlaying;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        weapon = player.GetComponent<WeaponManger>();
        dataImporter = player.GetComponent<DataImporter>();
        audio = GetComponent<AudioSource>();

        stopAutoFire();

        CrossPlatformInputManager.GetButtonUp("Fire1");

        //ammo = magazine * mags;
        magazineTamp = magazine;
    }

    public IEnumerator stopAutoFire()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.05f);
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        Recoil = player.currentAccuracy;

        totalAmmo = player.GetComponent<ItemAssets>().ammoAmount;

        if (Pistol)
        {
            player.SetLayer("Rifle", 0);
            player.SetLayer("Item", 0);
            player.SetLayer("Melee", 0);
            player.SetLayer("Pistol", 1);
        }

        if (Rifle || flamethrower)
        {
            player.SetLayer("Rifle", 1);
            player.SetLayer("Item", 0);
            player.SetLayer("Melee", 0);
            player.SetLayer("Pistol", 0);
        }

        if (Minigun)
        {
            player.SetLayer("Rifle", 0);
            player.SetLayer("Item", 0);
            player.SetLayer("Melee", 0);
            player.SetLayer("Pistol", 0);
            player.SetLayer("Minigun", 1);
        }

        //Reloading
        if (magazine <= 0 && !isReloading && !weapon.isSwitching && totalAmmo > 0)
        {
            StartCoroutine(reload());
        }

        if (CrossPlatformInputManager.GetButton("Jump") && !isReloading && !weapon.isSwitching && magazine < magSize && totalAmmo > 0)
        {
            StartCoroutine(reload());
        }

        x = player.rotJoystick.Horizontal;
        z = player.rotJoystick.Vertical;

        Vector3 fireBTN = new Vector3(x, 0, z);

        if (fireBTN.sqrMagnitude >= 0.5f && Time.time >= nextTimeToFire && !isReloading && !weapon.isSwitching && magazine > 0)
        {
            if (!flamethrower)
            {
                Fire();
            }
            else if (flamethrower)
            {
                Flame();
                FlameVFX.Play();
            }

            flameSFX();

            nextTimeToFire = Time.time + 1f / fireRate;

            

            //audio.Play();
        }
        else if (flamethrower)
        {
            FlameVFX.Stop();
           // audio.Stop();
            
            //StartCoroutine(stopNoise());
        }

        if(fireBTN.sqrMagnitude == 0f && !isReloading && !weapon.isSwitching && magazine > 0 && !player.isRunning)
        {
            audio.Stop();
            isAudioPlaying = false;
        }

        //UI
        ammoTXT.text = magazine.ToString("0") + " / " + totalAmmo.ToString("0");
        weaponSlot.sprite = weaponImage;
    }

    void flameSFX()
    {
        if (isAudioPlaying) return;
        audio.Play();

        isAudioPlaying = true;
    }

    void Fire()
    {
        Vector3 shootDirection = muzzle.forward;
        shootDirection.x += Random.Range(Recoil, -Recoil);
        shootDirection.y += Random.Range(Recoil, -Recoil);

        magazine--;
        muzzleFlash.Play();
        audio.PlayOneShot(fireSFX);
        //anim.SetTrigger("Fire");

        player.SetTrigger("Fire");

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, shootDirection, out hit, range))
        {
            //Trail
            if (hit.transform.tag == "Metal")
            {
                GameObject hitEffect = Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            }

            if (hit.transform.tag == "Shop")
            {
                GameObject hitEffect = Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            }

            if (hit.transform.tag == "Wall")
            {
                GameObject hitEffect = Instantiate(wallEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(wallHitSFX[Random.Range(0, wallHitSFX.Length)]);
            }

            if (hit.transform.tag == "Vehicle")
            {
                GameObject hitEffect = Instantiate(vehicleEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            }

            Zombie_BP zom = hit.transform.GetComponentInChildren<Zombie_BP>();
            if (zom != null)
            {
                zom.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            CarGFX veh = hit.transform.GetComponent<CarGFX>();
            if (veh != null)
            {
                veh.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(vehicleEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            }

            HelicopterController hel = hit.transform.GetComponent<HelicopterController>();
            if (hel != null)
            {
                hel.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(vehicleEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            }

            BodyParts targetBoss = hit.transform.GetComponentInChildren<BodyParts>();
            if (targetBoss != null)
            {
                targetBoss.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            Bandit_BP bp = hit.transform.GetComponentInChildren<Bandit_BP>();
            if (bp != null)
            {
                bp.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            Cannibal cb = hit.transform.GetComponent<Cannibal>();
            if (cb != null)
            {
                cb.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            Animal animal = hit.transform.GetComponentInChildren<Animal>();
            if (animal != null)
            {
                animal.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            TrailRenderer trail = Instantiate(BulletTrail, gunMuzzle.position, gunMuzzle.rotation);

            StartCoroutine(SpawnTrail(trail, hit));
        }
    }

    void Flame()
    {
        magazine--;
        //muzzleFlash.Play();
        
        //anim.SetTrigger("Fire");

        player.SetTrigger("Fire");

        Collider[] colliders = Physics.OverlapSphere(gunMuzzle.position, radius);
        Collider[] colliders2 = Physics.OverlapSphere(gunMuzzle2.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            CarGFX c = nearbyObject.GetComponent<CarGFX>();
            if (c != null)
            {
                c.TakeDamage(Damage);
            }

            Zombie_BP z = nearbyObject.GetComponent<Zombie_BP>();
            if (z != null)
            {
                z.Burn(Damage);
            }

            Bandit_BP b = nearbyObject.GetComponent<Bandit_BP>();
            if (b != null)
            {
                b.Burn(Damage);
            }

            Cannibal cb = nearbyObject.GetComponent<Cannibal>();
            if (cb != null)
            {
                cb.Burn(Damage);
            }

            BodyParts bossBody = nearbyObject.GetComponent<BodyParts>();
            if (bossBody != null && bossBody.brute.miniBoss)
            {
                bossBody.Burn(Damage);
            }

            Animal a = nearbyObject.GetComponent<Animal>();
            if (a != null)
            {
                a.TakeDamage(Damage + 50f);
            }

            HelicopterController h = nearbyObject.GetComponent<HelicopterController>();
            if (h != null)
            {
                h.TakeDamage(Damage);
            }

            BodyParts targetBoss = nearbyObject.GetComponent<BodyParts>();
            if (targetBoss != null)
            {
                targetBoss.TakeDamage(Damage);
            }

            Animal animal = nearbyObject.GetComponent<Animal>();
            if (animal != null)
            {
                animal.TakeDamage(Damage);
            }
        }

        foreach (Collider nearbyObject in colliders2)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            CarGFX c = nearbyObject.GetComponent<CarGFX>();
            if (c != null)
            {
                c.TakeDamage(Damage);
            }

            Zombie_BP z = nearbyObject.GetComponent<Zombie_BP>();
            if (z != null)
            {
                z.Burn(Damage);
            }

            Bandit_BP b = nearbyObject.GetComponent<Bandit_BP>();
            if (b != null)
            {
                b.Burn(Damage);
            }

            Animal a = nearbyObject.GetComponent<Animal>();
            if (a != null)
            {
                a.TakeDamage(Damage + 50f);
            }

            HelicopterController h = nearbyObject.GetComponent<HelicopterController>();
            if (h != null)
            {
                h.TakeDamage(Damage);
            }

            BodyParts targetBoss = nearbyObject.GetComponent<BodyParts>();
            if (targetBoss != null)
            {
                targetBoss.TakeDamage(Damage);
            }

            Animal animal = nearbyObject.GetComponent<Animal>();
            if (animal != null)
            {
                animal.TakeDamage(Damage);
            }
        }

    }

    IEnumerator reload()
    {
        isReloading = true;

        if(flamethrower)
        {
            audio.Stop();
            isAudioPlaying = false;
        }
        

        player.SetBool("Reload", true);

        audio.PlayOneShot(reloadSFX);




        yield return new WaitForSeconds(reloadTime);


        player.SetBool("Reload", false);

        totalAmmo = totalAmmo - magSize + magazine;
        magazine = magazineTamp;

        player.inventory.RemoveItem(new Item {itemType = Item.ItemType.ammo, amount = magazine});

        dataImporter.SendInventoryData();

        if (totalAmmo < 0)
        {
            magazine += totalAmmo;
            totalAmmo = 0;
        }

        isReloading = false;
    }

    IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 StartPosition = Trail.transform.position;

        while (time < 0.1f)
        {
            Trail.transform.position = Vector3.Lerp(StartPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = hit.point;

        Destroy(Trail.gameObject, Trail.time);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gunMuzzle.position, radius);

        if(flamethrower)
        Gizmos.DrawWireSphere(gunMuzzle2.position, radius);
    }
}
