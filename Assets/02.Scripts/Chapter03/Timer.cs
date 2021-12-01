using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // 3_1 정화세이버에서 시간을 보여주기 위해 사용하는 스크립트

    private float startTime;
    private string textTime;
    private float guiTime;
    private int minutes;
    private int seconds;
    private int fraction;
    private bool progressGame;
    private bool flag;

    public TMP_Text textField;
    
    // 시간은 70초로 초기 설정과 진행중인 상태를 true로 넣음
    void Start()
    {
        progressGame = true;
        flag = true;
    }

    // 진행중일동안 시간 보여주는 함수 실행
    void Update()
    {
        if (progressGame && GameObj.checkGameSuccess == 0)
        {
            ShowTime();
        }
        if (flag && GameObj.checkGameSuccess == 0)
        {
            flag = false;
            startTime = Time.time + 20;
        }
    }

    // 시간을 계산해서 보여주는 함수
    void ShowTime()
    {
        // 하트가 없으면 비활성화 시킴
        if (GameCtrl.instance.heartCount == 0)
            gameObject.SetActive(false);
        guiTime =  startTime - Time.time;
        minutes = (int)guiTime / 60;
        seconds = (int)guiTime % 60;
        fraction = (int)(guiTime * 100) % 100;
        textTime = string.Format("{0:00}:{1:00}", minutes, seconds, fraction);
        // 제한 시간동안 살아있거나 아이템 슬롯을 채워서 성공한 경우 진입하는 부분
        if (minutes == 0 && seconds == 0 || GameCtrl.instance.GameSuccess == 2) 
        {
            //Debug.Log("slotPos값 : " + GameCtrl.instance.slotPos);
            if (GameCtrl.instance.slotPos < 3)
            {
                textField.text = "00:00";
                GameCtrl.instance.ResumeGame();
            }
            else
            {
                GameCtrl.instance.GameSuccess = 2;
                //Debug.Log("GameSuccess 값 : " + GameCtrl.instance.GameSuccess);
                progressGame = false;
            }
        }
        else
        {
            textField.text = textTime;
        }
    }
}
