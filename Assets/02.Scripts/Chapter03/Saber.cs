using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saber : MonoBehaviour
{
    // 정화세이버 중에 SettingXR 씬의 양쪽 컨트롤러에 달 스크립트로, 같은 layer로 해놨을 때 동시에 베었을 때, 슬롯이 2개가 변경되는 이슈가 있었음

    // 컨트롤러로 해당 layer만 베기
    public LayerMask layer;
    private Vector3 previousPos;
    public AudioSource[] au;
    // Slot객체 받아오는 변수
    public GameObject[] slot;

    // 바꿀 이미지 받아오는 변수
    public Texture[] slot_tex;

    public GameObject leftController;
    public GameObject rightController;

    void Update()
    {
        // 게임매니저에서 settingXR로 슬롯이미지 객체를 가져옴
        slot = GameCtrl.instance.slotImages;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        {
            // hit한 순간에 사운드 효과
            if (hit.collider.CompareTag("CUBE"))
                au[0].Play();
            if (hit.collider.CompareTag("ITEMBOX"))
                au[1].Play();
            // lock이 true일 때만 진입하고 바로 false로 바꿔서 잠금, 동시에 벨 때 슬롯 두개가 변경되는 것을 방지
            if (GameCtrl.instance._lock)
            {   
                GameCtrl.instance._lock = false;
                // slot의 위치를 가져와서 해당 위치 이미지 교체하는 기능
                int pos = GameCtrl.instance.slotPos;
                // 총 3개의 슬롯을 벗어나지 않고 벤 객체 태그가 ITEMBOX인 경우
                if (pos < 3 && hit.collider.CompareTag("ITEMBOX"))
                {   // 해당 슬롯 위치에 이미지를 바꿔 줌
                    slot[pos].SetActive(true);
                    GameCtrl.instance.slotPos += 1;
                }
                // 0.5초 정도 후에 lock을 false에서 true로 바꿔주는 코루틴
                StartCoroutine(LockState());
            }
            Destroy(hit.transform.gameObject);
        }
        previousPos = transform.position;
    }

    // lock을 false에서 true로 바꿔주는 코루틴
    IEnumerator LockState()
    {
        yield return new WaitForSeconds(0.5f);
        GameCtrl.instance._lock = true;
    }
}
