using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject _XRrig;
    public GameObject _Left;
    public ActionBasedController _LeftCtrl;
    public ActionBasedContinuousMoveProvider _movement;
    /*public Material[] _skybox;
    public void SettingSkyBOX(int idx)
    {
        //camera rig potion 
        //skybox setting
        RenderSettings.skybox = _skybox[idx];
    }*/

    private void Awake()
    {
        //_camera.clearFlags = CameraClearFlags.SolidColor;
        //SceneManager.LoadScene("Chapter03_0", LoadSceneMode.Additive);
    }

    public void InitXrRigPosition()
    {
        //camera rig potion 
        Transform tr = _XRrig.GetComponent<Transform>();
        tr.localPosition = Vector2.zero;
    }
    public void ActivateMovememt(bool _enable)
    {
        //ContinuousMoveProvider
        _movement.enabled = _enable;
    }
    
}
