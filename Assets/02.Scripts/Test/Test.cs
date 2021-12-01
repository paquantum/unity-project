using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Test : MonoBehaviour
{
    Volume volume;
    public ColorAdjustments colorAdjustments;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out ColorAdjustments colorAdjustments);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Color()
    {
        while (true)
        {
            //colorAdjustments.colorFilter.Interp(UnityEngine.Color.white, UnityEngine.Color.black, Time.fixedDeltaTime);
            Color temp = colorAdjustments.colorFilter.value;
            temp.r -= Time.fixedDeltaTime;
            temp.g -= Time.fixedDeltaTime;
            temp.b -= Time.fixedDeltaTime;
            colorAdjustments.colorFilter.value = temp;
            yield return new WaitForFixedUpdate();

            if (colorAdjustments.colorFilter.value.r <= 0)
            {
                break;
            }
        }
    }
}
