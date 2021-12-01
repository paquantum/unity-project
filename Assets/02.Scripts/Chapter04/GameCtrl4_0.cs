using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl4_0 : MonoBehaviour
{
    public GameObject[] noticeMsg;

    public GameObject[] buttons;
    public BoxCollider[] btns;
    public bool greenBtn, blueBtn, redBtn, stopBtn;

    // 차량내부배경음, 시동꺼지는소리, 임무안내, 정보설명, 클릭
    public AudioSource[] audioSources;

    public static GameCtrl4_0 instance;

    // GameCtrl 인스턴스화
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSources[0].Play();
        StartCoroutine(NoticeMsg());
    }

    void Update()
    {
        if (greenBtn && !blueBtn && !redBtn)
        {
            Debug.Log("greenBtn이 true로 : " + greenBtn);
            buttons[0].GetComponent<Animator>().SetBool("greenBtn", false);
            buttons[1].GetComponent<Animator>().SetBool("blueBtn", true);
        }
        else if (greenBtn && blueBtn && !redBtn)
        {
            buttons[1].GetComponent<Animator>().SetBool("blueBtn", false);
            buttons[2].GetComponent<Animator>().SetBool("redBtn", true);
        }
        else if (greenBtn && blueBtn && redBtn)
        {
            buttons[2].GetComponent<Animator>().SetBool("redBtn", false);
            buttons[3].GetComponent<Animator>().SetBool("stopBtn", true);
        }
        if (stopBtn)
        {
            stopBtn = false;
            StartCoroutine(CompleteBtn());
        }
    }

    void PlaySound()
    {
        audioSources[1].Play();
    }

    IEnumerator NoticeMsg()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(2.0f);
            noticeMsg[i].SetActive(true);
            audioSources[2].Play();
            yield return new WaitForSeconds(4.0f);
            noticeMsg[i].SetActive(false);
        }
        noticeMsg[2].SetActive(true);
        audioSources[3].Play();
        yield return new WaitForSeconds(7.0f);
        noticeMsg[2].SetActive(false);
        // 깜박이는 애니메이션
        buttons[0].GetComponent<Animator>().SetBool("greenBtn", true);
    }

    IEnumerator CompleteBtn()
    {
        Debug.Log("버튼 모두 완성");
        audioSources[1].Play();
        yield return new WaitForSeconds(1.0f);
        audioSources[0].Stop();
        yield return new WaitForSeconds(4.0f);
        GameObj.instance.leftCtrlSaber.GetComponent<Raycast04_0>().enabled = false;
        GameObj.instance.rightCtrlSaber.GetComponent<Raycast04_0>().enabled = false;
        SceneLoader.Instance.LoadNewScene("Chapter04_1_blackUniverse");
    }
}
