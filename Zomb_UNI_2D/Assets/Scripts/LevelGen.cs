using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {


	public GameObject ground;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		// create ground
		for (int y = 0; y < 10; y++) {
			for (int x = 0; x < 10; x++) {
				Instantiate(ground, new Vector2(x, y), Quaternion.identity);
			}
		}

		for (int i = 0; i < 11; i++) {
			// create wall
			Instantiate(wall, new Vector2(-1,i), Quaternion.identity);
			Instantiate(wall, new Vector2(10, i), Quaternion.identity);
			Instantiate(wall, new Vector2(i, 10), Quaternion.identity);
			Instantiate(wall, new Vector2(i, -1), Quaternion.identity);

		}


	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
