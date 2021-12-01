using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter01 : MonoBehaviour
{
    public Material _skybox;
    public void Start()
    {
        //skybox setting
        RenderSettings.skybox = _skybox;
        GameManager.Instance.InitXrRigPosition();
        GameManager.Instance.ActivateMovememt(false);
    }


}
