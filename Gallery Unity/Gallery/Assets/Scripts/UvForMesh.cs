using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvForMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2[] uvs = GetComponent<MeshFilter> ().sharedMesh.uv;
 
         uvs [6] = new Vector2 (0, 0);
         uvs [7] = new Vector2 (1, 0);
         uvs [10] = new Vector2 (0, 1);
         uvs [11] = new Vector2 (1, 1);
 
         GetComponent<MeshFilter> ().sharedMesh.uv = uvs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
