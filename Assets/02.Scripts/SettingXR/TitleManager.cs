using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    //public string _Chapter = "Chapter00";
    public int _curMission = 0;
    public Text _ChapterTitle;
    public Text _MisstionTitle;

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _txt_intensity = 0.0f;

    private void Start()
    {
        _MisstionTitle.color = new Color(_MisstionTitle.color.r, _MisstionTitle.color.g, _MisstionTitle.color.b, 0);
    }
    public void ChapterTitle(string _sceneName)
    {
        //씬로드시 표시할 챕터 타이틀 
        switch (_sceneName)
        {
            case "Chapter01":
                _ChapterTitle.text = "Chapter 1. 행성 착륙";
                break;
            case "Chapter02":
                _ChapterTitle.text = "Chapter 2. 개척 시작";
                break;
            case "Chapter03":
                _ChapterTitle.text = "Chapter 3. 정화 작업";
                break;
            case "Chapter04-1":
                _ChapterTitle.text = "Chapter 4. 지구화 실패";
                break;
            case "Chapter04-2":
                _ChapterTitle.text = "Chapter 4. 지구화 성공";
                break;
        }
    }

    public void MissonTitle(int _idx)
    {
        _curMission = _idx;
        //씬로드시 표시할 챕터 타이틀 
        switch (_curMission)
        {
            case 0:
                _MisstionTitle.text = "임무1-1 : 자신의 기억을 확인하세요.";
                break;
            case 1:
                _MisstionTitle.text = "임무1-1 : 완료!";
                break;
            case 2:
                _MisstionTitle.text = "임무1-2 : 계약서에 서명하세요.";
                break;
            case 3:
                _MisstionTitle.text = "임무1-2 : 완료!";
                break;
            case 4:
                _MisstionTitle.text = "임무2-1 : 본부를 둘러보세요.";
                break;
            case 5:
                _MisstionTitle.text = "임무2-1 : 완료!";
                break;
            case 6:
                _MisstionTitle.text = "임무2-2 : ID 카드를 이용해 차고 문을 열으세요.";
                break;
            case 7:
                _MisstionTitle.text = "임무2-2 : 완료!";
                break;
            case 8:
                _MisstionTitle.text = "임무2-3 : 차고를 둘러보세요.";
                break;
            case 9:
                _MisstionTitle.text = "임무2-3 : 완료!";
                break;
            case 10:
                _MisstionTitle.text = "임무2-4 : 정화 차량에 탑승하세요.";
                break;
            case 11:
                _MisstionTitle.text = "임무2-4 : 완료!";
                break;
            case 12:
                _MisstionTitle.text = "임무3-1 : 정화 작업을 완료하세요.";
                break;
           
        }
    }

    public void ShowMisson()
    {
        _txt_intensity = 0;
        StopAllCoroutines();
        StartCoroutine(ShowMissonText());
    }

    private IEnumerator ShowMissonText()
    {
        //Fade-in
        _MisstionTitle.color = new Color(_MisstionTitle.color.r, _MisstionTitle.color.g, _MisstionTitle.color.b, 0);
        while (_txt_intensity <= 1.0f)
        {
            _txt_intensity += _speed * Time.deltaTime;
            _MisstionTitle.color = new Color(_MisstionTitle.color.r, _MisstionTitle.color.g, _MisstionTitle.color.b, _txt_intensity);
            yield return null;
        }

        yield return new WaitForSeconds(2f); //delay

        //Fade-out
        _MisstionTitle.color = new Color(_MisstionTitle.color.r, _MisstionTitle.color.g, _MisstionTitle.color.b, 1);
        while (_txt_intensity >= 0.0f)
        {
            _txt_intensity -= _speed * Time.deltaTime;
            _MisstionTitle.color = new Color(_MisstionTitle.color.r, _MisstionTitle.color.g, _MisstionTitle.color.b, _txt_intensity);
            yield return null;
        }

    }

}
