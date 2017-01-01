using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator anim;
	bool isBossActive = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBossActive) {
			
		}
	}

	void OnTriggerExit(Collider c) {
		if (transform.tag == "entry") {
			GetComponent<Collider> ().isTrigger = false;
			isBossActive = true;
		}
	}

	IEnumerator NormalAttack(float time) {
		yield return new WaitForSeconds (time);

		anim.SetTrigger ("Normal Attack");
	}
}
