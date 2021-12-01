using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCtrl : MonoBehaviour
{
    // 하트 이미지 객체 받아오는 변수
    public GameObject[] hearts;
    // 바꿀 이미지 받아오는 변수
    public Texture empty_heart;
    // 실수 가능한 횟수 받아오는 변수
    private int hp_count;

    void Start()
    {
        // 게임매니저에서 하트 객체 가져오기
        hearts = GameCtrl.instance.hpImages;
    }

    // CUBE나 ITEMBOX를 베지 못한 경우 하트 이미지를 빈 이미지로 교체하는 함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("CUBE") || coll.collider.CompareTag("ITEMBOX"))
        {
            hp_count = GameCtrl.instance.heartCount;
            Destroy(coll.gameObject);
            if (hp_count > 0)
            {
                hearts[hp_count].SetActive(false);
                GameCtrl.instance.heartCount -= 1;
            }
            hearts[hp_count - 1].SetActive(true);
        }
        
    }
}
