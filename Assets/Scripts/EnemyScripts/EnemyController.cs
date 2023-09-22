using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Color DefaultColor = new Color(1f, 1f, 1f);
    public Color ActivateColor = new Color(1f, 1f, 0f);
    public Color DeadColor = new Color(0.2f, 0.2f, 0.2f);
    public string EnemyName = "default enemy #1";
    public float health = 5.0f;
    public Transform patrolRoute;
    public List<Transform> locations;
    private NavMeshAgent agent;
    private Transform Player;

    //private Color CustomColor;
    private Renderer renderer;
    public bool isDead = false;
    private bool isPossessed = false;
    private int locationIndex;

    // TODO: how do enemies attack? Do elite and normal enemies need separate scripts?

    // Start is called before the first frame update
    void Start()
    {   
        renderer = this.GetComponent<Renderer>();
        //CustomColor = new Color(0.5f, 0.0f, 0.6f, 1f);
        //Get the default color set in this enemy's material
        DefaultColor = renderer.material.color;
        InitializePatrolRoute();

        agent = this.GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        //makes sure that this enemy is no longer protected(possessed). enemy will only receive damage from bullets when this EnemyController script is enabled AND isPossessed is false.
        DisableProtectSelf();
    }

    /// <summary>
    /// When a game object tagged 'player' enters the alert zone 
    /// </summary>
    /// <summary>
    /// When a game object tagged 'player' enters the alert zone 
    /// </summary>
    public void SetAlertOn()
    {
        // Change this enemy's color to alert color
        renderer.material.SetColor("_Color", ActivateColor);
        // enable the sprite renderer in the child game object to show the '!' on its head
        // only if this object is not a key enemy.
<<<<<<< Updated upstream
        if(!gameObject.CompareTag("KeyEnemy"))
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        
=======
        if (!gameObject.CompareTag("KeyEnemy"))
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

>>>>>>> Stashed changes
        Debug.Log("Enemy" + this.EnemyName + ": Player enters my warning zone");
    }

    /// <summary>
    /// When a game object tagged 'player' exits the alert zone 
    /// </summary>
    public void SetAlertOff()
    {
        renderer.material.SetColor("_Color", DefaultColor); // remove custom color for now
        // disable the sprite renderer in the child game object to show the '!' on its head
        // only if this object is not a key enemy.
<<<<<<< Updated upstream
        if(!gameObject.CompareTag("KeyEnemy"))
=======
        if (!gameObject.CompareTag("KeyEnemy"))
>>>>>>> Stashed changes
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Debug.Log("Enemy" + this.EnemyName + ": Player exits my warning zone");
    }

    private void OnCollisionEnter(Collision collision)
    {   
        // Check collision ONLY IF hit by a bullet and self is not protected (possessed) at the moment.
        if (collision.collider.CompareTag("Bullet") && !isPossessed)
        {
            // if hit by a normal bullet: decrease health
            NormalBullet nb = collision.gameObject.GetComponent<NormalBullet>();
            //Debug.Log("Enemy is hit by a bullet");
            if (nb != null)
                DecreaseHealth(nb);
            
            // protect self if hit by a possession bullet
            PossessionBullet pb = collision.gameObject.GetComponent<PossessionBullet>();
            if (pb != null)
                ProtectSelf();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            agent.destination = Player.position;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.destination = Player.position;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.destination = locations[locationIndex].position;
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
   
    /// <summary>
    /// Mark the game object as dead; triggers EnemyDies event; destroy self in 1 second. 
    /// </summary>
    private void Dies()
    {
        isDead = true;
        renderer.material.SetColor("_Color", DeadColor);
        EventCenter.GetInstance().TriggerEvent("EnemyDies", this);
        Destroy(this.gameObject, 1);
    }

    /// <summary>
    /// Triggered after self is hit by a possession bullet. Temporarily protect self from being killed by any other bullets until the player successfully possesses this enemy game object. At that time this EnemyController script will be disabled so there's no need to call DisableProtectSelf.  
    /// </summary>
    public void ProtectSelf()
    {
        isPossessed = true;
    }
    
    /// <summary>
    /// Set isPossessed to false. Make this enemy become vulnerable to bullets again. Should be called everytime when this EnemyController script is enabled.
    /// </summary>
    public void DisableProtectSelf()
    {
        isPossessed = false;
    }

    public void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    public void MoveToNextLocation()
    {
        if (locations.Count == 0)
        { return; }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    
    public void Update()
    {
        if(agent.remainingDistance < 0.3f && !agent.pathPending)
        {
            MoveToNextLocation();
        }
    }
}
