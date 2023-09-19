using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private GameObject SkillLogDisplay = GameObject.Find("PlayerSkillLogContainer");

    public void SetSkillLogDisplay(GameObject obj)
    {
        SkillLogDisplay = obj;
    }

    public GameObject GetSkillLogDisplay()
    {
        return SkillLogDisplay;
    }
}
