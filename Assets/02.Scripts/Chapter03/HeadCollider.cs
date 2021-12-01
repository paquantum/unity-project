using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadCollider : MonoBehaviour
{
    // 하트 UI 객체 담을 변수
    public GameObject[] hearts;
    // 목숨을 잃을 때 교체할 텍스처를 담을 변수
    public Texture empty_heart;
    // 하트 갯수 담을 변수
    private int hp_count;

    public AudioSource audioSource;

    // 게임 매니저에서 하트 객체를 가져옴
    void Update()
    {
        hearts = GameCtrl.instance.hpImages;
    }
    
    // 카메라에 캡슐을 하나 붙여서 돌에 맞는지 확인하는 함수
    void OnCollisionEnter(Collision coll) {
        if (coll.collider.CompareTag("ROCKS")) {
            audioSource.Play();
            Destroy(coll.gameObject);
            hp_count = GameCtrl.instance.heartCount;
            if (hp_count > 0) {
                hearts[hp_count].SetActive(false);
                GameCtrl.instance.heartCount -= 1;
            }
            hearts[hp_count - 1].SetActive(true);
        }
    }
}
