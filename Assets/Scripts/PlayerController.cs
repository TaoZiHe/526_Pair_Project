using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public float jumpSpeed = 5f;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool bulletTrigger = false;


    private float verticalInput;
    private float horizontalInput;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;

        this.transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            bulletTrigger = true;
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

            GameObject bullet = ObjPoolManager.GetInstance().GetObj("Prefabs/Bullet");
            bullet.transform.position = this.transform.position + new Vector3(1, 0, 0);
            bullet.transform.rotation = this.transform.rotation;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * bulletSpeed;
            bulletTrigger = false;
        }
    }
}
