using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAlertZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform parent = this.transform.parent;
            EnemyController parentEnemyController = parent.GetComponent<EnemyController>();
            parentEnemyController.SetAlertOn();
        }
    }
    
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //         // Pass back the alert zone's parent (enemy itself)
    //         //EventCenter.GetInstance().TriggerEvent("PlayerStaysInAlertZone", this.transform.parent);
    //         Debug.Log("Player stays in zone");
    // }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform parent = this.transform.parent;
            EnemyController parentEnemyController = parent.GetComponent<EnemyController>();
            parentEnemyController.SetAlertOff();
        }
    }
}
