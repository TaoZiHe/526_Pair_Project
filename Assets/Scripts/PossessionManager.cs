using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
  
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("PossessionSequence", InitiatePossession);
    }


    private void InitiatePossession(object info)
    {
        List<GameObject> objList = info as List<GameObject>;
        
        if (objList != null && objList.Count == 2)
        {
            Debug.Log(objList[0].gameObject.name + " shoots a possession bullet onto "+ objList[1].gameObject.name);
        }
    }
}
