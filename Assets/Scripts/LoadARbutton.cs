using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadARbutton : MonoBehaviour
{
    public GameObject AR_Camera;
    public GameObject Game_Camera;
    public GameObject Canvases;
    public GameObject OtherCanvases;

    public void AR()
    {
        AR_Camera.SetActive(true);
        Canvases.SetActive(false);
        OtherCanvases.SetActive(true);
        Game_Camera.SetActive(false);
    }

    public void Game()
    {
        Game_Camera.SetActive(true);
        Canvases.SetActive(true);
        OtherCanvases.SetActive(false);
        AR_Camera.SetActive(false);
    }
}
