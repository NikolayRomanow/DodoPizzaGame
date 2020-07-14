using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    //public AudioClip jump, crash, correct, incorrect, pressbutton, run;
    //public AudioSource JUMP, CRASH, CORRECT, INCORRECT, PRESSBUTTON, RUN;
    public AudioClip button, correctanswer, incorrectanswer, crash2, formenu, forgame;
    public AudioSource BUTTON, CORRECTANSWER, INCORRECTANSWER, CRASH2, FORMENU, FORGAME;//TODO
    public GameObject SoundOff, SoundOn;
    public float Volume = 0.5f;
    public void VolumeOn()
    {
        Volume = 0.5F;
        FORGAME.volume = 0.5f;
        FORMENU.volume = 0.5f;
        SoundOff.SetActive(false);
        SoundOn.SetActive(true);
    }
    public void VolumeOff()
    {
        Volume = 0F;
        FORGAME.volume = 0;
        FORMENU.volume = 0;
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
    }
    public void SoundOfCrash()
    {
        //CRASH.PlayOneShot(crash, 0.5F);
        CRASH2.PlayOneShot(crash2, Volume);
    }
    public void SoundOfRun()
    {

    }
    public void SoundOfPressedButton()
    {
        //PRESSBUTTON.PlayOneShot(pressbutton, 0.5F);
        //BUTTON.PlayOneShot(button, 0.5F);
    }
    public void SoundOfCorrectAnswer()
    {
        
        //CORRECT.PlayOneShot(correct, Volume);
    }
    public void SoundOfInCorrectAnswer()
    {
        
        //INCORRECT.PlayOneShot(incorrect, Volume);

    }
    public void SoundOfJump()
    {
        //JUMP.PlayOneShot(jump, Volume);
    }
    public void SoundInMenuOn()
    {
        FORMENU.Play();
    }
    public void SoundInMenuOff()
    {
        FORMENU.Stop();
    }
    public void SoundInGameOn()
    {
        FORGAME.Play();
    }
    public void SoundInGameOff()
    {
        FORGAME.Stop();
    }
}
