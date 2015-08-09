using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_ATK : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigid;
	Vector2 current;
	Vector2 currentTarget;
	public Transform target;
	public Button ATK;
	float time;
	float time2;
	bool slap;
	bool back;
	bool prevState;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		current = transform.position;
		currentTarget = target.position;
		time2 = 1f;
		time = 1.2f;
		slap = false;
		back = false;
		anim.SetBool ("IDLE", false);	
	
	}

	void EndAttack() {

		time2 = 1f;
		time = 1.2f;
		slap = false;
		back = false;
		//rigid.MovePosition (current);


	}

	public void Update () {



		if (anim.GetBool ("Slap")) {
			time -= Time.deltaTime;
		}

		if (time < 0 ) {

			anim.SetBool ("Slap", false);
			slap = true;


		}

		if (slap) {

			anim.SetBool ("WalkR", true);
			rigid.velocity = new Vector3 (current.x, current.y, 0) * Time.deltaTime * 50f;
			back = true;

		}

		if (back) {


			Invoke("comeBack", 1.5f);

			if (time2 <= 0 ){

				//CancelInvoke("comeback");
				EndAttack();

			}

		}



	}


		

	public void comeBack() {


		if (transform.position.x >= current.x) {
			rigid.velocity = new Vector3 (0, 0, 0) * 0;
			anim.SetBool ("WalkR", false);
			anim.SetBool ("IDLE", true);
			slap = false;
			time2-=Time.deltaTime;
		}


	}

	

	public void Attack (bool ATK ) {


		if (ATK) {

			anim.SetBool ("WalkL", true);

			if (anim.GetBool("WalkL")){
			rigid.velocity = new Vector3 (target.position.x, target.position.y, 0) * Time.deltaTime * 50f;
			}
			anim.SetBool ("IDLE", false);
			slap = false;
			time2 = 0.1f;
			time = 1.2f;


		}

	}
	


	

	public void OnCollisionEnter2D (Collision2D col) {

		anim.SetBool ("WalkL", false);
		anim.SetBool ("Slap", true);
		time = 1.2f;
		Vector3 v = rigid.velocity;
		v.y = 0.0f;
		rigid.velocity = v;

	}

	


}
