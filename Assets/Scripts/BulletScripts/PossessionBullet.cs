using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionBullet : MonoBehaviour
{     
    // Attach this script to bullets

    public float lifeCycle = 5.0f;
    public float atk = 0.0f;
    
    // assign this field when a possession bullet obj is initialized.
    [SerializeField] private GameObject shooter;
    [SerializeField] private List<GameObject> possessionPair;
    
    private void OnEnable()
    {
        Invoke( "RecycleObj", lifeCycle);
    }
    void RecycleObj()
    {
        shooter = null;
        possessionPair.Clear();
        
        ObjPoolManager.GetInstance().PushObj(this.gameObject.name,
            this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("NormalEnemy"))
        {
            possessionPair.Add(shooter);
            possessionPair.Add(other.gameObject);
            EventCenter.GetInstance().TriggerEvent("PossessionSequence", possessionPair );
        }
        // if hitting anything, recycle the bullet immediately
        RecycleObj();
    }

    public void SetShooter(GameObject obj)
    {
        this.shooter = obj;
    }
}
