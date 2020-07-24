using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChislo : MonoBehaviour
{
    public int Rand;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SlotMachine()
    {
        Rand = Random.Range(11111, 99999);
    }
    public int Chislo()
    {
        return Rand;
    }

    // Update is called once per frame
    void Update()
    {
        SlotMachine();
    }
}
