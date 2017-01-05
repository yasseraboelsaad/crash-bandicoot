﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;

public class win_script : MonoBehaviour {

	GameObject player;
	crash_script controlscript;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Crash");
		controlscript = player.GetComponent<crash_script>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (player.GetComponent <Collider> () == c) {
			if (crash_script.lastScene.EndsWith ("4")) {
				SceneManager.LoadScene ("credits");
			} 
			else {
//				string newScene = crash_script.lastScene;
				StringBuilder sb = new StringBuilder (crash_script.lastScene);
				Debug.Log ("old: " + sb.ToString());
				int sceneNumber = int.Parse (sb.ToString().Substring (sb.Length - 1));
				int newSceneNumber = sceneNumber + 1;
				Debug.Log (sceneNumber.ToString () + " " + newSceneNumber.ToString ());
				sb.Length--;
				string newScene = sb.ToString ();
				newScene += newSceneNumber.ToString ();
				newScene = newScene.Replace (" ", string.Empty);
				newScene = "TransitionTo" + newScene;
				Debug.Log ("new: " + newScene);
				SceneManager.LoadScene (newScene);
			}

		}
	}

	public void nextScene() {
		if (crash_script.lastScene.EndsWith ("4")) {
			SceneManager.LoadScene ("credits");
		} 
		else {
			//				string newScene = crash_script.lastScene;
			StringBuilder sb = new StringBuilder (crash_script.lastScene);
			Debug.Log ("old: " + sb.ToString());
			int sceneNumber = int.Parse (sb.ToString().Substring (sb.Length - 1));
			int newSceneNumber = sceneNumber + 1;
			Debug.Log (sceneNumber.ToString () + " " + newSceneNumber.ToString ());
			sb.Length--;
			string newScene = sb.ToString ();
			newScene += newSceneNumber.ToString ();
			newScene = newScene.Replace (" ", string.Empty);
			newScene = "TransitionTo" + newScene;
			Debug.Log ("new: " + newScene);
			SceneManager.LoadScene (newScene);
		}
	}
}
