using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTester : MonoBehaviour
{
    private void OnValidate()
    {
        //�̽�ũ��Ʈ�� ������Ʈ�� ���� ���ӿ�����Ʈ �̸��� �ٲ�
        gameObject.name = "DebugTester";

    }
    public void TestDebug(string arg)
    {
        Debug.Log(arg);
    }
}
