using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableScenScript : MonoBehaviour
{
    public GameObject AllTheStaff;
    private bool check = false;

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && check == false)
        {
            AllTheStaff.SetActive(true);
            check = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && check == true)
        {
            AllTheStaff.SetActive(false);
            check = false;
        }
    }
}
