using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using SupremumStudio;

public class Run : MonoBehaviour
{   
    public Animator Animator,ScoreZone;
    public AudioClip jump, crash;
    public AudioSource JUMP, CRASH;
    public Vector3 Vector3, Jump;
    Rigidbody rb;
    [HideInInspector]
    public QuizView QuizView;
   


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3.x = 1;
        Vector3.y = 1;
        Jump.x = 0;
        Jump.y = 2;
        Jump.z = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.tag == "Prov" && QuizView.IsCorrectAnswer == true)
            {
                rb.AddForce(Jump * 180);
                Statistic.Speed = 3f;
                JUMP.PlayOneShot(jump, 0.5F);
            }
            if (other.tag == "Prov" && QuizView.IsCorrectAnswer == false)
            {
                SoundOfDeath();
                CRASH.PlayOneShot(crash, 0.5F);
            }
            if (other.tag == "Prep")
            {
                Death();
                Animator.SetTrigger("Start");
                Statistic.Speed = 0;
                ScoreZone.SetTrigger("on");

                //Statistic.Jiv = false;
                //CRASH.PlayOneShot(crash, 0.5F);
            }
            if (other.tag == "NewVopros")
            {
                NewVopros();                
            }
        
    }
    public event Action JumpBird;
    public event Action SoundOfDeath;
    public event Action Death;
    public event Action NewVopros;
    // Update is called once per frame
    void Update()
    {   
        //Vector3.z = gameObject.transform.position.z - 1;
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, Vector3, Time.deltaTime*2);
    }
}
