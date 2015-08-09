using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enem_HP : MonoBehaviour {
	public Scrollbar healthbar;
	// Use this for initialization
	void Start () {
	
	}
	
	void EnemDamage (float damage) {

		healthbar.size -= damage / 100f;

	}	
}
