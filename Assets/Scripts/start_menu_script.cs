using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class start_menu_script : MonoBehaviour {

	public Button newGame;
	public Button quit;

	// Use this for initialization
	void Start () {
		Button btn = newGame.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Button btn2 = quit.GetComponent<Button>();
		btn2.onClick.AddListener(TaskOnClick2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TaskOnClick(){
		Application.LoadLevel("Level 1");
	}

	void TaskOnClick2(){
		Debug.Log ("You have clicked the quit button!");
	}
}
