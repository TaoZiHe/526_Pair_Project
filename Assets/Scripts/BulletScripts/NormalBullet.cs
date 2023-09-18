using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{     
    // Attach this script to bullets

    public float lifeCycle = 1.0f;
    public float atk = 1.0f;
    private void OnEnable()
    {
        Invoke( "RecycleObj", lifeCycle);
    }
    void RecycleObj()
    {
        ObjPoolManager.GetInstance().PushObj(this.gameObject.name,
            this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {   
        // if hitting anything, recycle the bullet immediately
        RecycleObj();
    }
}
