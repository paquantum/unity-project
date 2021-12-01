using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class GameCtrl : MonoBehaviour
{
    // 씬 Chapter03_1_game 게임매니저
    
    // 실수 가능한 횟수 담은 변수
    public int heartCount = 5;
    // 하트 객체 담을 변수
    public GameObject[] hpImages;
    // 슬롯 객체 담을 변수
    public GameObject[] slotImages;

    // 슬롯에 넣을 Texture와 Image 소스
    public Texture[] slotTex;
    public Image[] changeSlotImages;
    // 현재 바뀔 슬롯 위치 값 담은 변수
    public int slotPos = 0;

    // 양 쪽 컨트롤이 같은 객체를 벨 때 ITEMBOX가 2개 베지 않도록 잠그는 변수
    public bool _lock = true;
    // 장애물 스폰하는 객체
    public GameObject spawnerCube;

    // 업무 재개 메시지 보관 변수
    public GameObject resumeMsg;
    public Button resumeBtn;
    public GameObject failMsg;
    public GameObject sucMsg;
    public bool _resume;

    // 업무 재개 가능한 횟수 변수
    public int remainRound = 3;

    // 시간 표시할 객체
    public GameObject timer;

    // 게임 상태 여부 0은 진행중, 1은 실패, 2는 성공
    public int GameSuccess = 0;
    // 게임 상태에 따라 업데이트에서 불리는 것을 제한하기 위한 변수
    private bool _gameSuccess = true;

    // 게임오버인지 상태 체크 변수
    private bool gameOver = true;

    // 내부 레지스터에 저장된 데이터가 있는지 확인하기 위한 변수
    private bool isSave;

    // GameCtrl 인스턴스화를 위해 선언
    public static GameCtrl instance;

    public TMP_Text textField;

    // 배경음, 차량시동, 임무안내, 정보설명, 버튼클릭
    public AudioSource[] audioSources;

    private GameObject lineVisual;

    // GameCtrl 인스턴스화
    void Awake()
    {
        instance = this;
    }
    
    // 내부 레지스터에 데이터를 가져와서 활성화 된 slot은 다시 활성화 하고
    // 내부 레지스터 값 지움
    void Start()
    {
        audioSources[0].Play();
        audioSources[1].Play();
        StartCoroutine(StartGame());
        GameObj.instance.leftShape[0].SetActive(false);
        GameObj.instance.rightShape[0].SetActive(false);
        GameObj.instance.leftShape[1].SetActive(true);
        GameObj.instance.rightShape[1].SetActive(true);
        GameObj.instance.leftCtrlSaber.GetComponent<XRInteractorLineVisual>().enabled = false;
        GameObj.instance.rightCtrlSaber.GetComponent<XRInteractorLineVisual>().enabled = false;
        // 기존에 저장된 값이 있는지 확인
        isSave = PlayerPrefs.HasKey("SlotPos");
        // 데이터가 있다면 불러오고 없으면 넘어감
        if (isSave)
        {
            //Debug.Log("저장된 데이터가 있습니다");
            slotPos = PlayerPrefs.GetInt("SlotPos");
            remainRound = PlayerPrefs.GetInt("RemainRound");
            //Debug.Log("현재 slotPos값 : " + slotPos);
            // 오픈된 슬롯 만큼 다시 열어주는 역할
            for (int i = 0; i < slotPos; i++)
            {
                slotImages[i].SetActive(true);
            }
        }
        else
        {
            Debug.Log("저정된 데이터가 없습니다");
        }
        // 불러왔기 때문에 저장된 데이터 전체 삭제함
        PlayerPrefs.DeleteAll();
        GameObj.objManage = 1;
    }

    // 게임 실패 후 업무재개 화면
    void Update()
    {
        resumeBtn.onClick.AddListener(DataSave);
        // 게임오버되고 하트 갯수가 0이라면 오디오를 멈추고 업무재개 메시지창 띄움
        if (gameOver && heartCount == 0)
        {
            gameOver = false;
            ChangeController();
            ResumeGame();
        }
        // 게임을 성공하면 장애물 스폰과 오디오를 끄고 성공 메시지창 띄움
        if (GameSuccess == 2 && _gameSuccess)
        {
            _gameSuccess = false;
            ChangeController();
            spawnerCube.SetActive(false);
            audioSources[0].Stop();
            timer.SetActive(false);
            StartCoroutine(SucMsg());
        }
    }

    // 5개 목숨을 다 잃고 나타나는 업무재개창과 
    // 몇번째 시도인지에 따라 메시지창에 기회가 몇 번 남았는지 알리는 역할
    public void ResumeGame() {
        spawnerCube.SetActive(false);
        audioSources[0].Stop();
        GameObj.checkGameSuccess = -1;
        resumeMsg.SetActive(true);
        audioSources[3].Play();
        remainRound -= 1;
        //Debug.Log("remainRound : " + remainRound);
        textField.text = "남은 기회 : [ " + remainRound + " ]";
        // 남은 라운드가 0인경우 실패 메시지 출력
        if (remainRound == 0)
        {
            resumeMsg.SetActive(false);
            timer.SetActive(false);
            StartCoroutine(FailMsg());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5.0f);
        GameObj.checkGameSuccess = 0;
    }

    // 실패 메시지를 보여준 후 게임 실패 씬으로 이동
    IEnumerator FailMsg()
    {
        GameObj.objManage = 0;
        yield return new WaitForSeconds(2.0f);
        failMsg.SetActive(true);
        audioSources[3].Play();
        yield return new WaitForSeconds(3.0f);
        GameObj.checkGameSuccess = 1;
        SceneLoader.Instance.LoadNewScene("Chapter03_2_landFill");
    }

    // 게임 성공 메시지를 보여준 후 게임 성공 씬으로 이동
    IEnumerator SucMsg()
    {
        GameObj.objManage = 0;
        yield return new WaitForSeconds(2.0f);
        sucMsg.SetActive(true);
        audioSources[3].Play();
        yield return new WaitForSeconds(3.0f);
        GameObj.checkGameSuccess = 2;
        SceneLoader.Instance.LoadNewScene("Chapter03_2_landFill");
    }

    public void DataSave()
    {
        audioSources[4].Play();
        SceneLoader.Instance.LoadNewScene("Chapter03_1_game");
        resumeMsg.SetActive(false);
        int slotPos = GameCtrl.instance.slotPos;
        int remainRound = GameCtrl.instance.remainRound;
        //Debug.Log("저장할 slotpos, remainround 값 : " + slotPos + " " + remainRound);
        PlayerPrefs.SetInt("SlotPos", slotPos);
        PlayerPrefs.SetInt("RemainRound", remainRound);
        PlayerPrefs.Save();
        Debug.Log("저장 되었습니다");
    }

    void ChangeScene()
    {
        SceneLoader.Instance.LoadNewScene("Chapter03_1_game");
    }

    void ChangeController()
    {
        GameObj.instance.leftShape[0].SetActive(true);
        GameObj.instance.rightShape[0].SetActive(true);
        GameObj.instance.leftShape[1].SetActive(false);
        GameObj.instance.rightShape[1].SetActive(false);
        GameObj.instance.leftCtrlSaber.GetComponent<XRInteractorLineVisual>().enabled = true;
        GameObj.instance.rightCtrlSaber.GetComponent<XRInteractorLineVisual>().enabled = true;
    }
}
