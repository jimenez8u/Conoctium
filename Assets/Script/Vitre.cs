using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vitre : MonoBehaviour {
	public int directionToBreak;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
		float breackingSpeed = 0.5f;
		Debug.Log ("x speed " + collision.gameObject.GetComponent<Rigidbody> ().velocity.x);
		Debug.Log ("y speed " + collision.gameObject.GetComponent<Rigidbody> ().velocity.y);
		if(Mathf.Abs(collision.gameObject.GetComponent<Rigidbody>().velocity.x) >= breackingSpeed && directionToBreak == 0 || Mathf.Abs(collision.gameObject.GetComponent<Rigidbody>().velocity.y) >= breackingSpeed && directionToBreak == 1)
        { 
            gameObject.SetActive(false);
        }        
    }
}
