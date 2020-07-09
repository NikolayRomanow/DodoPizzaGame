using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public Animator Animator;
    public AudioClip jump;
    public AudioSource JUMP;
    public Vector3 Vector3, Jump;
    Rigidbody rb;    
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
        if (Statistic.BOOL == true)
        {
            if (other.tag == "Prov" && Statistic.VoprosOtvet == true)
            {
                rb.AddForce(Jump * 180);
                Statistic.Speed = 3f;
                JUMP.PlayOneShot(jump, 0.5F);
            }
            if (other.tag == "Prep")
            {
                //gameObject.SetActive(false);
                Animator.SetTrigger("Start");
                Statistic.Speed = 0;
                Statistic.Jiv = false;
            }
            if (other.tag == "NewVopros")
            {
                Statistic.OK = false;
                Statistic.VoprosOtvet = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {   
        //Vector3.z = gameObject.transform.position.z - 1;
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, Vector3, Time.deltaTime*2);
    }
}
