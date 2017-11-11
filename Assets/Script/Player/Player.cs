using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private float speed = 33.0f;
    private float jumpSpeed = 10f;
	private bool repulsion = false;
	private bool isGrounded;
	private float maxSpeed = 25f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		IsGrounded();
		
        //Selection du joueur
        string select = "";
        if(gameObject.tag == "Player1")
        {
            select = "Player1";
        }
        else
        {
            select = "Player2";
        }
        //On récupère la vitesse actuel
        Vector3 actualVelocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y, gameObject.GetComponent<Rigidbody>().velocity.z);

        //On récupère la direction (normalisé) des joystick dans l'axe x et y
        float horizontalMoove = Input.GetAxis("Horizontal" + select);
        float verticalMoove = Input.GetAxis("Vertical" + select);

        //■■■■■■■■■■ MOOVE ■■■■■■■■■■■
        //On ajoute à la vitesse actuel sa propre vitesse plus celle du joystick * speed * temps entre deux frames
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(actualVelocity.x + Input.GetAxis("Horizontal" + select) * speed * Time.deltaTime, actualVelocity.y, 0);

        //Mise à jour de la vitesse actuel
        actualVelocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y, gameObject.GetComponent<Rigidbody>().velocity.z);

        //■■■■■■■■■■ SAUT ■■■■■■■■■■■■
		if (Input.GetButtonDown("Fire1" + select) && isGrounded)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(actualVelocity.x, actualVelocity.y + jumpSpeed, 0);
		}

        //■■■■■■■■■■ ATTRACTION ■■■■■■■■■■■■
		if (Input.GetAxis("Fire2"+select) == 1)
		{
			//Debug.Log (Input.GetAxis("Fire2"+select) + " Attraction" + select);
			Vector3 otherPlayer;
			if (select == "Player1") 
			{
				otherPlayer = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Transform> ().position;
			}
			else 
			{
				otherPlayer = GameObject.FindGameObjectWithTag ("Player1").GetComponent<Transform> ().position;
			}
			Vector3 mePlayer = gameObject.GetComponent<Transform> ().position;
			Vector3 inbetween = Vector3.Normalize(otherPlayer - mePlayer);
			gameObject.GetComponent<Rigidbody> ().velocity += 2f*inbetween;
			//gameObject.GetComponent<Rigidbody>().velocity = new Vector3(actualVelocity.x, actualVelocity.y + jumpSpeed, 0);
		}
		//■■■■■■■■■■ REPULSION ■■■■■■■■■■■■
		if (Input.GetAxis("Fire3"+select) == 1)
		{
			//Debug.Log (Input.GetAxis("Fire3"+select) + " Repulsion" + select);
			if (!repulsion) 
			{
				Vector3 otherPlayer;
				if (select == "Player1") 
				{
					otherPlayer = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Transform> ().position;
				}
				else 
				{
					otherPlayer = GameObject.FindGameObjectWithTag ("Player1").GetComponent<Transform> ().position;
				}
				Vector3 mePlayer = gameObject.GetComponent<Transform> ().position;
				Vector3 inbetween = Vector3.Normalize (otherPlayer - mePlayer);
				gameObject.GetComponent<Rigidbody> ().velocity -= 20 * inbetween;
				
				repulsion = true;
			}
		}
		else if(isGrounded)
		{
			repulsion = false;
		}
        //■■■■■■■■■■ TESTS VITESSE ■■■■■■■■■■■■
        if (gameObject.GetComponent<Rigidbody> ().velocity.x > maxSpeed)
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(maxSpeed, gameObject.GetComponent<Rigidbody> ().velocity.y, gameObject.GetComponent<Rigidbody> ().velocity.z);

		if(gameObject.GetComponent<Rigidbody> ().velocity.y > maxSpeed)
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(gameObject.GetComponent<Rigidbody> ().velocity.x, maxSpeed, gameObject.GetComponent<Rigidbody> ().velocity.z);

		if(gameObject.GetComponent<Rigidbody> ().velocity.z > maxSpeed) 
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(gameObject.GetComponent<Rigidbody> ().velocity.x, gameObject.GetComponent<Rigidbody> ().velocity.y, maxSpeed);

		if (gameObject.GetComponent<Rigidbody> ().velocity.x < -maxSpeed)
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(-maxSpeed, gameObject.GetComponent<Rigidbody> ().velocity.y, gameObject.GetComponent<Rigidbody> ().velocity.z);

		if(gameObject.GetComponent<Rigidbody> ().velocity.y < -maxSpeed)
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(gameObject.GetComponent<Rigidbody> ().velocity.x, -maxSpeed, gameObject.GetComponent<Rigidbody> ().velocity.z);

		if(gameObject.GetComponent<Rigidbody> ().velocity.z < -maxSpeed) 
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(gameObject.GetComponent<Rigidbody> ().velocity.x, gameObject.GetComponent<Rigidbody> ().velocity.y, -maxSpeed);
    }


	private void OnCollisionEnter(Collision coli)
	{
		if (coli.gameObject.tag == "Player2" && gameObject.tag == "Player1") 
		{
			int level = SceneManager.GetActiveScene ().buildIndex;
			if(level == SceneManager.sceneCount)
				SceneManager.LoadScene(0, LoadSceneMode.Single);
			else
				SceneManager.LoadScene(level+1, LoadSceneMode.Single);
		}

	}
	private void IsGrounded()
	{
		float distanceTouch = 1f;
		Vector3 decal = new Vector3(0,-1,0);     

		RaycastHit hit;

		//Dessine un trait de couleur bleu si on ne touche pas le sol
		Debug.DrawRay(gameObject.GetComponent<Transform>().position, Vector3.down, Color.blue);

		//Si on touche un objet vers le bas à une distance inférieur à distanceTouch
		if (Physics.Raycast(gameObject.GetComponent<Transform>().position, Vector3.down, out hit, distanceTouch))
		{
			//Si l'objet a le tag "Sol"
			if (hit.transform.tag == "Sol")
			{
				//On dessine un trait de couleur rouge
				Debug.DrawRay(gameObject.GetComponent<Transform>().position, Vector3.down, Color.red);
				isGrounded = true;
			}
		}
		else
			isGrounded = false;        
	}
}

