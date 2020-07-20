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
    GameObject FirstVopros;
    [HideInInspector]
    public QuizView QuizView;
    public static event Action SoundOfDeath;
    public static event Action Death;
    public static event Action NewVopros;
    public static event Action WinOrNot;
    public static event Action<float> CheckArrived;

    public GameObject Tile1;
    public GameObject Tile2;
    public GameObject Tile3;
    public GameObject Tile4;

    public UIController UIController;

    private Animator Animator;
    private CapsuleCollider Collider;
    public Transform Tile1Transform;
    public Transform Tile2Transform;
    public Transform Tile3Transform;
    public Transform Tile4Transform;

    private void NotTrueAnswer()
    {
        Statistic.Speed = 3f;
        SoundOfDeath();
        StartCoroutine(ColliderOff2());
        StartCoroutine(BadFinish());
        Animator.Play("Obstacle Failed");
    }

    void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
        Animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        FirstVopros = GameObject.FindGameObjectWithTag("FirstVopros");
    }
    public void Jump()
    {
        //rb.AddForce(new Vector3(0, 2, 0) * 180);
        StartCoroutine(ColliderOff());
        Animator.Play("Jump");
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Prov" && QuizView.IsCorrectAnswer == true)
        {
            CheckArrived(3);
            Jump();
            QuizView.SetColorGreen();
        }
        if (other.tag == "Prov" && QuizView.IsCorrectAnswer == false)
        {
            NotTrueAnswer();
            QuizView.SetColorRed();
        }
        if (other.tag == "Prov" && QuizView.IsCorrectAnswer == null)
        {
            UIController.VictorineZoneOff();
            UIController.TimerOff();
            NotTrueAnswer();
        }
        if (other.tag == "Prep")
        {
            //Death();
        }
        if (other.tag == "NewVopros")
        {
            NewVopros();
        }
        if (other.tag == "FirstVopros")
        {               
            NewVopros();
            FirstVopros.SetActive(false);
        }
        if (other.tag == "WinOrNot")
        {
            WinOrNot();
        }

    }
    private void Update()
    {
        //if(Vector3.Distance(gameObject.transform.position,))
    }

    IEnumerator ColliderOff()
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Collider.enabled = true;
    }

    IEnumerator ColliderOff2()
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(3.0f);
        Collider.enabled = true;
    }

    IEnumerator BadFinish()
    {
        yield return new WaitForSeconds(4.0f);
        Death();
        Tile1.transform.position = Tile1Transform.position;
        Tile2.transform.position = Tile2Transform.position;
        Tile3.transform.position = Tile3Transform.position;
        Tile4.transform.position = Tile4Transform.position;

    }
}
