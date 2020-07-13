using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBirdController : MonoBehaviour
{
    public Animator DodoBird;

    public void PeaceOn()
    {
        DodoBird.SetTrigger("PeaceOn");
    }
    public void PeaceOff()
    {
        DodoBird.SetTrigger("PeaceOff");
    }
    public void DriveOn()
    {
        DodoBird.SetTrigger("DriveOn");
    }
    public void DriveOff()
    {
        DodoBird.SetTrigger("DriveOff");
    }
    public void JumpOn()
    {
        DodoBird.SetTrigger("JumpOn");
    }
    public void JumpOff()
    {
        DodoBird.SetTrigger("JumpOff");
    }
    public void CrashOn()
    {
        DodoBird.SetTrigger("CrashOn");
    }
    public void CrashOff()
    {
        DodoBird.SetTrigger("CrashOff");
    }
    public void TurnOn()
    {
        DodoBird.SetTrigger("TurnOn");
    }
    public void TurnOff()
    {
        DodoBird.SetTrigger("TurnOff");
    }
}