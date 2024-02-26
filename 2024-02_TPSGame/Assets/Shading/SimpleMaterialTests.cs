using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMaterialTests : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            meshRenderer.material.SetFloat("_Glossiness", 1.0f);
            meshRenderer.material.SetColor("_Color", Color.blue);
        }    
    }
}
