using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour {

    [Range(0,10)]
    public float wormDistanceFactor;

	public static WormManager instance;
	public GridCell[] wormCells;

    public GameObject wormPrefab;

	// gets initialized when it's needed
	private bool initialized = false;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.I)) {
            spawnWorm();
        }

    }

    // Use this for initialization
    void Start () {
		if (instance != null) {
			Debug.Log ("DUDE STOP PLEASE AHHHHAGAHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAGHAG i die now");
			Destroy (this.gameObject);
		}
		instance = this;
	}

    private void init() {
        wormCells = new GridCell[GridManager.instance.getGameSize()];
        for (int i = 0; i < wormCells.Length; i++) {
            wormCells[i] = Instantiate(GridManager.instance.gridCellPrefab).GetComponent<GridCell>();
            wormCells[i].transform.position = transform.position + new Vector3(i, 1, 0);
            wormCells[i].xPos = i;
            wormCells[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        initialized = true;
        Debug.Log("Initialized");
    }

	// How about the worm follows the player if his meter is such and such? :)
	public void spawnWorm(){

        if (!initialized) {
            init();
        }

        float max_x = Mathf.Log(GridManager.instance.getGameSize(), 2);
        float picker = UnityEngine.Random.value * max_x; // safety number
        int picked = (int) Mathf.Ceil(Mathf.Pow(2, picker))/2;
		picked = Mathf.Min (picked, 5);

        Debug.Log("Calculated max_x: " + max_x + " picker: " + picker + " picked: " + picked + " from:" + 0.1f * Mathf.Pow(2, picker));

        // try 3 times to spawn a worm.

        for (int i = 0; i < 3; i++) {
            int potential = UnityEngine.Random.Range(0, wormCells.Length);
            bool good = true;

            for (int x = 0; x < picked; x++) {
                if (wormCells[potential + x].occupied || potential + picked > wormCells.Length) {
                    good = false;
                    break;
                }
            }

            if (good) {
                GameObject worm = Instantiate(wormPrefab);
                for (int x = potential; x < potential + picked; x++)
                {
                    wormCells[x].occupied = true;
                    wormCells[x].setObject(worm.GetComponent<GridBehaviour>());
                }
                worm.transform.position = transform.position;
                worm.transform.position += new Vector3(potential, 0, 0);
                worm.GetComponent<Worm>().set(potential, UnityEngine.Random.Range(picked / 2 + 2, picked * 3) * wormDistanceFactor, picked);
                return;
            }
        }
	}

    public void free(int start, int size)
    {
        for (int i = start; i < wormCells.Length && i < start + size; i++)
        {
            wormCells[i].occupied = false;
            wormCells[i].setObject(null);
        }
    }

    public bool occupied(int xPos) {
        if (wormCells[xPos].occupied) {
            return true;
        }
        return false;
    }
}
