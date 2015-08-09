using UnityEngine;
using System.Collections;

public class CamControll : MonoBehaviour {

	public Transform target;
	
	// Use this for initialization
	void Start () {

		transform.position = new Vector3 (target.position.x, target.position.y, 0);


	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp(transform.position ,new Vector3 (target.position.x, target.position.y, 0), Time.deltaTime * 10);
	
	}
}
