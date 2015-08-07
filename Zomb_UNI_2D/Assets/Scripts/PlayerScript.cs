using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D rigid;
	public float spd;

	// Use this for initialization
	void Start () {

		transform.position = new Vector2 (3f, 3.3f);
		rigid = GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {


		this.rigid.velocity = new Vector3 (Input.GetAxis ("Horizontal") , Input.GetAxis ("Vertical") , 0) * spd;
	
	}
}
