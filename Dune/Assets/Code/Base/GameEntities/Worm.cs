using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : GridBehaviour {
	[Range(0,1)]
	public float moveTime;

	public int originalPos;

	private float timer;

	private Vector3 oldPos;
	private Vector3 newPos;

	private int mode = 0;

	private bool moving;

	void Start() {
		
	}

	void Update(){
		if (timer <= 0) {
			moveRandom ();			
		}
	}

	private void moveRandom(){
		int go = UnityEngine.Random.Range (-1, 2);

		if (xPos == 0) {
			go = 1;
		} else if (xPos + size < GridManager.instance.getGameSize() - 1) {
			go = -1;
		}

		if(xPos - originalPos >= size/2) {
			go = -1;
		} else if (xPos - originalPos <= -size/2) {
			go = 1;
		}

		newPos = transform.position + new Vector3 (go, 0, 0);
		xPos += go;

		

	}
}