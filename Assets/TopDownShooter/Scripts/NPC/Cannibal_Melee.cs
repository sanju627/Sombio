using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannibal_Melee : MonoBehaviour
{
    public Cannibal npc;
    public Animator anim;
    public bool Pistol;
    public bool Rifle;

    float nextTimeToFire = 0f;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (npc.dead) return;

        anim.SetLayerWeight(anim.GetLayerIndex("Rifle"), 0);
        anim.SetLayerWeight(anim.GetLayerIndex("Melee"), 1);
        anim.SetLayerWeight(anim.GetLayerIndex("Pistol"), 0);


        if (npc.canFire && Time.time >= nextTimeToFire)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                anim.SetTrigger("attack1");
            }
            else
            {
                anim.SetTrigger("attack2");
            }
        }
    }
}
