using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDvij : MonoBehaviour
{
    public Vector3 Vector3;
    // Start is called before the first frame update
    void Start()
    {
        Vector3.x = 0;
        Vector3.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.z = gameObject.transform.position.z + 1;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, Vector3, Time.deltaTime * Statistic.Speed);
        Destroy(gameObject, 20f);
    }
}
