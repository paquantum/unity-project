using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter02 : MonoBehaviour
{
    //������
    public Material _skybox;
    void Start()
    {
        Initialized();
    }
    /// <summary>
    /// �� �ʱ�ȭ
    /// </summary>
    public void Initialized()
    {
        RenderSettings.skybox = _skybox; //��ī�̹ڽ� ����
        GameManager.Instance.InitXrRigPosition();//ī�޶� ������ �ʱ�ȭ
        GameManager.Instance.ActivateMovememt(true);//�����Ʈ ����
    }
}
