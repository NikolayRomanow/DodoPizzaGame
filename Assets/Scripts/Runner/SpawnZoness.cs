using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoness : MonoBehaviour
{

    public GameObject GameZone, Point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Instantiate(GameZone, Point.transform.position, Quaternion.identity);
        }
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

}
