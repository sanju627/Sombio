using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float radius;
    public float damage;
    public LayerMask targetLayer;

    [Header("GFX")]
    public GameObject trail;
    public GameObject acidImpact;

    [Header("SFX")]
    public AudioClip[] impactSFX;
    AudioSource audio;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Attack();

        if(collision.transform.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }

    public void Attack()
    {
        Collider[] targetCol = Physics.OverlapSphere(transform.position, radius, targetLayer);

        audio.PlayOneShot(impactSFX[Random.Range(0, impactSFX.Length)]);

        foreach (Collider c in targetCol)
        {

            Player player = c.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            NPC npc = c.GetComponent<NPC>();
            if (npc != null)
            {
                npc.TakeDamage(damage);
            }

            CarGFX car = c.GetComponent<CarGFX>();
            if (car != null)
            {
                car.TakeDamage(damage);
            }

            Bandit_BP b = c.GetComponent<Bandit_BP>();
            if (b != null)
            {
                b.TakeDamage(damage);
            }

            OBJ o = c.GetComponent<OBJ>();
            if (o != null)
            {
                o.TakeDamage(damage);
            }

            Animal a = c.GetComponent<Animal>();
            if (a != null)
            {
                a.TakeDamage(damage);
            }
        }

        rb.isKinematic = true;
        trail.SetActive(false);

        Instantiate(acidImpact, transform.position, transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
