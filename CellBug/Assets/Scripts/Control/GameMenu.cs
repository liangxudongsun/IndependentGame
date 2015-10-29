using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel("02");
    }

    public void StopGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        Application.LoadLevel("02");
    }

    public void OpenSetPane()
    {
        TweenPosition tp = GameObject.Find("Panel_set").GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.PlayForward();
    }

    public void CloseSetPane()
    {
        TweenPosition tp = GameObject.Find("Panel_set").GetComponent<TweenPosition>();
        tp.PlayReverse();
    }

    public void OpenMenuPane()
    {
        TweenPosition tp = GameObject.Find("Panel_menu").GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.PlayForward();
    }

    public void CloseMenuPane()
    {
        TweenPosition tp = GameObject.Find("Panel_menu").GetComponent<TweenPosition>();
        tp.PlayReverse();
    }

    public void GoBack()
    {
        Application.LoadLevel("01");
    }
}
