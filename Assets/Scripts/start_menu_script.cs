using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class start_menu_script : MonoBehaviour {

	public Button newGame;
	public Button options;
	public Button quit;
	public Button Mute;
	public Button howToPlay;
	public Button credits;
	public Button back;
	public Text howToPlayText;
	public Text creditsText;
	GameObject[] optionsObjects;
	GameObject[] startObjects;
	Text text1;
	Text text2;

	// Use this for initialization
	void Start () {
		Button btn = newGame.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		Button btn2 = quit.GetComponent<Button>();
		btn2.onClick.AddListener(TaskOnClick2);
		Button btn3 = options.GetComponent<Button>();
		btn3.onClick.AddListener(TaskOnClick3);
		Button btn4 = Mute.GetComponent<Button>();
		btn4.onClick.AddListener(TaskOnClick4);
		Button btn5 = howToPlay.GetComponent<Button>();
		btn5.onClick.AddListener(TaskOnClick5);
		Button btn6 = credits.GetComponent<Button>();
		btn6.onClick.AddListener(TaskOnClick6);
		Button btn7 = back.GetComponent<Button>();
		btn7.onClick.AddListener(TaskOnClick7);
		optionsObjects = GameObject.FindGameObjectsWithTag("OptionsMenu");
		startObjects = GameObject.FindGameObjectsWithTag("StartMenu");
		foreach(GameObject g in optionsObjects){
			g.SetActive(false);
		}
		text1 =  howToPlayText.GetComponent<Text>();
		text1.enabled = false;
		text2 =  creditsText.GetComponent<Text>();
		text2.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TaskOnClick(){
		Application.LoadLevel("Level 1");
	}

	void TaskOnClick2(){
		Application.Quit();
	}

	void TaskOnClick3(){
		foreach(GameObject g in startObjects){
			g.SetActive(false);
		}
		foreach(GameObject g in optionsObjects){
			g.SetActive(true);
		}
	}

	void TaskOnClick4(){
	//mute here
		AudioListener.volume = 0;
	}

	void TaskOnClick5(){
		text1.enabled = true;
	}

	void TaskOnClick6(){
		text2.enabled = true;
	}

	void TaskOnClick7(){
		foreach(GameObject g in startObjects){
			g.SetActive(true);
		}
		foreach(GameObject g in optionsObjects){
			g.SetActive(false);
		}
		text1.enabled = false;
		text2.enabled = false;
	}
}
