using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejScene : MonoBehaviour
{
    public GameObject Scene1, Scene2, Scene3;
    public Animator StartMenu, ARMenu1, ARMenu2, ARMenu3;
    public UIController UIController;

    public void Zapusk1()
    {
        UIController.MoreButtonOff();
        StartCoroutine(Ojidanuie());
    }

    public void Zapusk2()
    {
        Scene2.SetActive(false);
        Scene1.SetActive(true);
    }
    
    public void Zapusk3()
    {
        Scene2.SetActive(false);
        Scene3.SetActive(true);
        ARMenu3.SetTrigger("On");
    }

    public void Zapusk4()
    {
        Scene3.SetActive(false);
        Scene2.SetActive(true);
        ARMenu1.Play("On");
        ARMenu2.Play("On");
    }

    IEnumerator Ojidanuie()
    {
        yield return new WaitForSeconds(1f);
        Scene1.SetActive(false);
        Scene2.SetActive(true);
        ARMenu1.Play("On");
        ARMenu2.Play("On");
    }
}
