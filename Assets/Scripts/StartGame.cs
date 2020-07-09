using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Animator Animator;
    public GameObject CanvasPlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartGamee()
    {
       // Application.LoadLevel(0);
        Statistic.Score = 0;
        Statistic.Jiv = true;
        Statistic.Speed = 3f;
        Statistic.BOOL = true;
        Animator.SetTrigger("Menu");
        CanvasPlay.SetActive(false);


    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
