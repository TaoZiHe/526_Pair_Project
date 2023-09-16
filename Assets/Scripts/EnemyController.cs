using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Color ActivateColor;

    private Color CustomColor;
    private Renderer render;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            render.material.SetColor("_Color", ActivateColor);
            Debug.Log("Player enter warning");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player stay in zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            render.material.SetColor("_Color", CustomColor);
            Debug.Log("Player exit zone");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        render = this.GetComponent<Renderer>();
        CustomColor = new Color(0.5f, 0.0f, 0.6f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
