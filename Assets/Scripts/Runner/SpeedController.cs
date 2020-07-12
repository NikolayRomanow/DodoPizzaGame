using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{   private void OnEnable()
    {
        Run.Death += ZeroSpeed;
    }
    private void OnDisable()
    {
        Run.Death -= ZeroSpeed;
    }
    public void ZeroSpeed()
    {
        Statistic.Speed = 0f;
    }
   
    public void ImprovedSpeed()
    {
        Statistic.Speed = 9f;
    }
    public void StandartSpeed()
    {
        Statistic.Speed = 3f;
    }
    public void SetSpeed(float speed)
    {
        Statistic.Speed = speed;
    }

}
