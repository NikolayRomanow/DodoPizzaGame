using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetAnotherARscript : MonoBehaviour
{
    private bool isOn = false;
    public GameObject Canvas;
    public GameObject ImageTarget;

    private void Update()
    {
        if (ImageTarget.activeSelf && !isOn)
        {
            isOn = true;
            Canvas.SetActive(false);
        }

        if (!ImageTarget.activeSelf && isOn)
        {
            isOn = false;
            Canvas.SetActive(true);
        }
    }
}
