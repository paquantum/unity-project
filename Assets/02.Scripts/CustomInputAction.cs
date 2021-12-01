using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputAction : MonoBehaviour
{
    public InputActionReference _toggleReference = null;
    private void Awake()
    {
        _toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        _toggleReference.action.started -= Toggle;

    }
   
    private void Toggle(InputAction.CallbackContext context)
    {
        /*bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);*/
        SceneLoader.Instance.LoadNewScene("Chapter01");

    }
}
