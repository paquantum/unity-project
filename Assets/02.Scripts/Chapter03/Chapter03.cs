using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter03 : MonoBehaviour
{
    public Material _skybox;
    // 메시지 담을 변수
    public GameObject missionMsg;
    public GameObject questMsg;

    // 배경음, 임무안내, 정보설명, 클릭
    public AudioSource[] audioSources;

    public void Start()
    {
        RenderSettings.skybox = _skybox;
        GameManager.Instance.InitXrRigPosition();
        GameManager.Instance.ActivateMovememt(false);
        PlayerPrefs.DeleteAll();

        audioSources[0].Play();
        StartCoroutine(NoticeMsg());
    }

    IEnumerator NoticeMsg()
    {
        yield return new WaitForSeconds(2.0f);
        missionMsg.SetActive(true);
        audioSources[1].Play();
        yield return new WaitForSeconds(4.0f);
        missionMsg.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        questMsg.SetActive(true);
        audioSources[2].Play();
    }

    public void ChangeScene()
    {
        audioSources[3].Play();
        SceneLoader.Instance.LoadNewScene("Chapter03_1_game");
    }
}
