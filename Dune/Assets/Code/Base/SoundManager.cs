using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public List<AudioClip> sounds;
	public List<float> weights;

	[Range(0,100)]
	public float spawnRate;
	[Range(0,100)]
	public float spawnJitter;

	private float timer;

	public AudioSource src;

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
		if (src.isPlaying) {
			return;
		}

		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = spawnRate + Random.Range (-spawnJitter, spawnJitter);
			src.clip = Utils.getWeightedEntry<AudioClip> (sounds, weights);
			src.Play ();
		}
	}
}
