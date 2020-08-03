using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DodoDromeScript : MonoBehaviour
{
    public GameObject RunnerGreen, RunnerRed, RunnerYellow, RunnerBlue;
    public GameObject HelloUI, HelloDodo;
    public GameObject AR_Button, HoumeButton;
    public GameObject SwipedIcon;
    public Animator ProgressBarAnimator;
    public Text GamersRatingPanel;
    private int _gamersRating;

    private void Start()
    {
        var i = PlayerPrefs.GetInt("FirstLaunchDododrom");
        if (i == 0)
        {
            HelloUI.SetActive(true);
            HelloDodo.SetActive(true);
            PlayerPrefs.SetInt("FirstLaunchDododrom", 1);
            SwipedIcon.SetActive(false);
        }
        else
        {
            AR_Button.GetComponent<Animator>().Play("On");
            HoumeButton.GetComponent<Animator>().Play("On");
            SwipedIcon.SetActive(true);
        }
        _gamersRating = PlayerPrefs.GetInt("BestScore");
        GamersRatingPanel.text = Convert.ToString(_gamersRating);
        if (_gamersRating >= 100 && _gamersRating < 200)
        {
            ProgressBarAnimator.Play("From0to100");
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 200 && _gamersRating < 400)
        {
            ProgressBarAnimator.Play("From100to200");
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 400 && _gamersRating < 600)
        {
            ProgressBarAnimator.Play("From200to400");
            RunnerYellow.SetActive(true);
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
        }
        if (_gamersRating >= 600)
        {
            ProgressBarAnimator.Play("From400to600");
            RunnerYellow.SetActive(true);
            RunnerBlue.SetActive(true);
            RunnerGreen.SetActive(true);
            RunnerRed.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            SwipedIcon.SetActive(false);
    }

    public void Button_OK_InFirstStart()
    {
        HelloUI.GetComponent<Animator>().Play("Off");
        HelloDodo.GetComponent<Animator>().Play("Goodbyedodo");
        AR_Button.GetComponent<Animator>().Play("On");
        HoumeButton.GetComponent<Animator>().Play("On");
        SwipedIcon.SetActive(true);
    }

    public void Button_AR()
    {
        AR_Button.GetComponent<Animator>().Play("ToARButton");
        StartCoroutine(AnimUIFordododrom());
    }

    public void ReturnHome()
    {
        SceneManager.LoadSceneAsync(2);
    }

    IEnumerator AnimUIFordododrom()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(2);
    }
}
