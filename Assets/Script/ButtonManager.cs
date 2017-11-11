using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	Animator animator;

	void Start () {
		animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
	}

	public void Transition1 () {
		animator.SetTrigger("MoveMenuRight");
	}
	public void Transition2 () {

		animator.SetTrigger("MoveMenuLeft");

	}
	public void NewGameBtn(string newGameLevel)
    {
		
        SceneManager.LoadScene(newGameLevel);

    }

    public void ExitGameBtn()
    {

        Application.Quit();
    }
}
