using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Jump : MonoBehaviour
{
    public ZombiesTarget target;
    public float distance;
    public float speed;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        distance = Vector3.Distance(transform.position, player.transform.position);

        target = player.GetComponent<ZombiesTarget>();

        var targetT = target.transform.position;
        targetT.y = transform.position.y;
        transform.LookAt(targetT);


    }

    void FindClosesteEnemy()
    {
        float distanceToClosesteTarget = Mathf.Infinity;
        target = null;

        ZombiesTarget[] allTarget = GameObject.FindObjectsOfType<ZombiesTarget>();

        foreach (ZombiesTarget currentTarget in allTarget)
        {
            float distToTarget = (currentTarget.transform.position - this.transform.position).sqrMagnitude;
            if (distToTarget < distanceToClosesteTarget)
            {
                distanceToClosesteTarget = distToTarget;
                target = currentTarget;

                if (distance >= 2f && !target.isAlive)
                {
                    target = player.GetComponent<ZombiesTarget>();
                }
                else
                {
                    target = currentTarget;
                }

            }
        }

        var targetT = target.transform.position;
        targetT.y = transform.position.y;
        transform.LookAt(targetT);
    }
}
