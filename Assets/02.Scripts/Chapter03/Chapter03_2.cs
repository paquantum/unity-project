using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter03_2 : MonoBehaviour
{
    // 배경음, 자동차운전배경음, 
    public AudioSource[] audioSources;
    
    void Start()
    {
        audioSources[0].Play();
        audioSources[1].Play();
    }
}
