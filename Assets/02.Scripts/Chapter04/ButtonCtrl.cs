using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public Animator greenBtnAnim;
    public Animator blueBtnAnim;
    public Animator redBtnAnim;
    public Animator stopBtnAnim;
    public bool _greenBtn, _blueBtn, _redBtn, _stopBtn;
    public BoxCollider[] boxColliders;
    private int pos = 0;

    public void OnSelectedGreen()
    {
        if (!_greenBtn)
        {
            Debug.Log("초록버튼셀렉");
            greenBtnAnim.SetBool("pushBtn", true);
            boxColliders[pos].isTrigger = true;
            StartCoroutine(DelayTime());
            greenBtnAnim.SetBool("greenBtn", false);
            GameCtrl4_0.instance.greenBtn = true;
            _greenBtn = true;
        }
    }

    public void OnSelectedBlue()
    {
        if (!_blueBtn && _greenBtn)
        {
            blueBtnAnim.SetBool("pushBtn", true);
            boxColliders[pos].isTrigger = true;
            StartCoroutine(DelayTime());
            greenBtnAnim.SetBool("blueBtn", false);
            GameCtrl4_0.instance.blueBtn = true;
            _blueBtn = true;
        }
    }

    public void OnSelectedRed()
    {
        if (!_redBtn && _blueBtn)
        {
            redBtnAnim.SetBool("pushBtn", true);
            boxColliders[pos].isTrigger = true;
            StartCoroutine(DelayTime());
            greenBtnAnim.SetBool("redBtn", false);
            GameCtrl4_0.instance.redBtn = true;
            _redBtn = true;
        }
    }

    public void OnSelectedStop()
    {
        if (!_stopBtn && _redBtn)
        {
            blueBtnAnim.SetBool("stopBtn", false);
            boxColliders[pos].isTrigger = true;
            GameCtrl4_0.instance.stopBtn = true;
            _stopBtn = true;
        }
    }

    IEnumerator DelayTime()
    {
        pos += 1;
        yield return new WaitForSeconds(1.0f);
        boxColliders[pos].isTrigger =  false;
    }
}
