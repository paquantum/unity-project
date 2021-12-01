using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public delegate void FloatingEventHandler(object sender);

public class ItemContractTablet : MonoBehaviour
{
    public event FloatingEventHandler ShowUI;
    public event FloatingEventHandler HideUI;
    public event FloatingEventHandler ContractConfirmComplete;
    public event FloatingEventHandler ContractSignComplete;

    public GameObject _ContractTablet;
    public SpriteRenderer _Sign;
    private Vector3 _tabletposition;
    private Quaternion _tabletrotation;

    //collider
    public BoxCollider _colTablet;
    public BoxCollider _colBtnOK;
    public BoxCollider _colBtnSign;

    //Interactable 
    public XRGrabInteractable contractTablet;
    public XRSimpleInteractable btnOK;
    public XRSimpleInteractable btnSign;

    //확인, 서명버튼 flag
    private bool _btnChange = false;
    private bool _deActivate = false;

    //인터렉션 UI 설정
    void Start()
    {
        _tabletposition = _ContractTablet.GetComponent<Transform>().localPosition;
        _tabletrotation = _ContractTablet.GetComponent<Transform>().localRotation;
        //PlayMemory();
    }

    /// <summary>
    /// 잡기
    /// </summary>
    public void OnGrab()
    {
        Debug.Log("OnGrab");
        onHideUI();

        //isTrigger
        _colTablet.isTrigger = true;

        if(!_btnChange)
        {
            //확인 버튼 활성화
            btnOK.gameObject.SetActive(true);
            btnSign.gameObject.SetActive(false);
        }
        else
        {
            //서명 버튼 활성화
            btnSign.gameObject.SetActive(true);
            btnOK.gameObject.SetActive(false);
        }

    }
    /// <summary>
    /// 떨어뜨림
    /// </summary>
    public void OnDrop()
    {
        if(!_deActivate)
        {
            Debug.Log("OnDrop");
            onShowUI();

            btnOK.gameObject.SetActive(false);
            btnSign.gameObject.SetActive(false);

            //isTrigger
            _colTablet.isTrigger = false;

            //원래 자리로 돌아감
            _ContractTablet.GetComponent<Transform>().localPosition = _tabletposition;
            _ContractTablet.GetComponent<Transform>().localRotation = _tabletrotation;
        }
    }
    /// <summary>
    /// 확인버튼 누름
    /// </summary>
    public void OnSelectConfirm()
    {
        Debug.Log("OnSelectConfirm");
        //isTrigger
        _deActivate = true;
        contractTablet.enabled = false;

        //원래 자리로 돌아감
        _ContractTablet.GetComponent<Transform>().localPosition = _tabletposition;
        _ContractTablet.GetComponent<Transform>().localRotation = _tabletrotation;

        //충돌무시, 책상위에 고정
        _ContractTablet.GetComponent<Rigidbody>().isKinematic = true;

        btnOK.gameObject.SetActive(false);
        btnSign.gameObject.SetActive(false);

        onContractConfirmComplete();
    }
    /// <summary>
    /// 서명확인 버튼 누름
    /// </summary>
    public void OnSelectSign()
    {
        _deActivate = true;
        btnOK.gameObject.SetActive(false);
        btnSign.gameObject.SetActive(false);

        //isTrigger
        _colTablet.isTrigger = true;
        _colBtnOK.isTrigger = true;
        _colBtnSign.isTrigger = true;

        //interaction
        float _alpha = _Sign.color.a;
        while (_alpha <= 1f)
        {
            _alpha += 1f * Time.deltaTime;
            _Sign.color = new Color(255,255,255, _alpha);
        }
        //mission complete
        onContractSignComplete();
        //챕터타이틀
    }
    public void ActivateContract()
    {
        Debug.Log("ActivateContract");
        _btnChange = true;
        _deActivate = false;
        contractTablet.enabled = true;
        _ContractTablet.GetComponent<Rigidbody>().isKinematic = false;
        _colTablet.isTrigger = false;
    }

    ////////////////////////////////////////////////////
    // Floating Event
    ////////////////////////////////////////////////////
    protected virtual void onShowUI()
    {
        if (ShowUI != null)
            ShowUI(this);
    }
    protected virtual void onHideUI()
    {
        if (HideUI != null)
            HideUI(this);
    }
    protected virtual void onContractConfirmComplete()
    {
        if (ContractConfirmComplete != null)
            ContractConfirmComplete(this);
    }

    protected virtual void onContractSignComplete()
    {
        if (ContractSignComplete != null)
            ContractSignComplete(this);
    }

}
