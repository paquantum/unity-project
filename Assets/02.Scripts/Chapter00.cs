using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter00 : MonoBehaviour
{
    public Material _skybox;
    public void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        //skybox setting
        RenderSettings.skybox = _skybox;
        GameManager.Instance.InitXrRigPosition();
        GameManager.Instance.ActivateMovememt(false);
    }


}
