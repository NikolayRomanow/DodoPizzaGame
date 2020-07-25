using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class RandomChislo : MonoBehaviour
{
    
    public int Rand;
    public int lenghtOfScore = 4;
    // Start is called before the first frame update
    
    
    
    public int Chislo()
    {
        return Rand;
    }    
    void Start()
    {
        StartCoroutine(randCifraa());
    }
    IEnumerator randCifraa()
    {
        yield return new WaitForSeconds(0.05f);
        switch (lenghtOfScore)
        {
            case 0:
                Rand = Random.Range(0, 9999);
                break;
            case 1:
                Rand = Random.Range(0, 9999);
                break;
            case 2:
                Rand = Random.Range(0, 9999);
                break;
            case 3:
                Rand = Random.Range(0, 9999);
                break;
            case 4:
                Rand = Random.Range(999, 10000);   
                break;
            case 5:
                Rand = Random.Range(9999, 100000);
                break;
            case 6:
                Rand = Random.Range(99999, 1000000);
                break;
            case 7:
                Rand = Random.Range(999999, 10000000);
                break;
            case 8:
                Rand = Random.Range(9999999, 100000000);
                break;
            case 9:
                Rand = Random.Range(99999999, 1000000000);
                break;            
        }
        StartCoroutine(randCifraa());    
        
        //for (int i = 0; i < Cifra.Length; i++)
        //{
            
        //}            
            
    }
}
