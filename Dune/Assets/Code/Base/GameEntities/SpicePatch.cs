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

	public string GetName(){
		return "spice";
	}

	// Use this for initialization
	void Start () {
		timer = tickRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0) {
			tick ();
		}
		if (mining) {
			timer -= Time.deltaTime;
		}
	}

	private void tick() {
		spiceValue -= 1;
		timer = tickRate;
		ScoreManager.instance.addScore (10);
		ScoreManager.instance.CreateScoreObject (10, transform.position);
		// @TODO: Add score stuff!
		if(spiceValue <= 0){
			OnDeath ();
			ScoreManager.instance.addScore (100);
			ScoreManager.instance.CreateScoreObject (100, transform.position);
			GameObject.Destroy (this.gameObject);
		}

	}

    public override void OnDeath()
    {
        base.OnDeath();
        GameObject.Destroy(this.gameObject);
    }
}
