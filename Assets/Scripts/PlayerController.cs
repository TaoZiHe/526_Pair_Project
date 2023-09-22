using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public float jumpSpeed = 5f;

    public GameObject logTextContainer;
    [SerializeField]  
    private TextMeshProUGUI logText;

    public float PossessionSkillCooldown = 5.0f;
    private float lastSkillUseTime = -5.0f;

    //public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool bulletTrigger = false;
    
    private float verticalInput;
    private float horizontalInput;
    
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        logText = logTextContainer.GetComponent<TMPro.TextMeshProUGUI>();
        
        // add event listener
        EventCenter.GetInstance().AddEventListener("EnemyDies", OnKillingEnemy);
        // Use event center to make sure a possession manager is initialized. Then pass an initial player reference (self) to the possession manager
        EventCenter.GetInstance().AddEventListener("PossessionManagerInitialized", RegisterInitialPlayerControllable);

    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;

        this.transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);
        
        // Shooting a normal bullet
        if(Input.GetMouseButtonDown(0))
        {
            bulletTrigger = true;
        }
        
        // Shooting a possession bullet
        if(Input.GetMouseButtonDown(1))
        {
            // Check if the skill is off cooldown
            if (Time.time - lastSkillUseTime >= PossessionSkillCooldown)
            {
                ShootPossessionBullet();
                logText.text = "Possession bullet is not ready yet.";
            }
                
            else
            {
                // Notify the player that the skill is still on cooldown
                Debug.Log("Possession bullet is not ready");
                logText.text = "Possession bullet is not ready yet.";
            }
        }

        if (Time.time - lastSkillUseTime >= PossessionSkillCooldown)
        {
            logText.text = "Possession bullet is ready.";
        }
        
    }
    
    private void FixedUpdate()
    {
        if(bulletTrigger)
        {
            // GameObject newBullet = Instantiate(bullet, this.transform.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            // Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            // bulletRb.velocity = this.transform.forward * bulletSpeed;
            // bulletTrigger = false;
            
            ShootNormalBullet();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Nullify the forces caused by collision
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void ShootNormalBullet()
    {
        // Manually change this path for now. 
        GameObject bullet = ObjPoolManager.GetInstance().GetObj("Prefabs/Bullet"); 
        bullet.transform.position = this.transform.position + new Vector3(1, 0, 0);
        bullet.transform.rotation = this.transform.rotation;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = this.transform.forward * bulletSpeed;
        bulletTrigger = false;
    }


    private void ShootPossessionBullet()
    {
        // Manually change this path for now. 
        GameObject bullet = ObjPoolManager.GetInstance().GetObj("Prefabs/PossessionBullet");
        PossessionBullet pb = bullet.GetComponent<PossessionBullet>();
        pb.SetShooter(this.gameObject);
        
        bullet.transform.position = this.transform.position + new Vector3(1, 0, 0);
        bullet.transform.rotation = this.transform.rotation;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = this.transform.forward * bulletSpeed;
        
        lastSkillUseTime = Time.time;
    }
    
    private void OnKillingEnemy(object info)
    {   
        Debug.Log("Player killed Enemy " + (info as EnemyController).EnemyName);
    }


    private void RegisterInitialPlayerControllable(object info)
    {
        PossessionManager pm = info as PossessionManager;
        if (pm != null)
            pm.RegisterCurrentPlayerControllable(this.gameObject);
        else
            Debug.Log("Player Controler: Failed to register new player controller to Possession Manager.");
    }
}
