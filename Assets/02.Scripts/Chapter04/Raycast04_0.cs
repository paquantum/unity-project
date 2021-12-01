using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast04_0 : MonoBehaviour
{
    RaycastHit hit;
    // Ray 길이
    float MaxDistance = 100.0f;
    private bool _greenBtn, _blueBtn, _redBtn, _stopBtn;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            if (hit.collider.CompareTag("GREENBTN") && !_greenBtn)
            {
                Debug.Log("초록버튼 끄고 들어옴?");
                hit.collider.GetComponent<Animator>().SetBool("greenBtn", false);
                audioSource.Play();
                //hit.transform.GetComponent<Animator>().SetBool("greenBtn", false);
                GameCtrl4_0.instance.greenBtn = true;
                _greenBtn = true;
                //GameCtrl4_0.instance.flag = false;
                //hit.transform.GetComponent<Animator>().SetBool("blueBtn", true);
            }
            else if (hit.collider.CompareTag("BLUEBTN") && _greenBtn && !_blueBtn)
            {
                hit.collider.GetComponent<Animator>().SetBool("blueBtn", false);
                audioSource.Play();
                //hit.transform.GetComponent<Animator>().SetBool("blueBtn", false);
                GameCtrl4_0.instance.blueBtn = true;
                _blueBtn = true;
                //hit.transform.GetComponent<Animator>().SetBool("redBtn", true);
            }
            else if (hit.collider.CompareTag("REDBTN") && _greenBtn && _blueBtn && !_redBtn)
            {
                hit.collider.GetComponent<Animator>().SetBool("redBtn", false);
                audioSource.Play();
                //hit.transform.GetComponent<Animator>().SetBool("redBtn", false);
                GameCtrl4_0.instance.redBtn = true;
                _redBtn = true;
                //GameCtrl4_0.instance.stopBtn = true;
                //hit.transform.GetComponent<Animator>().SetBool("stopBtn", true);
            }
            else if (hit.collider.CompareTag("STOPBTN") && _greenBtn && _blueBtn && _redBtn && !_stopBtn)
            {
                hit.collider.GetComponent<Animator>().SetBool("stopBtn", false);
                audioSource.Play();
                //hit.transform.GetComponent<Animator>().SetBool("stopBtn", false);
                GameCtrl4_0.instance.stopBtn = true;
                _stopBtn = true;
                //hit.transform.GetComponent<Animator>().SetBool("stopBtn", true);
            }
        }
    }
}
