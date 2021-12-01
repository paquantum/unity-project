using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LodingBar : MonoBehaviour
{
    //private float initLoding = 0.05f;
    public float currLoding;
    private Image lodingBar;
    public GameObject uiMsg;

    void Start()
    {
        lodingBar = GameObject.FindGameObjectWithTag("LOADINGBAR")?.GetComponent<Image>();
        StartCoroutine(ChargeLoding());
    }

    IEnumerator ChargeLoding()
    {
        DisplayLoding();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ChargeLoding());
    }
    
    void DisplayLoding()
    {
        lodingBar.fillAmount += 0.1f;
    }
}
