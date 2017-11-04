using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GridManager : MonoBehaviour {

	public static GridManager instance;

	public GameObject gridCellPrefab;
	public GameObject fremenPrefab;

	private int gameSize;

	private GridCell[] cells;
	private bool gameOn = false;

	void Start(){
		if (instance != null) {
			Debug.Log ("HEY WHAT THE FUCK ARE U DOING THERE ARE 2 INSTANCES OF THIS SINGLETON:" + this.GetType ().Name + "... I WILl NOW SELF DESTRUCT");
			GameObject.Destroy (this.gameObject);
		}
		instance = this;
		gameOn = false;
		createGame (10);
	}

	public void createGame(int gameSize){
		List<GridCell> temp = new List<GridCell> ();
		for(int i = 0; i < gameSize; i ++){
			GameObject cell = Instantiate (gridCellPrefab);
			GridCell gcell = cell.GetComponent<GridCell> ();
			gcell.xPos = i;
			cell.transform.position = this.transform.position;
			cell.transform.position += new Vector3(i, 0, 0);
			temp.Add (gcell);
		}

		cells = temp.ToArray ();

		// create fremen
		GameObject fremen = Instantiate (fremenPrefab);
		fremen.transform.position = this.transform.position;

		Camera.main.transform.position += new Vector3 (gameSize / 2, 0, 0);
		gameOn = true;
		this.gameSize = gameSize;
	}

	public int getGameSize(){
		return gameOn ? gameSize : -1;
	}
}
