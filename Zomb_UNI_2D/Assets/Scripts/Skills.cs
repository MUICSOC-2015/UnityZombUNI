using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Skills : System.Object {

	public Names Name;
	public int Damage;
	public string Info;
	public bool melee;

}


public enum Names {

	Induction,
	Suicide,
	RandomMagic,
	Normal


}
	