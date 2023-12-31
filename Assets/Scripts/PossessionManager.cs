using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{

    public float PossessionSkillLastTime = 10.0f;
    public GameObject originalPlayer = null;
    [SerializeField] private GameObject currentPlayerControllable = null;
    // TODO: make it private?
   
    
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("PossessionSequence", InitiatePossession);
        // let the initial player's PlayerController register itself
        EventCenter.GetInstance().TriggerEvent("PossessionManagerInitialized", this);
    }


    private void InitiatePossession(object info)
    {
        List<GameObject> objList = info as List<GameObject>;
        // if shooter and the possessed are all valid game objects
        if (objList != null && objList.Count == 2)
        {
            Debug.Log(objList[0].gameObject.name + " shoots a possession bullet onto "+ objList[1].gameObject.name);
            
            // deactivate the original player and attach a player controller to the possessed
            currentPlayerControllable.SetActive(false);
            
            // get the possessed enemy (temporary player)
            GameObject tempPlayer = objList[1];
            
            // disable the enemy controller on it 
            tempPlayer.GetComponent<EnemyController>().enabled = false;
            
            // attach a possessed player controller script
            tempPlayer.AddComponent<PlayerControllerPossessed>();
            RegisterCurrentPlayerControllable(tempPlayer.gameObject);
            
            Debug.Log("Possession started. Current time: "+ Time.time.ToString());
            // Start the timer of the possession skill
            StartCoroutine(CO_DelayedReturnToOriginalPlayer());
        }
    }

    /// <summary>
    /// Set the currentPlayerControllable field in the PossessionManager instance 
    /// </summary>
    /// <param name="player">Current controllable 'player' object. Could be a possessed enemy. </param>
    public void RegisterCurrentPlayerControllable(GameObject player)
    {
        this.currentPlayerControllable = player;
        Debug.Log("Possession Manager: Current player registered. Player game object name: " + player.gameObject.name);
    }
    
    /// <summary>
    ///  Coroutine that makes sure that the possession lasts for an exact period of time. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CO_DelayedReturnToOriginalPlayer()
    {
        // TODO: Pass the shooter and the possessed into ReturnToOriginalPlayer?  
        yield return new WaitForSeconds(PossessionSkillLastTime);  // Wait for PossessionSkillLastTime seconds
        ReturnToOriginalPlayer();  // Call method ReturnToOriginalPlayer after the delay
    }

    private void ReturnToOriginalPlayer()
    {
        Debug.Log("ReturnToOriginalPlayer() started. Current time: "+ Time.time.ToString());
        
        // reactivate the player
        originalPlayer.SetActive(true);
        
        // destroy the possession controller on the enemy
        PlayerControllerPossessed pcp = currentPlayerControllable.GetComponent<PlayerControllerPossessed>();
        if (pcp != null)
            Destroy(pcp);
        
        // reactivate the enemy script
        EnemyController ec = currentPlayerControllable.GetComponent<EnemyController>();
        if (ec != null)
            ec.enabled = true;
        else
            throw new Exception("Error: original enemy controller not found on possessed enemy.");
        
        EventCenter.GetInstance().TriggerEvent("PossessionEnded", this);
        
        this.currentPlayerControllable = originalPlayer;
    }
}
