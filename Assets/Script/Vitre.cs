using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitre : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bite");
        float breackingSpeed = 0.5f;
        if(collision.gameObject.GetComponent<Rigidbody>().velocity.x >= breackingSpeed || collision.gameObject.GetComponent<Rigidbody>().velocity.y >= breackingSpeed || collision.gameObject.GetComponent<Rigidbody>().velocity.z >= breackingSpeed)
        {
            Debug.Log("AAHAHHAHA");
            gameObject.SetActive(false);
        }        
    }
}
