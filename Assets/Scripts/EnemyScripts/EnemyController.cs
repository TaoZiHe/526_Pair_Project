using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Color ActivateColor;
    public Color DeadColor = new Color(0.2f, 0.2f, 0.2f);
    public string EnemyName = "default enemy #1";
    public float health = 5.0f;
    private Color CustomColor;
    private Renderer renderer;

    //private bool isAlertOn = false;
    
    // Start is called before the first frame update
    void Start()
    {   
        renderer = this.GetComponent<Renderer>();
        CustomColor = new Color(0.5f, 0.0f, 0.6f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// When a gameobject tagged 'player' enters the alert zone 
    /// </summary>
    public void SetAlertOn()
    {
        //this.isAlertOn = true;
        renderer.material.SetColor("_Color", ActivateColor);
        Debug.Log("Enemy" + this.EnemyName + ": Player enters my warning zone");
    }

    /// <summary>
    /// When a gameobject tagged 'player' exits the alert zone 
    /// </summary>
    public void SetAlertOff()
    {
        //this.isAlertOn = false;
        renderer.material.SetColor("_Color", CustomColor);
        Debug.Log("Enemy" + this.EnemyName + ": Player exits my warning zone");
    }

    private void OnCollisionEnter(Collision collision)
    {   
        // if hit by a bullet
        if (collision.collider.CompareTag("Bullet"))
        {
            // if hit by a normal bullet: decrease health
            NormalBullet nb = collision.gameObject.GetComponent<NormalBullet>();
            Debug.Log("Enemy is hit by a bullet");
            if (nb != null)
                DecreaseHealth(nb);
        }
    }

    /// <summary>
    /// Decreases this enemy's health if hit by a normal bullet. Also checks if it is killed by this bullet
    /// </summary>
    /// <param name="nb">NormalBullet component attached to a bullet</param>
    private void DecreaseHealth(NormalBullet nb)
    {
        float bulletDamage = nb.atk;
        health -= nb.atk;
        
        if (health <= 0)
            Dies();
    }
   
    private void Dies()
    {
        renderer.material.SetColor("_Color", DeadColor);
        EventCenter.GetInstance().TriggerEvent("EnemyDies", this);
        Destroy(this.gameObject, 1);
    }
}
