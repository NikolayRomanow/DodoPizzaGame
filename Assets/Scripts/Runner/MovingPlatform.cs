using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject Point;

    void Update()
    {
        if(gameObject.transform.position.z<-20)
        {
            gameObject.transform.position = Point.transform.position;
        }
    }
}
