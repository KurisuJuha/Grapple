using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject pointobj;
    public SpringJoint springJoint;
    
    
    public static bool grapple;

    public LineRenderer line;

    public float p1;

    public float p2;

    public float pw;

    public float t;

    public float bTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = bTime;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += new Vector3(0, 100, 0);
        }
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));// Rayを生成;
        //springJoint.connectedAnchor = transform.position;
        if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            line.positionCount = 2;
            grapple = true;
            pointobj.transform.position = hit.point;
            pointobj.transform.parent = hit.transform;
            float distance = Vector3.Distance(pointobj.transform.position, transform.position);
            springJoint.maxDistance = distance * p1;
            springJoint.minDistance = distance * p2;
        }

        if (Input.GetMouseButtonUp(0))
        {
            grapple = false;
        }

        if (Input.GetMouseButton(0) && grapple)
        {
            if (Physics.Linecast(transform.position, pointobj.transform.position, out var hit2))  // もしRayを投射して何らかのコライダーに衝突したら
            {
                pointobj.transform.position = hit2.point;
                pointobj.transform.parent = hit2.transform;
            }
            
            pointobj.SetActive(true);
            line.gameObject.SetActive(true);
            
            float l =  Vector3.Distance(pointobj.transform.position, transform.position);
            if (springJoint.maxDistance > l)
            {
                springJoint.maxDistance = l;
            }

            line.SetPosition(0, pointobj.transform.position);
            line.SetPosition(1, transform.position);
            
            Debug.Log(springJoint.maxDistance < l);
        }
        else
        {
            pointobj.SetActive(false);
            line.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;
        GetComponent<Rigidbody>().AddForce(velocity * pw);
    }
}
