using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpicePatch : GridBehaviour, IGridInteractable {

	[Range(0,1)]
	public float tickRate;
	[Range(0,10)]
	public int spiceValue;

	private float timer;
	private bool mining;

	public void StartInteract() {
		mining = true;
		timer = tickRate;
	}

	public void StopInteract() {
		mining = false;
	}

	// Use this for initialization
	void Start () {
		timer = tickRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0) {
			
		}
		timer -= Time.deltaTime;
	}

	private void tick() {
		spiceValue -= 1;
		timer = tickRate;
		// @TODO: Add score stuff!
	}
}
