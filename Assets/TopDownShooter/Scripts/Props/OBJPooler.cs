using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJPooler : MonoBehaviour
{

    #region singelton
    public static OBJPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private List<GameObject> poolObjects = new List<GameObject>();
    public int totalSpawn = 30;
    [Space]
    public GameObject Prefab;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalSpawn; i++)
        {
            GameObject obj = Instantiate(Prefab);
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledOBJ()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if(!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }

        return null;
    }
}
