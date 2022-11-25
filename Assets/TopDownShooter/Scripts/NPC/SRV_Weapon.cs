using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRV_Weapon : MonoBehaviour
{
    public Survivor srv;
    public bool Pistol;
    public bool Rifle;
    public bool Flamethrower;
    public float weaponCheckRadius;

    [Header("Stats")]
    public Transform muzzle;
    public Transform gunMuzzle;
    public float fireRate;
    public float Damage;
    public float range;
    public int currentAmmo;
    public int maxAmmo;
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


    float nextTimeToFire = 0f;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();

        currentAmmo = maxAmmo;

        if (!Flamethrower)
        {
            weaponCheckRadius = 5f;
        }
        else
        {
            weaponCheckRadius = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (srv.dead) return;

        srv.weaponCheckRadius = weaponCheckRadius;


        if (Pistol)
        {
            srv.SetLayer("Rifle", 0);
            srv.SetLayer("Melee", 0);
            srv.SetLayer("Pistol", 1);
        }

        if (Rifle)
        {
            srv.SetLayer("Rifle", 1);
            srv.SetLayer("Melee", 0);
            srv.SetLayer("Pistol", 0);
        }

        //Reloading
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(reload());
        }

        if (srv.canFire && Time.time >= nextTimeToFire && !isReloading && currentAmmo > 0)
        {
            if (!Flamethrower)
                Fire();
            else
                Flame();

            nextTimeToFire = Time.time + 1f / fireRate;
        }
        else if (Flamethrower)
        {
            FlameVFX.Stop();
        }
    }

    void Fire()
    {
        Vector3 shootDirection = muzzle.forward;
        shootDirection.x += Random.Range(Recoil, -Recoil);
        shootDirection.y += Random.Range(Recoil, -Recoil);

        currentAmmo--;
        muzzleFlash.Play();
        audio.PlayOneShot(fireSFX);
        srv.SetTrigger("Fire");

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

            OBJ obj = hit.transform.GetComponent<OBJ>();
            if (obj != null)
            {
                obj.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(OBJEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(wallHitSFX[Random.Range(0, wallHitSFX.Length)]);
            }

            Bandit_BP bp = hit.transform.GetComponentInChildren<Bandit_BP>();
            if (bp != null)
            {
                bp.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }

            Cannibal can = hit.transform.GetComponent<Cannibal>();
            if (can != null)
            {
                can.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(OBJEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(wallHitSFX[Random.Range(0, wallHitSFX.Length)]);
            }

            TrailRenderer trail = Instantiate(BulletTrail, gunMuzzle.position, gunMuzzle.rotation);

            StartCoroutine(SpawnTrail(trail, hit));
        }
    }

    void Flame()
    {
        currentAmmo--;
        FlameVFX.Play();
        audio.PlayOneShot(fireSFX);
        //anim.SetTrigger("Fire");

        srv.SetTrigger("Fire");

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range))
        {
            //Trail
            // if(hit.transform.tag == "Metal")
            //   {
            //     GameObject hitEffect = Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //     Destroy(hitEffect, 2f);
            //     audio.PlayOneShot(metalHitSFX[Random.Range(0, metalHitSFX.Length)]);
            //   }

            //   if(hit.transform.tag == "Wall")
            //   {
            //     GameObject hitEffect = Instantiate(wallEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //     Destroy(hitEffect, 2f);
            //     audio.PlayOneShot(wallHitSFX[Random.Range(0, wallHitSFX.Length)]);
            //   }

            Zombie_BP target = hit.transform.GetComponentInChildren<Zombie_BP>();
            if (target != null)
            {
                target.Burn(Damage);
            }

            Bandit_BP bandit = hit.transform.GetComponentInChildren<Bandit_BP>();
            if (bandit != null)
            {
                bandit.Burn(Damage);
            }

            Cannibal can = hit.transform.GetComponent<Cannibal>();
            if (can != null)
            {
                can.Burn(Damage);
            }

            CarGFX veh = hit.transform.GetComponent<CarGFX>();
            if (veh != null)
            {
                veh.TakeDamage(Damage);
            }

            BodyParts bossBody = hit.transform.GetComponent<BodyParts>();
            if (bossBody != null && bossBody.brute.miniBoss)
            {
                bossBody.Burn(Damage);
            }

            BodyParts targetBoss = hit.transform.GetComponentInChildren<BodyParts>();
            if (targetBoss != null)
            {
                targetBoss.TakeDamage(Damage);

                GameObject hitEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 2f);
                audio.PlayOneShot(bloodHitSFX[Random.Range(0, bloodHitSFX.Length)]);
            }
        }
    }

    IEnumerator reload()
    {
        isReloading = true;
        srv.SetBool("Reload", true);

        audio.PlayOneShot(reloadSFX);

        yield return new WaitForSeconds(reloadTime);

        srv.SetBool("Reload", false);

        currentAmmo = maxAmmo;
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
}
