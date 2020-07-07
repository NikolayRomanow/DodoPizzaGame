using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoness : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameZone, Point;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Instantiate(GameZone, Point.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
