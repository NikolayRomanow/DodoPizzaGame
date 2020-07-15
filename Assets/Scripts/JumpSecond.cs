using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DodoDataModel;

public class JumpSecond : MonoBehaviour
{
    public Transform[] WayPoints;
    Rigidbody rb;

    //public float SpeedMove = 0.5f;
    // public float SpeedRotate = 15f;

    private int _count = 0;
    void Start()
            {
                rb = gameObject.GetComponent<Rigidbody>();
            }
    public void Jump()
            {
                rb.AddForce(new Vector3(0, 2, 0) * 180);
            }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Jump();          
        }

        if (_count == 2 || _count == 3 || _count == 5 || _count == 6 || _count == 9 || _count == 10 || _count == 14 || _count == 15 || _count == 17 || _count == 18 || _count == 21 || _count == 22 || _count == 26 || _count == 27)
        {
            Stats.MovementVelocitySecondBird = 3f;
        }
        else
        {
            Stats.MovementVelocitySecondBird = 0.45f;
        }
        if (Stats.isReady == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, WayPoints[_count].position, Time.deltaTime * (Stats.MovementVelocitySecondBird+0.26f));
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, WayPoints[_count].rotation, Time.deltaTime * Stats.RotateVelocitySecondBird + 0.5f);
            if (this.transform.position == WayPoints[_count].position /*&& this.transform.rotation == WayPoints[_count].rotation*/)
                _count++;
        }
    }
}
