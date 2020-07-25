using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class RandomChislo : MonoBehaviour
{
    public Text[] Cifra;
    public int Rand;   
    // Start is called before the first frame update
    
    public void RandomCifra()
    {
        for (int i = 0; i < Cifra.Length; i++) 
        {
            var randChislo = Random.Range(0, 10);
            Cifra[i].text = randChislo.ToString();
        }
    }
    public void SlotMachine()
    {
        Rand = Random.Range(99999, 1000000);
    }
    public int Chislo()
    {
        return Rand;
    }

    // Update is called once per frame
    
    void Update()
    {
        //SlotMachine();
        
    }
    private void FixedUpdate()
    {
        //SlotMachine();
        //RandomCifra();
        //randCifraa();
    }
    void Start()
    {
        StartCoroutine(randCifraa());
    }
    IEnumerator randCifraa()
    {
        yield return new WaitForSeconds(0.05f);
        Rand = Random.Range(99999, 1000000);
        //for (int i = 0; i < Cifra.Length; i++)
        //{
            
        //}            
        StartCoroutine(randCifraa());        
    }
}
