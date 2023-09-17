using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedRecycle : MonoBehaviour
{     
    // Attach this script to bullets

    public float lifeCycle = 1.0f;
    private void OnEnable()
    {
        Invoke( "RecycleObj", lifeCycle);
    }
    void RecycleObj()
    {
        ObjPoolManager.GetInstance().PushObj(this.gameObject.name,
            this.gameObject);
    }
}
