using UnityEngine;
using System.Collections;

public class transitionToLevel2_script : MonoBehaviour {
	private double Timer = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;

		if(Timer>=5){
			Application.LoadLevel("Level 2");
		}
	}
}
