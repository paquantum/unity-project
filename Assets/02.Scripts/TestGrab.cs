using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestGrab : MonoBehaviour
{
    public XRGrabInteractable _grab;
    public GameObject _tablet;
    public GameObject _box;
    public GameObject _docx;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool isGrab = false;
    // Update is called once per frame
    void Update()
    {
        if (isGrab)
        {
            _tablet.GetComponent<Transform>().localPosition = GameManager.Instance._Left.GetComponent<Transform>().localPosition;
            _tablet.GetComponent<Transform>().localRotation = GameManager.Instance._Left.GetComponent<Transform>().localRotation;

            //_obj.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(GameManager.Instance._Left.GetComponent<Transform>().localRotation.x, GameManager.Instance._Left.GetComponent<Transform>().localRotation.y, GameManager.Instance._Left.GetComponent<Transform>().localRotation.z - 85f));
        }
    }

    public void OnDrop()
    {
        isGrab = false;
        _box.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 0f));
        _tablet.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 0f));
        _tablet.GetComponent<Transform>().localPosition = new Vector3(0.015f, -0.28f, 2.129f);

        _docx.GetComponent<BoxCollider>().isTrigger = true;

    }
    public void OnTest()
    {
        if (!isGrab)
        {
            isGrab = true;
            _docx.GetComponent<BoxCollider>().isTrigger = false;
            _box.GetComponent<Rigidbody>().isKinematic = true;
            _box.GetComponent<Rigidbody>().useGravity = false;
            _box.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
            _box.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -80f));
        }
    }
}
