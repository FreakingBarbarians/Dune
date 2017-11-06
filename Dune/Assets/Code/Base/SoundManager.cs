using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public List<AudioClip> sounds;
	public List<float> weights;

	public float spawnRate;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Debug.Log("HEY WTF");
			Destroy (this.gameObject);
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
			
	}
}
