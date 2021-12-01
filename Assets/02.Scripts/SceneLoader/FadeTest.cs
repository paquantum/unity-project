using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    public Renderer cubeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer.material.SetColor("_BaseMap", Color.red);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
