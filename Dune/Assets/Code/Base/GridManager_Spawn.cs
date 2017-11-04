using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridManager : MonoBehaviour {
	// The rate at which spice (and other things possibly) spawn
	[Range(0, 5)]
	public float spawnRate;

	// The delta in which spice (and other things possibly) spawn
	[Range(0,5)]
	public float rateJitter;

	// The cooldown time before spawning the next thing
	[Range(0,5)]
	public float minRate;

	// The TIME TIME TIME
	private float timer = 0;

	public List<GameObject> SpawnObjects;
	public List<float> SpawnWeights;

	void Update () {
		if (timer <= 0) {
			// time until next spawn
			spawn(Utils.getWeightedEntry(SpawnObjects,SpawnWeights));
			// Debug.Log ("returned");
			timer = Mathf.Max (spawnRate + UnityEngine.Random.Range (-rateJitter / 2, rateJitter / 2), minRate);
		}
			timer -= Time.deltaTime;
	}

	void spawn(GameObject prefab){
		// will try to find a random position 10 times that it can spawn the item in
		// Otherwise it will return and not spawn the item

		GridBehaviour gridPrefab = prefab.GetComponent<GridBehaviour> ();

		// try this ten times
		for (int i = 0; i < 10; i++) {
			// choose the random cell
			int potential = UnityEngine.Random.Range (0, gameSize);
			bool good = true;

			// check if those cells are good
			for(int x = 0; x < gridPrefab.size; x++){

				// not enough space anyways
				if (potential + x > gameSize - 1) {
					good = false; 
					break;
				}

				// There is enough space, lets see if it works!
				if (cells [potential + x].occupied) {
					good = false;
					break;
				}
			}

			if (good) {
				// spawn
				GameObject spawned = Instantiate(prefab);

				for(int x = 0; x < gridPrefab.size; x ++){
					cells[potential + x].occupied = true;
				}

				spawned.transform.position = transform.position;
				spawned.transform.position += new Vector3 (potential, 0, 0);
				spawned.GetComponent<GridBehaviour> ().xPos = potential;
				// Debug.Log ("RETURN");
				return;
			}
		}
	}
}
