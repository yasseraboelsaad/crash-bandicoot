using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class death_screen_script : MonoBehaviour {

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
		SceneManager.LoadScene(crash_script.lastScene);
		GameObject[] rootObjects = SceneManager.GetActiveScene ().GetRootGameObjects ();
		foreach (GameObject g in rootObjects) {
			if (g.name == "Crash") {
				g.GetComponent<crash_script> ().init ();
			}
		}
	}

	void TaskOnClick2(){
		Application.Quit();
	}
}
