using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObj04 : MonoBehaviour
{
    // XR rig를 움직여서 위치를 알아내기 위해 만들었지만
    // 위치를 고정시키기로 해서 사용하지 않을 예정
    
    public GameObject _XRrig;
    public GameObject _Left;
    public ActionBasedController _LeftCtrl;
    public ActionBasedContinuousMoveProvider _movement;
    public GameObject[] noticeMsg;
    public bool _distance;
    public int state;

    public static GameObj04 instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (_distance)
        {
            _distance = false;
            StartCoroutine(NoticeMsg());
        }
    }

    IEnumerator NoticeMsg()
    {
        yield return new WaitForSeconds(5.0f);
        noticeMsg[state].SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5.0f);
        noticeMsg[state].SetActive(false);
        yield return new WaitForSeconds(10.0f);
        SceneLoader.Instance.LoadNewScene("Chapter04_3_credit");
    }
}
