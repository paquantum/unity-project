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
        //���ε�� ǥ���� é�� Ÿ��Ʋ 
        switch (_sceneName)
        {
            case "Chapter01":
                _ChapterTitle.text = "Chapter 1. �༺ ����";
                break;
            case "Chapter02":
                _ChapterTitle.text = "Chapter 2. ��ô ����";
                break;
            case "Chapter03":
                _ChapterTitle.text = "Chapter 3. ��ȭ �۾�";
                break;
            case "Chapter04-1":
                _ChapterTitle.text = "Chapter 4. ����ȭ ����";
                break;
            case "Chapter04-2":
                _ChapterTitle.text = "Chapter 4. ����ȭ ����";
                break;
        }
    }

    public void MissonTitle(int _idx)
    {
        _curMission = _idx;
        //���ε�� ǥ���� é�� Ÿ��Ʋ 
        switch (_curMission)
        {
            case 0:
                _MisstionTitle.text = "�ӹ�1-1 : �ڽ��� ����� Ȯ���ϼ���.";
                break;
            case 1:
                _MisstionTitle.text = "�ӹ�1-1 : �Ϸ�!";
                break;
            case 2:
                _MisstionTitle.text = "�ӹ�1-2 : ��༭�� �����ϼ���.";
                break;
            case 3:
                _MisstionTitle.text = "�ӹ�1-2 : �Ϸ�!";
                break;
            case 4:
                _MisstionTitle.text = "�ӹ�2-1 : ���θ� �ѷ�������.";
                break;
            case 5:
                _MisstionTitle.text = "�ӹ�2-1 : �Ϸ�!";
                break;
            case 6:
                _MisstionTitle.text = "�ӹ�2-2 : ID ī�带 �̿��� ���� ���� ��������.";
                break;
            case 7:
                _MisstionTitle.text = "�ӹ�2-2 : �Ϸ�!";
                break;
            case 8:
                _MisstionTitle.text = "�ӹ�2-3 : ���� �ѷ�������.";
                break;
            case 9:
                _MisstionTitle.text = "�ӹ�2-3 : �Ϸ�!";
                break;
            case 10:
                _MisstionTitle.text = "�ӹ�2-4 : ��ȭ ������ ž���ϼ���.";
                break;
            case 11:
                _MisstionTitle.text = "�ӹ�2-4 : �Ϸ�!";
                break;
            case 12:
                _MisstionTitle.text = "�ӹ�3-1 : ��ȭ �۾��� �Ϸ��ϼ���.";
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
