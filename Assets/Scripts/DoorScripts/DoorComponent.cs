using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class DoorComponent : MonoBehaviour
{
    private bool isUnlocked  = false;
    // Allow access if "KeyEnemy" presses E.
    [SerializeField] private GameObject debugShooter, debugPossessed;
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("PossessionSequence", CheckNewPlayerIdentity);
        EventCenter.GetInstance().AddEventListener("PossessionEnded", LockSelf);
    }

    private void Update()
    {
        // check if a game object with key is near AND if player wants to unlock door.
        if (isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void CheckNewPlayerIdentity(object info)
    {
        List<GameObject> myList = info as List<GameObject>;
        if (myList != null)
        {
            debugPossessed = myList[1];
            debugShooter = myList[0];
            
            // unlock self if player is possessing a keyEnemy. 
            if ( myList[1].CompareTag("KeyEnemy") )
                UnlockSelf();
        }
    }
    
    
    public void UnlockSelf()
    {
        isUnlocked = true;
    }
    
    public void LockSelf()
    {
        isUnlocked = false;
    }
    
    public void LockSelf(object info)
    {
        isUnlocked = false;
    }
}
