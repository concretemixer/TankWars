using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    [SerializeField]
    GameObject turret;

    [SerializeField]
    GameObject hull;

    [SerializeField]
    GameObject barrel;

    [SerializeField]
    Projectile projectile;

    Vector3 target;
    Plane ground;

    float barrelAngle = 45;

    // Use this for initialization
    void Start () {
        ground = new Plane(Vector3.up, turret.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;

        float rayDistance;
        if (ground.Raycast(ray, out rayDistance))
        {
            Vector3 touchGround = ray.origin + ray.direction * rayDistance;
            target = touchGround;

           
        }
      /*
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 touchGround = ray.origin + ray.direction * hit.distance;
            target.transform.position = touchGround;
        }*/
    }


    void UpdateTurret()
    {

        Vector3 localTarget = (target - turret.transform.position);
        Vector3 realTarget = new Vector3(0,0,localTarget.magnitude);       

        
        Quaternion t = Quaternion.LookRotation(localTarget);

        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, t, 100 * Time.fixedDeltaTime);


        realTarget = turret.transform.TransformVector(realTarget)+turret.transform.position;

        //  foreach (var w in GetComponentsInChildren<Weapon>())
        //   {
        //      w.Target = realTarget;
        //  }

        //        Vector3 pp = transform.position + transform.rotation * (Vector3.forward * (trg.transform.position - transform.position).magnitude);
        //      pp.y = trg.transform.position.y;
        //    target = pp;

        barrel.transform.localRotation = Quaternion.Euler(90-barrelAngle, 0, 0);
    }

    void FixedUpdate()
    {
        fireCooldown -= Time.fixedDeltaTime;

        UpdateTurret();

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
        if (Input.GetMouseButton(1))
        {
            // foreach (var w in GetComponentsInChildren<Weapon>())
            //   w.Fire(false);
        }

        if (Input.GetKey(KeyCode.KeypadPlus)) {
            barrelAngle += Time.fixedDeltaTime * 15;            
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            barrelAngle -= Time.fixedDeltaTime * 15;
        }
        barrelAngle = Mathf.Clamp(barrelAngle, -10, 80);

        float v = GetComponent<Rigidbody>().velocity.magnitude;

        if (Input.GetKey(KeyCode.W) && v < 5)
        {
            hull.GetComponent<Rigidbody>().drag = .1f;
            hull.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 30);
        }
        else if (Input.GetKey(KeyCode.S) && v < 3)
        {
            hull.GetComponent<Rigidbody>().drag = .1f;
            hull.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -20);
        }
        else
            hull.GetComponent<Rigidbody>().drag = 5;


        if (Input.GetKey(KeyCode.A))
        {
            hull.GetComponent<Rigidbody>().angularDrag = 1;
            hull.GetComponent<Rigidbody>().drag = 5;
            hull.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * -2.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hull.GetComponent<Rigidbody>().angularDrag = 1;
            hull.GetComponent<Rigidbody>().drag = 5;
            hull.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * 2.0f);
        }
        else
            hull.GetComponent<Rigidbody>().angularDrag = 5;

    }


    float fireCooldown = 0;
    void Fire()
    {
        if (fireCooldown <= 0)
        {
            Vector3 pos = GetComponentInChildren<Muzzle>().transform.position;
            Vector3 direction = GetComponentInChildren<Muzzle>().transform.up;

            Projectile bullet1 = GameObject.Instantiate<Projectile>(projectile, pos, Quaternion.identity);
            bullet1.GetComponent<Rigidbody>().AddForce(direction * 1500);

            fireCooldown = 5;
        }
    }
}
