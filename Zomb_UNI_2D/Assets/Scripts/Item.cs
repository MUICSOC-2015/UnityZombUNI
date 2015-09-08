using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item: MonoBehaviour {
	
	public string Name;
	public int giveHP;
	public int giveMP;
	public string Info;
	public int Amount;
	public Type Type;



}


public enum Type {
	
	HP_Potion,
	MP_Potion,
	Poison,
	Super_Potion
	
	
}

