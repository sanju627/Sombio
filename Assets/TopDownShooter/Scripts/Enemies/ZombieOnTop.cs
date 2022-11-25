using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOnTop : MonoBehaviour
{
    public Player player;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        player.TakeDamage(damage);
    }
}
