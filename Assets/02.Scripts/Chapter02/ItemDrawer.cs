using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrawer : MonoBehaviour
{
    public event FloatingEventHandler ShowUI;
    public event FloatingEventHandler HideUI;
    public event FloatingEventHandler DrawerComplete;

    public Animator _aniDrawer;
    public Animator _aniCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// ����
    /// </summary>
    public void OnSelect()
    {
        //���� ����
        _aniDrawer.SetInteger("OPEN", 1);
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

    protected virtual void onDrawerComplete()
    {
        if (DrawerComplete != null)
            DrawerComplete(this);
    }
}
