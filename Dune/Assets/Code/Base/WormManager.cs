using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour {

	public static WormManager instance;
	public GridCell[] wormCells;

	// gets initialized when it's needed
	private bool initialized = false;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Debug.Log ("DUDE STOP PLEASE AHHHHAGAHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAG i die now");
			Destroy (this.gameObject);
		}
		instance = this;
	}

	// How about the worm follows the player if his meter is such and such? :)
	public void spawnWorm(){
		
	}
}
