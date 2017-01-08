using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kart : MonoBehaviour {
	public int TurnSpeed = 50;
	public int WalkSpeed = 20;
	public int apples;
	public int masks;
	public int lives;
	public Text livesText;
	public Text masksText;
	public Text applesText;
	public Rigidbody rigidBody;
	public int cam;
	public Camera fps;
	public Camera tps;
	public bool pause;
	public GameObject quitButton;
	// Use this for initialization
	void Start () {
		apples = 0;
		masks = 0;
		lives = 3;
		livesText.text = "Lives : " + lives.ToString ();
		masksText.text = "Masks : " + masks.ToString ();
		applesText.text = "Apples : " + apples.ToString ();
		cam = 1;
		if (cam == 1) {
			fps.gameObject.SetActive (true);
			tps.gameObject.SetActive (false);
		}
		pause = false;
		quitButton.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxis ("Vertical");
		transform.Translate (Vector3.forward * WalkSpeed * Time.deltaTime  );
		float h = Input.GetAxis ("Horizontal");
		transform.Rotate (new Vector3 (0, 1, 0) * -h * Time.deltaTime * TurnSpeed);
		checkKey ();

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("apple")) {
			apples = apples + 1;
			applesText.text = "Apples : " + apples.ToString ();
			other.gameObject.SetActive (false);
		} else {
			if(other.gameObject.CompareTag("mask")){
				masks = masks + 1;
				masksText.text = "Masks : " + masks.ToString ();
				other.gameObject.SetActive (false);
			} else { 
				if (other.gameObject.CompareTag ("bounce")) {
					rigidBody.AddForce (new Vector3 (0, 10, 0), ForceMode.Impulse);
					other.gameObject.SetActive (false);
				} else {
					if (other.gameObject.CompareTag ("hole")) {
						//transform.localScale.y = transform.localScale.y - 
						SceneManager.LoadScene ("Death Screen");
					} else {
						if (other.gameObject.CompareTag ("finish")) {
							WalkSpeed = 0;
							SceneManager.LoadScene ("TransitionToLevel4");

						}
					}
				}
			}
		}

	}

	void checkKey() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (rigidBody.position.y < 6)
				rigidBody.AddForce (new Vector3 (0, 10, 0), ForceMode.Impulse);
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			if (cam == 1) {
				cam = 2;
				tps.gameObject.SetActive (true);
				fps.gameObject.SetActive (false);
			} else {
				if (cam == 2) {
					cam = 1;
					fps.gameObject.SetActive (true);
					tps.gameObject.SetActive (false);
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (pause == false) {
				pause = true;
				WalkSpeed = 0;
				TurnSpeed = 0;
				quitButton.SetActive (true);
			} else {
				if (pause == true) {
					pause = false;
					WalkSpeed = 20;
					TurnSpeed = 50;
					quitButton.SetActive (false);
				}
			}
		}

	}


}
