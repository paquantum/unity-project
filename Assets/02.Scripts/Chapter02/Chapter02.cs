using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter02 : MonoBehaviour
{
    //씬설정
    public Material _skybox;
    void Start()
    {
        Initialized();
    }
    /// <summary>
    /// 씬 초기화
    /// </summary>
    public void Initialized()
    {
        RenderSettings.skybox = _skybox; //스카이박스 셋팅
        GameManager.Instance.InitXrRigPosition();//카메라 포지션 초기화
        GameManager.Instance.ActivateMovememt(true);//무브먼트 해제
    }
}
