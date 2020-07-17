using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        animator.Play("Jump");
    }
}
