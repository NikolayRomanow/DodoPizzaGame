using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDvij : MonoBehaviour
{
    public void MovePlatform()
    {
        if (Statistic.isGameStart == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z - 1), Time.deltaTime * Statistic.Speed);
        }
    }



    void Update()
    {
        MovePlatform();        
    }
}
