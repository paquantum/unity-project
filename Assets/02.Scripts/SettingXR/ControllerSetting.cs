using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerSetting : MonoBehaviour
{
    public XRInteractorLineVisual _lineLeft;
    public XRInteractorLineVisual _lineRight;

    // Start is called before the first frame update
    void Start()
    {
        _lineRight.lineWidth = 0.001f;
        _lineRight.lineLength = 1.5f;

        _lineLeft.lineWidth = 0.001f;
        _lineLeft.lineLength = 1.5f;
    }

    public void OnChapter00()
    {
        _lineRight.lineWidth = 0f;
        _lineRight.lineLength = 0f;

        _lineLeft.lineWidth = 0f;
        _lineLeft.lineLength =  0f;
    }
    public void OnChapter03()
    {
        _lineRight.lineWidth = 0.05f;
        _lineRight.lineLength = 1.5f;

        _lineLeft.lineWidth = 0.05f;
        _lineLeft.lineLength = 1.5f;
    }
}
