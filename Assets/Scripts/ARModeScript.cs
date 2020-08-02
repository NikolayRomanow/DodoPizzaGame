using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARModeScript : MonoBehaviour
{
    public GameObject RunnerGreen, RunnerRed, RunnerYellow, RunnerBlue;
    private int _gamersRating;

    private void Start()
    {
        _gamersRating = PlayerPrefs.GetInt("BestScore");
        _gamersRating = 300;
        if (_gamersRating >= 100 && _gamersRating < 200)
        {
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 200 && _gamersRating < 400)
        {
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 400 && _gamersRating < 600)
        {
            RunnerYellow.SetActive(true);
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 600)
        {
            RunnerYellow.SetActive(true);
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
            RunnerRed.SetActive(true);
        }
    }
}
