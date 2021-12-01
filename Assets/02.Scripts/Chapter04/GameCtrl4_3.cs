using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl4_3 : MonoBehaviour
{
    public GameObject[] fail_illust;
    public GameObject[] success_illust;
    //private int pos = 10;
    public GameObject book;
    public AudioSource audioSource;
    public Material _skybox;

    void Start()
    {
        RenderSettings.skybox = _skybox;
        GameManager.Instance.InitXrRigPosition();
        GameManager.Instance.ActivateMovememt(false);
        GameManager.Instance._XRrig.transform.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(ShowImages());
    }

    IEnumerator ShowImages()
    {
        yield return new WaitForSeconds(5.0f);
        if (GameObj.checkGameSuccess == 3)
        {
            for (int i = 0; i < 5; i++)
            {
                fail_illust[i].SetActive(true);
                yield return new WaitForSeconds(3.0f);
                fail_illust[i].SetActive(false);
                yield return new WaitForSeconds(1.0f);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                success_illust[i].SetActive(true);
                yield return new WaitForSeconds(3.0f);
                success_illust[i].SetActive(false);
                yield return new WaitForSeconds(1.0f);
            }
        }
        
        book.GetComponent<Animator>().SetBool("bookAnim", true);
        yield return new WaitForSeconds(3.0f);
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);
        fail_illust[5].SetActive(true);
        fail_illust[6].SetActive(true);
        yield return new WaitForSeconds(7.0f);
        fail_illust[5].SetActive(false);
        fail_illust[6].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        fail_illust[7].SetActive(true);
        yield return new WaitForSeconds(3.0f);
        fail_illust[7].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        ChangeScene();
        
    }
    void ChangeScene()
    {
        GameObj.checkGameSuccess = -1;
        GameObj.instance.autoMove = true;
        SceneLoader.Instance.LoadNewScene("Chapter03_0");
    }
}
