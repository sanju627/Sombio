using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
	[Header("Stats")]
	public float radius;
	public LayerMask playerLayer;
	public bool withinPlayer;
	public NavMeshAgent agent;
	public Animator anim;
	[Space]
	public float barkTime;
	public bool barked;
	public float minBarkTime;
	public float maxBarkTime;

	[Header("AudioClips")]
	public AudioClip[] barkSFX;
	public AudioClip[] footsSFX;
	public AudioClip defeatBark;
	public AudioSource audio;

	Player player;
	float dist;
	bool dead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(dead)return;

    	withinPlayer = Physics.CheckSphere(transform.position, radius, playerLayer);
    	dist = Vector3.Distance(transform.position, player.transform.position);

        if(player.inCar)
        {
            transform.position = player.carController.seat.position;
            agent.enabled = false;

            DisableSkin();
        }
        else
        {
            if (dist > agent.stoppingDistance && !withinPlayer)
            {
                agent.SetDestination(player.transform.position);
                anim.SetBool("walk", true);

                if (!barked)
                {
                    float barkTime = Random.Range(minBarkTime, maxBarkTime);
                    StartCoroutine(Bark(barkTime));
                }

            }
            else
            {
                agent.SetDestination(transform.position);
                anim.SetBool("walk", false);
            }

            StartCoroutine(EnableSkin(0.1f));
        }


        if (player.Dead)
        {
        	audio.PlayOneShot(defeatBark);
        	anim.SetTrigger("defeat");

        	dead = true;
        }
    }

    public void DisableSkin()
    {
        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();


        foreach (SkinnedMeshRenderer c in skin)
        {
            c.enabled = false;
        }

        foreach (MeshRenderer m in mesh)
        {
            m.enabled = false;
        }
    }

    public IEnumerator EnableSkin(float delay)
    {
        yield return new WaitForSeconds(delay);

        SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        Collider[] col = GetComponentsInChildren<Collider>();

        agent.enabled = true;

        foreach (SkinnedMeshRenderer c in skin)
        {
            c.enabled = true;
        }

        foreach (MeshRenderer m in mesh)
        {
            m.enabled = true;
        }
    }

    public void Foots()
    {
    	audio.PlayOneShot(footsSFX[Random.Range(0, footsSFX.Length)]);
    }

    IEnumerator Bark(float barkTiming)
    {
    	barked = true;

    	yield return new WaitForSeconds(barkTiming);

    	audio.PlayOneShot(barkSFX[Random.Range(0, barkSFX.Length)]);

    	barked = false;
    }

    void OnDrawGizmosSelected()
    {
    	Gizmos.DrawWireSphere(transform.position, radius);
    }
}
