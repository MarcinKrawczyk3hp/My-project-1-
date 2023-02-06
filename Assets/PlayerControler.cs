using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float acceleration = 10;
    public GameObject bulletPrefab;
    private Rigidbody rb;
    private Vector2 controlls;

    private Transform gun;
    private bool fireButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = transform.Find("gun");
        
    }

    // Update is called once per frame
    void Update()
    {
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        
        //if(v !=0 && h !=0)
           controlls = new Vector2(h, v);

        //check and reposition ship if off screen (x<-42 y<-34)
        if (Mathf.Abs(transform.position.x) > 39)
        {
            Vector3 newPosition = new Vector3(transform.position.x * -1,
                0,
                transform.position.z);
            transform.position = newPosition;
        }
        if (Mathf.Abs(transform.position.z) > 32)
                {
                    Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z * -1);

                   transform.position = newPosition;
                }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fireButtonDown = true;
        }
            }


    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * controlls.y* acceleration, ForceMode.Acceleration);
        rb.AddTorque(transform.up * controlls.x * acceleration, ForceMode.Acceleration);
        
        if (fireButtonDown)
        {
            GameObject bullet1 = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
            bullet1.transform.parent = null;
            bullet1.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.VelocityChange);
            
            Destroy(bullet1, 5);
            fireButtonDown= false;
        }
        }
        
}

