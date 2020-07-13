using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip jump, crash, correct, incorrect, pressbutton, run;
    public AudioSource JUMP, CRASH, CORRECT, INCORRECT, PRESSBUTTON, RUN;
    public AudioClip button, correctanswer, incorrectanswer, crash2, formenu, forgame;
    public AudioSource BUTTON, CORRECTANSWER, INCORRECTANSWER, CRASH2, FORMENU, FORGAME;//TODO
    public void SoundOfCrash()
    {
        //CRASH.PlayOneShot(crash, 0.5F);
        CRASH2.PlayOneShot(crash2, 0.5F);
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
        //CORRECT.PlayOneShot(correct, 0.5F);
        CORRECT.PlayOneShot(correct, 0.5F);
    }
    public void SoundOfInCorrectAnswer()
    {
        //INCORRECT.PlayOneShot(incorrect, 0.5F);
        INCORRECT.PlayOneShot(incorrect, 0.5F);

    }
    public void SoundOfJump()
    {
        JUMP.PlayOneShot(jump, 0.5F);
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
