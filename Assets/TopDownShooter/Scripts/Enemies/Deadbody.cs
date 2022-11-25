using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadbody : MonoBehaviour
{
    Zombie zombie;
    bool applied;

    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponent<Zombie>();
    }

    // Update is called once per frame
    void Update()
    {
        if (applied) return;

        if(zombie.dead)
        {
            Spawn();
            Destroy(gameObject, 5f);
            applied = true;
        }
    }

    void Spawn()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("CityBase"))
        Instantiate(items[Random.Range(0, items.Length)], transform.position, transform.rotation);
    }
}
