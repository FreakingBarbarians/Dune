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

	void Start() {
				
	}
}