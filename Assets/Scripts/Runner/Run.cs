using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using SupremumStudio;

public class Run : MonoBehaviour
{
    Rigidbody rb;
    [HideInInspector]
    public QuizView QuizView;
    public static event Action SoundOfDeath;
    public static event Action Death;
    public static event Action NewVopros;
    public static event Action<float> CheckArrived;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void Jump()
    {
        rb.AddForce(new Vector3(0, 2, 0) * 180);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Prov" && QuizView.IsCorrectAnswer == true)
        {
            CheckArrived(3);
            Jump();
        }
        if (other.tag == "Prov" && QuizView.IsCorrectAnswer == false)
        {
            SoundOfDeath();
        }
        if (other.tag == "Prep")
        {
            Death();
        }
        if (other.tag == "NewVopros")
        {
            NewVopros();
        }
    }
    private void Update()
    {
        //if(Vector3.Distance(gameObject.transform.position,))
    }


}
