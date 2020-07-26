using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARUI : MonoBehaviour
{
    public GameObject SearchPanel;
    public GameObject ImageTarget;
    public bool isOn;

    private void Update()
    {
        if (ImageTarget.activeSelf && !isOn)
        {
            isOn = true;
            SearchPanel.SetActive(false);
        }

        if (!ImageTarget.activeSelf && isOn)
        {
            isOn = false;
            SearchPanel.SetActive(true);
        }
    }

    public void Domoy()
    {
        Application.LoadLevel(0);
    }
}
