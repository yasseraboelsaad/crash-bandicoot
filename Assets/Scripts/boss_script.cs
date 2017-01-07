using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	Animator bossAnim;
	public bool isBossActive;
	bool isInBossZone;
	bool timerOn;
	bool isBossVulnerable;
	static int idleState = Animator.StringToHash("idle");
	public GameObject boss;
	public Camera mainCam;
	public Camera bossCam;
	public int bossHp;
	private double lastAtkWasIn = 0.0;
	public static bool isPaused;

	// Use this for initialization
	void Start () {
		bossHp = 3;
		bossAnim = boss.GetComponent<Animator> ();
		bossCam.enabled = false;
		mainCam.enabled = true;
		isBossVulnerable = true;
		isPaused = false;
	}

	public void init () {
		bossHp = 3;
		bossCam.enabled = false;
		mainCam.enabled = true;
		isBossVulnerable = true;
		isBossActive = false;
		isInBossZone = false;
		timerOn = false;
		lastAtkWasIn = 0.0;
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			bossAnim.ResetTrigger ("normalAtk");
			bossAnim.ResetTrigger ("heavyAtk");
			bossAnim.ResetTrigger ("booyakasha");
		}
		else {
			if (bossHp == 0) {
				bossAnim.SetTrigger ("u_ded");
				isBossActive = false;
				bossHp--;
				bossAnim.SetBool ("isBossAlive", false);
			}
			if (bossAnim.GetCurrentAnimatorStateInfo (0).IsName ("Crunch@dying")) {
				if (bossAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.1) {
					GameObject.Find ("BossMusic").GetComponent<AudioSource> ().Stop ();
					GameObject.Find ("BossDie").GetComponent<AudioSource> ().Play ();
				}
				if (bossAnim.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1) {
					GameObject.Find ("Crash").GetComponent<win_script> ().nextScene ();
				}
			}
			if (lastAtkWasIn > 12.0) {
				timerOn = false;
				isBossVulnerable = true;
				lastAtkWasIn = 0.0;
			}
			if (timerOn)
				lastAtkWasIn += Time.deltaTime;
			if (isBossActive && bossAnim.GetCurrentAnimatorStateInfo (0).tagHash == idleState) {
				int rand = Random.Range (0, 3);
				// rand = 2;
				if (rand != 2) {
					if (!bossAnim.GetBool ("heavyAtk") && !bossAnim.GetBool ("booyakasha")) {
						bossAnim.SetTrigger ("normalAtk");
					}
				} else {
					if (!bossAnim.GetBool ("normalAtk") && !bossAnim.GetBool ("booyakasha")) {
						bossAnim.SetTrigger ("heavyAtk");
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (!isPaused && transform.tag == "Boss" && c.gameObject.tag == "Player"
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
		if (!isPaused && isBossVulnerable && transform.tag == "Boss" && c.gameObject.tag == "Player"
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
