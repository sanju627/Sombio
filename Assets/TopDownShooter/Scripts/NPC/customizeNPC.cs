using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customizeNPC : MonoBehaviour
{
    public SkinnedMeshRenderer skinMesh;

    public Mesh[] meshes;

    // Start is called before the first frame update
    void Start()
    {
        skinMesh = GetComponent<SkinnedMeshRenderer>();

        skinMesh.sharedMesh = meshes[Random.Range(0, meshes.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
