using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class P_BATscript : MonoBehaviour {

	Animator anim;
	Rigidbody2D rigid;
	Vector2 current;
	public Transform target;
	public Button ATK;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		current = transform.position;
	

	}
	
	// Update is called once per frame
	void Update () {



		}

	public void Attack (bool ATK ) {


		if (ATK) {

			anim.SetBool("WalkL",true);
			rigid.velocity = new Vector3 (target.position.x, target.position.y,0) * Time.deltaTime * 50f;
			//anim.SetBool("WalkL", false);
//			anim.SetBool("Slap", true);
//			anim.SetBool("Slap", false);
//			anim.SetBool("WalkR", true);
//			rigid.velocity = new Vector3 (current.x, current.y, 0) * Time.deltaTime * 10f;
//			anim.SetBool("WalkR", false);
			print ("Attack");

		}

	
	}
}
