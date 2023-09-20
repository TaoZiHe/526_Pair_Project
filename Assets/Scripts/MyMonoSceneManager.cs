using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyMonoSceneManager : MonoBehaviour
{
    
    private void OnEnable()
    {   
        ReSearchUIElements();
        // Disable the credits when the game is started.
        UIManager.GetInstance().ToggleCredits();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();

        else if (Input.GetKeyDown(KeyCode.C))
            UIManager.GetInstance().ToggleCredits();
        
        else if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }
    
    private void RestartGame()
    {
        // Remove all events from dictionary
        EventCenter.GetInstance().ClearEvents();
        Debug.Log("Restarting Scene...\n");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ReSearchUIElements();
    }
    
    public void ReSearchUIElements()
    {
        GameObject skillLogDisplay = GameObject.Find("PlayerSkillLogContainer");
        GameObject credits = GameObject.Find("CreditsContainer");
        UIManager.GetInstance().SetSkillLogDisplay(skillLogDisplay);
        UIManager.GetInstance().SetCredits(credits);
    }
    
}
