using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
	Transform player;

	public Vector3 offset;
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game"))
        {
            BGM.Play();
            BGM.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
