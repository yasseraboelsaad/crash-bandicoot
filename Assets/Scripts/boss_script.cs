using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator anim;
	bool isBossActive;
	static int idleState = Animator.StringToHash("idle");

	// Use this for initialization
	void Start () {
		isBossActive = false;
		anim = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBossActive && anim.GetCurrentAnimatorStateInfo (0).IsName ("Crunch@idle")) {
//			int rand = Random.Range (0, 2);
			int rand = 1;
			if (rand == 0) {
				StartCoroutine (NormalAttack (0.0f));	
			} else {
				StartCoroutine (HeavyAttack (0.0f));
			}
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

	IEnumerator HeavyAttack(float time) {
		yield return new WaitForSeconds (time);
		anim.SetTrigger ("Heavy Attack");
	}
}
