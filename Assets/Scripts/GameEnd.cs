using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    public GameObject youWin;
    public GameObject gameOver;

    private void Start()
    {
        youWin.SetActive(false);
        gameOver.SetActive(false);
    }

    private void OnEnable()
    {
        youWin.SetActive(false);
        gameOver.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
            youWin.SetActive(true);
    }
}
