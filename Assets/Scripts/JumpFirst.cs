using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodoDataModel;

public class JumpFirst : MonoBehaviour
{
    public Transform[] WayPoints;

    //public float SpeedMove = 0.5f;
    // public float SpeedRotate = 15f;

    private int _count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prep")
        {

        }
    }

    private void Update()
    {   
        
        if(_count==2||_count==3|| _count == 8 || _count == 9|| _count == 14 || _count == 15|| _count == 21 || _count == 22 || _count == 25 || _count == 26 || _count == 31 || _count == 32||_count==37||_count==38)
        {   
            Stats.MovementVelocityFirstBird = 3f;
        }
        else
        {
            Stats.MovementVelocityFirstBird = 0.45f;
        }
        if (Stats.isReady == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, WayPoints[_count].position, Time.deltaTime * (Stats.MovementVelocityFirstBird + 0.5f));
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, WayPoints[_count].rotation, Time.deltaTime * Stats.RotateVelocityFirstBird + 0.5f);
            if (this.transform.position == WayPoints[_count].position /*&& this.transform.rotation == WayPoints[_count].rotation*/)
                _count++;
        }
    }
}
