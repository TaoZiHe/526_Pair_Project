using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class PlayerControllerPossessed : MonoBehaviour
{
    // TODO: make these private and copy from enemy values 
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public float jumpSpeed = 5f;

    [SerializeField] private GameObject logTextContainer;
    [SerializeField]  private TextMeshProUGUI logText;
    
    //public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool bulletTrigger = false;
    
    private float verticalInput;
    private float horizontalInput;
    
    //private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        logTextContainer = UIManager.GetInstance().GetSkillLogDisplay();
        logText = logTextContainer.GetComponent<TMPro.TextMeshProUGUI>();
        logText.text = "Possession bullet disabled in this form!";
        
        // TODO: Does this possessed enemy need an event listener about enemies dying?
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
    }

    private void FixedUpdate()
    {
        if(bulletTrigger)
        {
            ShootNormalBullet();
        }
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
    
    private void OnKillingEnemy(object info)
    {   
        Debug.Log("Enemy possessed by the player killed Enemy " + (info as EnemyController).EnemyName);
    }
}
