using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    public GameObject Instruct, StartP, LoadScreen;
    // Start is called before the first frame update
    public void InstructPanel()
    {
        StartP.SetActive(false);
        Instruct.SetActive(true);
    }
    public void Scene2()
    {
        Instruct.SetActive(false);
        LoadScreen.SetActive(true);
        Application.LoadLevel(1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
