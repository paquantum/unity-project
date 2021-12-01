using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 장애물 받아올 변수
    public GameObject[] cubes;
    // 스폰 위치 받을 변수
    public Transform[] points;
    public float beat = (60 / 105) * 2;
    private float timer;

    
    void Update()
    {
        if (GameObj.checkGameSuccess == 0)
        {
            // 목숨을 다 잃으면 스폰 스크립트를 비활성화 함
            if (GameCtrl.instance.heartCount == 0) {
                this.gameObject.SetActive(false);
            }
            // 3개의 스폰위치에서 3개의 장애물이 랜덤으로 생성
            if (timer > beat && GameCtrl.instance.heartCount > 0) {
                GameObject cube = Instantiate(cubes[Random.Range(0, 3)], points[Random.Range(0, 3)]);
                cube.transform.localPosition = Vector3.zero;
                timer -= beat;
            }
            timer += Time.deltaTime;
        }
    }
}
