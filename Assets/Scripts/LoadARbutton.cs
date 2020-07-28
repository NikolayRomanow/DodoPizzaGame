using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadARbutton : MonoBehaviour
{
    public GameObject AR_Camera;
    public GameObject Game_Camera;
    public GameObject Canvases;
    public GameObject OtherCanvases;
    public GameObject EasyAR;

    public GameObject game;

    //private bool check;

    private void Awake()
    {
        EasyAR.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(VirubatEasyAR());
    }

    public void AR()
    {
        game.SetActive(false);
        EasyAR.SetActive(true);
        AR_Camera.SetActive(true);
        Canvases.SetActive(false);
        OtherCanvases.SetActive(true);
        Game_Camera.SetActive(false);
    }

    public void Game()
    {
        game.SetActive(true);
        EasyAR.SetActive(false);
        Game_Camera.SetActive(true);
        Canvases.SetActive(true);
        OtherCanvases.SetActive(false);
        AR_Camera.SetActive(false);
    }

    IEnumerator VirubatEasyAR()
    {
        yield return new WaitForSeconds(1f);
        EasyAR.SetActive(false);
    }
}
