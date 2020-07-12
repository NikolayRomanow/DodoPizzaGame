using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip jump, crash, correct, incorrect, pressbutton, run;
    public AudioSource JUMP, CRASH, CORRECT, INCORRECT, PRESSBUTTON, RUN;
    public void SoundOfCrash()
    {
        CRASH.PlayOneShot(crash, 0.5F);
    }
    public void SoundOfRun()
    {
        
    }
    public void SoundOfPressedButton()
    {
        PRESSBUTTON.PlayOneShot(pressbutton, 0.5F);
    }
    public void SoundOfCorrectAnswer()
    {
        CORRECT.PlayOneShot(correct, 0.5F);
    }
    public void SoundOfInCorrectAnswer()
    {
        INCORRECT.PlayOneShot(incorrect, 0.5F);
    }
    public void SoundOfJump()
    {
        JUMP.PlayOneShot(jump, 0.5F);
    }
}
