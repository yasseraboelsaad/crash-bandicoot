using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator bossAnim;
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
		bossAnim = boss.GetComponent<Animator> ();
		bossCam.enabled = false;
		mainCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isBossActive && bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			int rand = Random.Range (0, 3);

			if (rand != 2) {
				if (!bossAnim.GetBool ("heavyAtk")) {
					bossAnim.SetTrigger ("normalAtk");
				}
			} else {
				if (!bossAnim.GetBool ("normalAtk")) {
					bossAnim.SetTrigger ("heavyAtk");
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

	void OnTriggerEnter(Collider c) {
		if (transform.tag == "Boss" && c.gameObject.tag == "Player"
			&& isBossActive && bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			Animator crashAnim = c.gameObject.GetComponentInChildren<Animator> ();
			AnimatorStateInfo currentState = crashAnim.GetCurrentAnimatorStateInfo(0);
			if (currentState.IsName ("Spin")) {
				bossHp--;
			}
		}
	}

//	void OnCollisionEnter(Collision c) {
//		Debug.Log ("see");
//		if (transform.tag == "Boss" && c.gameObject.tag == "Player"
//			&& isBossActive && bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
//			Animator crashAnim = c.gameObject.GetComponentInChildren<Animator> ();
//			AnimatorStateInfo currentState = crashAnim.GetCurrentAnimatorStateInfo(0);
//			if (currentState.IsName ("Spin")) {
//				bossHp--;
//			}
//		}
//	}
}
