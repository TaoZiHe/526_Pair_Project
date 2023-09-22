using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorPromptZone : MonoBehaviour
{
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("PossessionEnded", OnPossessionEnd);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Show prompt if any enemy / player enters 
        Debug.Log("Something enters door area.");
        if ( !other.CompareTag("Bullet") )
        {
            UIManager.GetInstance().ShowDoorPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide prompt if any enemy / player leaves 
        Debug.Log("Something exits door area.");
        if ( !other.CompareTag("Bullet") )
        {
            UIManager.GetInstance().HideDoorPrompt();
        }
    }

    private void OnPossessionEnd(object info)
    {
        UIManager.GetInstance().HideDoorPrompt();
    }
}
