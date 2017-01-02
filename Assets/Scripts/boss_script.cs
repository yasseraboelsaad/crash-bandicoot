using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator anim;
	bool isBossActive;
	static int idleState = Animator.StringToHash("idle");
	public GameObject boss;
	public Camera mainCam;
	public Camera bossCam;
	public int bossHp;

	// Use this for initialization
	void Start () {
		bossHp = 3;
		isBossActive = false;
		anim = boss.GetComponent<Animator> ();
		bossCam.enabled = false;
		mainCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBossActive && anim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			int rand = Random.Range (0, 3);

			if (rand != 2) {
				if (!anim.GetBool ("heavyAtk")) {
					anim.SetTrigger ("normalAtk");
				}
			} else {
				if (!anim.GetBool ("normalAtk")) {
					anim.SetTrigger ("heavyAtk");
				}
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

	void OnCollisionExit(Collider c) {
		if (transform.tag == "Boss" && c.transform.tag == "Player") {
			
		}
	}
}
