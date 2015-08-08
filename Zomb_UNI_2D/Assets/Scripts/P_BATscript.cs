using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class P_BATscript : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigid;
	Vector2 current;
	Vector2 currentTarget;
	public Transform target;
	public Button ATK;
	public float time;
	public float time2;
	public bool back2;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		current = transform.position;
		currentTarget = target.position;
		time2 = 1f;
	
	}

	public void Update () {

		bool slap = false;
		bool back = false;

		if (anim.GetBool ("Slap")) {
			time -= Time.deltaTime;
		}

		if (time < 0) {

			anim.SetBool ("Slap", false);
			slap = true;


		}

		if (slap) {

			anim.SetBool ("WalkR", true);
			rigid.velocity = new Vector3 (current.x, current.y, 0) * Time.deltaTime * 20f;
			back = true;
			back2 = true;

		}



		if (transform.position.x >= current.x && back && back2) {
			rigid.velocity = new Vector3 (0, 0, 0) * 0;
			anim.SetBool ("WalkR", false);
			anim.SetBool ("IDLE", true);
			print ("notstop");
		}

	}


		



	

	public void Attack (bool ATK ) {


		if (ATK) {


			rigid.velocity = new Vector3 (target.position.x, target.position.y, 0) * Time.deltaTime * 20f;
			anim.SetBool ("WalkL", true);
			anim.SetBool ("IDLE", false);
			time2 = 1f;
			back2 = false;

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
