using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour {
	Animator animator;
	//GameObject btnmenu;
	//GameObject btnlevel;
	Button NewGameButton;

	void Start () {
		animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
		//btnmenu = GameObject.FindGameObjectWithTag("BtnMenu");
		//btnlevel = GameObject.FindGameObjectWithTag("BtnLevel");
	}

	public void Transition1 () {
		
		animator.SetTrigger("MoveMenuRight");
		StartCoroutine(toright());
	}
	public void Transition2 () {
		
		animator.SetTrigger("MoveMenuLeft");
		StartCoroutine(toleft());

	}


	 IEnumerator toright () {
		yield return new WaitForSeconds (2);
		GameObject.FindGameObjectWithTag("Level1btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Level2btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Level3btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Level4btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Level10btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Backbtn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("NewGameButton").GetComponent<Button>().interactable=false;

		GameObject.FindGameObjectWithTag("OptionsButton").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("Level1btn").GetComponent<Button>().Select(); 

	}

	 IEnumerator toleft () {
		yield return new WaitForSeconds (1);
		GameObject.FindGameObjectWithTag("NewGameButton").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("OptionsButton").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Level1btn").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("Level2btn").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("Level3btn").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("Level4btn").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("Level10btn").GetComponent<Button>().interactable=true;
		GameObject.FindGameObjectWithTag("Backbtn").GetComponent<Button>().interactable=false;
		GameObject.FindGameObjectWithTag("NewGameButton").GetComponent<Button>().Select();

	}
/*	public void isActive (GameObject gameObject) {
		int children = gameObject.childCount;
		for (int i = 0; i < children; ++i)
		{
			this.gameObject.GetComponentsInChildren<ButtonManager>;
	}


	}

	public void isnotActive (GameObject gameObject) {



	}*/
	public void NewGameBtn(string newGameLevel)
    {
		
        SceneManager.LoadScene(newGameLevel);

    }

    public void ExitGameBtn()
    {

        Application.Quit();
    }
}
