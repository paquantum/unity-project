using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // 생성된 큐브 및 땅 움직임
    void Update()
    {
        // GameSuccess가 0은 게임이 진행중으로 객체가 계속해서 이동함
        if (GameCtrl.instance.heartCount > 0) {
            this.gameObject.transform.position += Time.deltaTime * transform.forward * 3;
        }
        else { // 게임이 진행중이 아니라면 멈춤
            this.gameObject.transform.position += Time.deltaTime * transform.forward * 0;
        }
    }
}
