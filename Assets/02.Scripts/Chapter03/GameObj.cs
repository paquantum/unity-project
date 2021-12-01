using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObj : MonoBehaviour
{
    // 씬 SettingXR 게임매니저 인스턴스화를 위한 변수
    public static GameObj instance;

    // 게임 상태여부 체크로 1은 실패 2는 성공상태 // 3_2_landFill
    // 3은 실패 4는 성공상태 // 4_0_fail, 4_1_blackUniverse 에서 사용
    public static int checkGameSuccess = 4;
    public static int objManage;
    public GameObject collManage;
    // public static int ch4FailorSucces = 1;

    public GameObject leftCtrlSaber;
    public GameObject rightCtrlSaber;
    public GameObject[] leftShape;
    public GameObject[] rightShape;

    // Update에서 호출 제한을 두기 위한 변수
    public bool autoMove = true;

    // 슬롯 객체 담는 변수
    public GameObject[] slot;

    // 메시지 객체 담는 변수
    public GameObject[] uiMsg;

    // 정화트럭 객체 담는 변수
    public GameObject truck;

    // 매립지 객체 담는 변수
    public GameObject landfill;

    // 자동 이동할 때 사용할 지형 터레인 담는 변수
    public GameObject terrain1;

    public Button[] checkBtn;

    // 정보설명, 버튼클릭, 쌍따옴표메시지
    public AudioSource[] audioSources;

    // 싱글톤을 위해 선언
    void Awake()
    {
        instance = this;
    }

    // 게임이 1(실패), 2(성공) 둘 다 차량 자동이동이 있기 때문에 코루틴으로 이동하는 함수 호출
    void Update()
    {
        checkBtn[0].onClick.AddListener(ChangeScene);
        checkBtn[1].onClick.AddListener(ChangeScene);
        if (checkGameSuccess == 0)
        {
            leftCtrlSaber.GetComponent<Saber>().enabled = true;
            rightCtrlSaber.GetComponent<Saber>().enabled = true;
        }
        if ((checkGameSuccess == 1 || checkGameSuccess == 2) && autoMove)
        {
            autoMove = false;
            StartCoroutine(AutoMove());
        }
        if (checkGameSuccess == 3 || checkGameSuccess == 4)
        {
            truck.SetActive(false);
            GameManager.Instance._XRrig.GetComponent<NavMeshAgent>().enabled = false;
            GameManager.Instance._XRrig.GetComponent<MoveTruck>().enabled = false;
        }
        if (objManage == 1)
        {
            collManage.SetActive(true);
        }
        else
        {
            collManage.SetActive(false);
        }
    }

    // 트럭이 움직이기 전에 세팅을 위한 함수
    IEnumerator AutoMove()
    {
        // MoveTruck 스크립트 활성화 시킴
        yield return new WaitForSeconds(3.0f); // 4.0f를 3.0f로 수정 ######
        GameManager.Instance._XRrig.GetComponent<MoveTruck>().enabled = true;
    }

    public void ChangeScene()
    {
        audioSources[0].Play();
        if (checkGameSuccess == 3)
            SceneLoader.Instance.LoadNewScene("Chapter04_0_fail");
        else if (checkGameSuccess == 4)
            SceneLoader.Instance.LoadNewScene("Chapter04_1_blackUniverse");
    }
}
