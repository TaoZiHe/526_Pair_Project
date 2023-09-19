using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //set up camera offset
    public Vector3 cameraOffset = new Vector3(0f, 1.2f, -2.6f);
    private Transform originalTarget;
    private Transform target;
    // Start is called before the first frame update
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("PossessionSequence", ChangeFocus);
        EventCenter.GetInstance().AddEventListener("PossessionEnded", ReturnToOriginalTarget);
        originalTarget = GameObject.Find("Player").transform;
        target = originalTarget;
    }

    // Late update for camera to keep track of player after it moves.
    private void LateUpdate()
    {
        this.transform.position = target.TransformPoint(cameraOffset);
        this.transform.LookAt(target);
    }
    
    /// <summary>
    /// Fly to the new target and follow that target. 
    /// </summary>
    /// <param name="info"> List of GameObject possessionPair. [0] is the shooter, [1] is the possessed.</param>
    private void ChangeFocus(object info)
    {
        List<GameObject> possessionPair = info as List<GameObject>;

        if (possessionPair != null)
        {
            target = possessionPair[1].transform;
        }

        else
        {
            Debug.Log("Camera Controller: ChangeFocus(): Failed to switch camera target because the possession pair is null. ");
        }
        
    }
    
    private void ReturnToOriginalTarget(object info)
    {
        target = originalTarget;
    }
}
