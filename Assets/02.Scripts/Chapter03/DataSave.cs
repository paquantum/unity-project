using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSave : MonoBehaviour
{
    public AudioSource audioSource;
    public int slotPos;
    public int remainRound;

    // 업무 재개 버튼을 누를경우 현재 오픈된 슬롯 갯수와 남은 라운드 수를 저장
    public void Save()
    {
        Debug.Log("DataSave 들어옴");
        slotPos = GameCtrl.instance.slotPos;
        remainRound = GameCtrl.instance.remainRound;
        Debug.Log("저장할 slotpos, remainround 값 : " + slotPos + " " + remainRound);
        PlayerPrefs.SetInt("SlotPos", slotPos);
        PlayerPrefs.SetInt("RemainRound", remainRound);
        PlayerPrefs.Save();
        Debug.Log("저장 되었습니다");
        ChangeScene();
    }

    // 업무 재개를 위해 씬 리로드
    public void ChangeScene()
    {
        SceneLoader.Instance.LoadNewScene("Chapter03_1_game");
    }
}
