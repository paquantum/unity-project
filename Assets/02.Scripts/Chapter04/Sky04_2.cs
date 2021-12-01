using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky04_2 : MonoBehaviour
{
    public Material _skybox;
    
    public void Start()
    {
        Initialized();
    }

    // 3씬 스카이박스 초기화
    private void Initialized()
    {
        //skybox setting
        RenderSettings.skybox = _skybox;
    }
}
