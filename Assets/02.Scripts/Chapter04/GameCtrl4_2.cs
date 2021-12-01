using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameCtrl4_2 : MonoBehaviour
{
    // 0성공 1실패 상태
    private int gameState;
    public GameObject[] noticeMsg;
    //private int msgPos = 0;
    public GameObject[] grounds;
    public GameObject windowBar;

    // 정화차량 탑승한 주인공의 위치 담을 변수
    //public Transform playerTr;
    // 도착지의 위치 담을 변수
    public Transform windowTr;
    private bool _distance;

    // 지구화실패 자연소리, 지구화실패 숲웅성, 지구화성공 쓸쓸한바람, 쌍따옴표메시지
    public AudioSource[] audioSources;

    void Start()
    {
        GameManager.Instance._XRrig.transform.localEulerAngles = new Vector3(0, 180, 0);
        windowBar.GetComponent<Animator>().SetBool("onLight", true);
        //playerTr = GameManager.Instance._XRrig.GetComponent<Transform>();
        if (GameObj.checkGameSuccess == 3)
        {
            gameState = 0;
            audioSources[0].Play();
            audioSources[1].Play();
        }
        else if (GameObj.checkGameSuccess == 4)
        {
            gameState = 1;
            audioSources[2].Play();
        }
        grounds[gameState].SetActive(true);
        //StartCoroutine(PlayerBehaviour());
    }

    // IEnumerator PlayerBehaviour()
    // {
    //     CheckPlayerState();
    //     yield return new WaitForSeconds(0.5f);
    //     StartCoroutine(PlayerBehaviour());
    // }

    // 매립지와 주인공 거리를 가져와서 상태를 변환시키는 함수
    // void CheckPlayerState()
    // {
    //     float distance = Vector3.Distance(windowTr.position, playerTr.position);
    //     Debug.Log("주인공과 창문 거리 : " + distance);
    //     if (distance < 3.0f)
    //     {
    //         StopAllCoroutines();
    //         GameObj04.instance._distance = true;
    //         GameObj04.instance.state = gameState;
    //         windowBar.GetComponent<Animator>().SetBool("onLight", false);
    //     }
    // }
}
