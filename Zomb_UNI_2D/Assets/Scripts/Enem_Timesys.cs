using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enem_Timesys : MonoBehaviour {

	public Scrollbar bar;
	public bool ATK;

	// Use this for initialization
	void Start () {

		bar.size = 0;
		ATK = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		bar.size += Time.deltaTime * 0.25f;

		if (ATK) {

			Start ();

		}
	
	}


	void LateUpdate() {

		if (bar.size == 1) {

			ATK = true;

		}

	}
}
