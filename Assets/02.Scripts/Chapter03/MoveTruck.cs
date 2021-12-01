using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveTruck : MonoBehaviour
{
    // 3_1 정화세이버 게임 이후 성공과 실패시에 따라 차량 자동 이동과 성공, 실패 씬 전환하는 스크립트

    // 차량이동 정지 상태와 이동 상태를 위해 선언
    public enum State
    {
        IDLE,
        TRACE
    }
    // 차량 상태 초기값을 정지로 선언
    public State state = State.IDLE;

    // 정화차량 탑승한 주인공의 위치 담을 변수
    public Transform playerTr;
    // 도착지의 위치 담을 변수
    public Transform landTr;
    // 네비게이션 사용을 위한 변수
    public NavMeshAgent agent;

    // 게임 상태에 따라 메시지 담을 변수
    public GameObject noticeMsg1;
    int pos = 0;

    public GameObject truck;
    // 지형 터레인을 담을 변수
    public GameObject terrain;
    // 도착할 위치를 위해 매립지 담을 변수
    public GameObject landfill;
    // 죽은 생물 슬롯을 담을 변수
    public GameObject[] slot;

    // 성공 상태에서 작업을 계속할지 수락 거절을 위한 버튼
    public Button[] successBtn;

    // 슬롯확대효과음, 정보설명, 클릭, 쌍따옴표메시지
    public AudioSource[] audioSources;

    // 스크립트 활성화 될 때마다 실행
    void OnEnable()
    {
        landfill.SetActive(true);
        // 주인공, 매립지 위치 가져옴, agent 설정하고 NavMeshAgent 스크립트 활성화
        playerTr = GetComponent<Transform>();
        landTr = GameObject.FindWithTag("LANDFILL").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        // 주인공 정화세이버에 사용한 컨트롤러 꺼줌
        GameObj.instance.leftShape[1].SetActive(false);
        GameObj.instance.rightShape[1].SetActive(false);
        GameObj.instance.leftShape[0].SetActive(true);
        GameObj.instance.rightShape[0].SetActive(true);

        // 1실패 2성공의 값을 pos에 담고 그에 맞는 메시지창을 닫음
        pos = GameObj.checkGameSuccess;
        noticeMsg1 = GameObj.instance.uiMsg[pos - 1];
        if (pos == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                slot[i].SetActive(true);
            }
        }
        truck.SetActive(true);
        terrain.SetActive(true);
        // 자동 이동 시작하는 코루틴
        if (GameObj.checkGameSuccess == 1 || GameObj.checkGameSuccess == 2)
            StartCoroutine(NavMove());
    }

    // 도착지를 계속 탐색해서 찾아가는 함수
    void Update()
    {
        if (agent.remainingDistance >= 2.0f)
        {
            Vector3 direction = agent.desiredVelocity;
            if (direction.sqrMagnitude >= 0.1f * 0.1f)
            {
                Quaternion rot = Quaternion.LookRotation(direction);
                playerTr.rotation = Quaternion.Slerp(playerTr.rotation, rot, Time.deltaTime * 10.0f);
            }
        }
    }

    IEnumerator NavMove()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(PlayerBehaviour());
    }

    // 주인공과 매립지 거리를 확인해서 state에 반영하고 state값에 따라 이동 or 정지 상태를 코루틴으로 반복 호출
    IEnumerator PlayerBehaviour()
    {
        CheckPlayerState();
        PlayerAction();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayerBehaviour());
    }

    // 매립지와 주인공 거리를 가져와서 상태를 변환시키는 함수
    void CheckPlayerState()
    {
        float distance = Vector3.Distance(landTr.position, playerTr.position);
        if (distance <= 3.0f)
        {
            state = State.IDLE;
        }
        else
        {
            state = State.TRACE;
        }
    }

    // 입력된 상태에 따라 이동하거나 정지함
    void PlayerAction()
    {
        switch (state)
        {
            case State.IDLE:
                agent.isStopped = true;
                // 도착해서 모든 코루틴을 끄고 메시지창 코루틴 실행
                StopAllCoroutines();
                StartCoroutine(NoticeMsg());
                break;
            case State.TRACE:
                agent.SetDestination(landTr.position);
                agent.isStopped = false;
                break;
        }
    }

    // Start()에서 받은 메시지창을 실패, 성공에 따라 출력함
    IEnumerator NoticeMsg()
    {
        yield return new WaitForSeconds(2.0f);
        noticeMsg1.SetActive(true);
        if (pos == 1) audioSources[3].Play();
        else audioSources[1].Play();
        yield return new WaitForSeconds(5.0f);
        noticeMsg1.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        // 2 성공의 경우 사용자에게 슬롯 확대와 메시지창을 보여주기 위한 함수
        if (GameObj.checkGameSuccess == 2)
            StartCoroutine(ZoomSlot());
        else if (GameObj.checkGameSuccess == 1) // 1 실패의 경우 챕터4 실패 씬으로 이동
        {
            StartCoroutine(ChangeScene());
        }
    }

    // 슬롯을 확대하고 메시지창을 주고 버튼 상호작용을 대기
    IEnumerator ZoomSlot()
    {
        // 슬롯을 하나씩 확대 시킴
        for (int i = 0; i < 3; i++) 
        {
            yield return new WaitForSeconds(2.0f);
            slot[i].SetActive(false);
            yield return new WaitForSeconds(0.5f);
            slot[i + 3].SetActive(true);
            audioSources[0].Play();
        }
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i < 3; i++) 
        {
            slot[i + 3].SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        // 성공 축하 메시지를 보여줌
        GameObj.instance.uiMsg[2].SetActive(true);
        audioSources[1].Play();
        yield return new WaitForSeconds(5.0f);
        GameObj.instance.uiMsg[2].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        // 다시 업무를 이어서 할지 수락과 거절로 묻는 기능
        GameObj.instance.uiMsg[3].SetActive(true);
        audioSources[1].Play();
        successBtn[0].onClick.AddListener(AcceptBtn); // 수락 버튼
        successBtn[1].onClick.AddListener(RejectBtn); // 거절 버튼
    }

    // 수락 버튼을 선택하면 성공 2를 주고 챕터4 성공 씬으로 이동
    public void AcceptBtn()
    {
        audioSources[2].Play();
        GameObj.checkGameSuccess = 2;
        StartCoroutine(ChangeScene());
    }
    
    // 거절 버튼을 선택하면 실패 1을 주고 챕터4 실패 씬으로 이동
    public void RejectBtn()
    {
        audioSources[2].Play();
        GameObj.checkGameSuccess = 1;
        StartCoroutine(ChangeScene());
    }

    // 활성화 된 객체를 끄고 사용자를 원래 위치로 놓고 변수 값에 따라 성공, 실패 씬으로 이동
    IEnumerator ChangeScene() {
        yield return new WaitForSeconds(2.0f);
        playerTr.transform.SetPositionAndRotation(new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        GameObj.instance.uiMsg[3].SetActive(false);
        // 게임매니저에 알리기위해 기존과 다른 실패3과 성공4를 넣어서 보내고 챕터 이동
        if (GameObj.checkGameSuccess == 1)
        {
            GameObj.checkGameSuccess = 3;
            SceneLoader.Instance.LoadNewScene("Chapter04_0_fail");
            GameObj.instance.leftCtrlSaber.GetComponent<Raycast04_0>().enabled = true;
            GameObj.instance.rightCtrlSaber.GetComponent<Raycast04_0>().enabled = true;
        }
        if (GameObj.checkGameSuccess == 2)
        {
            GameObj.checkGameSuccess = 4;
            SceneLoader.Instance.LoadNewScene("Chapter04_1_blackUniverse");
        }
        yield return new WaitForSeconds(1.0f);
        terrain.SetActive(false);
        landfill.SetActive(false);
    }
}
