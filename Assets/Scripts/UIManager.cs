using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private GameObject skillLogDisplay = GameObject.Find("PlayerSkillLogContainer");
    private GameObject credits = GameObject.Find("CreditsContainer");
    private GameObject openDoorPrompt = GameObject.Find("OpenDoorPrompt");
    private GameObject doorLockedPrompt = GameObject.Find("DoorIsLockedPrompt");
    private GameObject gameOver = GameObject.Find("GameOverText");
    
    /// <summary>
    /// Allows other classes (original player or possessed enemies) to change the skill log display object. 
    /// </summary>
    /// <param name="obj"></param>
    public void SetSkillLogDisplay(GameObject obj)
    {
        skillLogDisplay = obj;
    }

    /// <summary>
    /// Get the skill log display object from other classes (original player or possessed enemies), so that they can modify the text on screen. 
    /// </summary>
    public GameObject GetSkillLogDisplay()
    {
        return skillLogDisplay;
    }
    
    /// <summary>
    /// Press C to toggle the credits display. Credits is enabled by default (so that it is searchable) and will be disabled when MyMonoSceneManager starts.
    /// </summary>
    
    public void ToggleCredits()
    {
        if (credits.activeSelf)
            credits.SetActive(false);
        else
            credits.SetActive(true);
    }
    
    
    // TODO: Should we keep the searching of UI elements inside UIManager? Currently it is controlled by MyMonoSceneManager.
    public void SetCredits(GameObject obj)
    {
        credits = obj;
    }

    public void ShowDoorPrompt()
    {
        openDoorPrompt.SetActive(true);
    }
    
    public void HideDoorPrompt()
    {
        openDoorPrompt.SetActive(false);
    }

    public void SetDoorPrompt(GameObject obj)
    {
        openDoorPrompt = obj;
    }

    public void ShowDoorLockedPrompt()
    {
        doorLockedPrompt.SetActive(true);
    }
    
    public void HideDoorLockedPrompt()
    {
        doorLockedPrompt.SetActive(false);
    }
    
    public void SetDoorLockedPrompt(GameObject obj)
    {
        doorLockedPrompt = obj;
    }
    
    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
    
    public void HideGameOver()
    {
        gameOver.SetActive(false);
    }

    public void SetGameOver(GameObject obj)
    {
        gameOver = obj;
        Debug.Log("Gameover is set.");
    }
}
