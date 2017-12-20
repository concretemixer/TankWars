using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    ParticleSystem explosionLand;

    [SerializeField]
    ParticleSystem explosionWater;
    // Use this for initialization
    float Cx;

    void Start () {
        Cx = 0.005f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void FixedUpdate()
    {
      
        Vector3 v = -GetComponent<Rigidbody>().velocity;
        float vSq = v.sqrMagnitude * Cx;
        v.Normalize();
        GetComponent<Rigidbody>().AddForce(v * vSq, ForceMode.Force);
        //GetComponent<Rigidbody>().AddForce(Vector3.zero);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            explosionLand.transform.parent = transform.parent;
            explosionLand.Play();
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 9)
        {
            explosionLand.transform.parent = transform.parent;
            explosionLand.Play();
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 4)
            Destroy(gameObject);
    }
}
