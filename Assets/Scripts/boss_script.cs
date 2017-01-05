﻿using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator bossAnim;
	static bool isBossActive;
	static bool isInBossZone;
	static bool timerOn;
	static bool isBossVulnerable;
	static int idleState = Animator.StringToHash("idle");
	public GameObject boss;
	public Camera mainCam;
	public Camera bossCam;
	public int bossHp;
	private double lastAtkWasIn = 0.0;

	// Use this for initialization
	void Start () {
		bossHp = 3;
		bossAnim = boss.GetComponent<Animator> ();
		bossCam.enabled = false;
		mainCam.enabled = true;
		isBossVulnerable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (bossHp == 0) {
			bossAnim.SetTrigger ("u_ded");
			isBossActive = false;
			bossHp--;
			bossAnim.SetBool ("isBossAlive", false);
		}
		if (bossAnim.GetCurrentAnimatorStateInfo (0).IsName ("Crunch@dying")) {
			GameObject sound = GameObject.Find ("BossDie");
			AudioSource audio = sound.GetComponent<AudioSource>();
			audio.Play ();
		}
		if (lastAtkWasIn > 12.0) {
			timerOn = false;
			isBossVulnerable = true;
			lastAtkWasIn = 0.0;
		}
		if (timerOn)
			lastAtkWasIn += Time.deltaTime;
		if (isBossActive && bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			int rand = Random.Range (0, 3);
			// rand = 2;
			if (rand != 2) {
				if (!bossAnim.GetBool ("heavyAtk") && !bossAnim.GetBool("booyakasha")) {
					bossAnim.SetTrigger ("normalAtk");
				}
			} else {
				if (!bossAnim.GetBool ("normalAtk") && !bossAnim.GetBool("booyakasha")) {
					bossAnim.SetTrigger ("heavyAtk");
				}
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (transform.tag == "Boss" && c.gameObject.tag == "Player"
		    && isBossActive) {
			isInBossZone = true;
		}
	}

	void OnTriggerStay(Collider c) {
		//		if (transform.tag == "Boss") {
		//			Debug.Log (isBossActive.ToString() + '\n'
		//				+ (bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState).ToString()
		//				+ '\n' + c.gameObject.tag);
		//		}
		if (isBossVulnerable && transform.tag == "Boss" && c.gameObject.tag == "Player"
			&& isBossActive && bossAnim.GetCurrentAnimatorStateInfo(0).tagHash == idleState) {
			Animator crashAnim = c.gameObject.GetComponentInChildren<Animator> ();
			AnimatorStateInfo currentState = crashAnim.GetCurrentAnimatorStateInfo(0);
			int spinHash = Animator.StringToHash ("Spin");
			if (currentState.shortNameHash == spinHash && isInBossZone) {
				GameObject sound = GameObject.Find ("EnemyHit");
				AudioSource audio = sound.GetComponent<AudioSource>();
				audio.Play ();
				bossHp--;
				isInBossZone = false;
				bossAnim.ResetTrigger ("normalAtk");
				bossAnim.ResetTrigger ("heavyAtk");
				bossAnim.SetTrigger ("booyakasha");
				timerOn = true;
				isBossVulnerable = false;
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

//	void OnCollisionExit(Collision c) {
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