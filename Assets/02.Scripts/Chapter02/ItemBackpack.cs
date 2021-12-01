using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBackpack : MonoBehaviour
{
    public event FloatingEventHandler ShowUI;
    public event FloatingEventHandler HideUI;
    public event FloatingEventHandler BackpackComplete;

    public Animator _aniTablet;
    public Animator _aniCard;
    void Start()
    {
        
    }
    /// <summary>
    /// 선택
    /// </summary>
    public void OnSelect()
    {
        //서랍 열림
        _aniTablet.SetInteger("OPEN", 1);
        _aniCard.SetInteger("OPEN", 1);
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

    protected virtual void onBackpackComplete()
    {
        if (BackpackComplete != null)
            BackpackComplete(this);
    }

}
