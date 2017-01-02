using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator anim;
	bool isBossActive;
	static int idleState = Animator.StringToHash("idle");
	public GameObject boss;
	public Camera mainCam;
	public Camera bossCam;

	// Use this for initialization
	void Start () {
		isBossActive = false;
		anim = boss.GetComponent<Animator> ();
		bossCam.enabled = false;
		mainCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBossActive && anim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			float rand = Random.Range (0, 3);

			if (rand != 2) {
				if (!anim.GetBool ("heavyAtk")) {
					anim.SetTrigger ("normalAtk");
				}
//				StartCoroutine (NormalAttack (20));	
			} else {
				if (!anim.GetBool ("normalAtk")) {
					anim.SetTrigger ("heavyAtk");
				}
//				StartCoroutine (HeavyAttack (10));
			}
		}
	}

	void OnTriggerExit(Collider c) {
		if (transform.tag == "entry") {
			GetComponent<Collider> ().isTrigger = false;
			isBossActive = true;
			bossCam.enabled = true;
			mainCam.enabled = false;
		}
	}

	IEnumerator NormalAttack(float time) {
		yield return new WaitForSeconds (time);
		anim.SetTrigger ("normalAtk");
	}

	IEnumerator HeavyAttack(float time) {
		yield return new WaitForSeconds (time);
		anim.SetTrigger ("heavyAtk");
	}
}
