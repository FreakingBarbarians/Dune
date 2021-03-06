﻿using System.Collections;
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
		createGame (50);
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

            // create camera sentinels

            if (i == 0) {
                cell.AddComponent<BoxCollider2D>();
                Camera.main.gameObject.GetComponent<CameraController>().LeftSentinel = cell;
            }

            if (i == gameSize - 1) {
                cell.AddComponent<BoxCollider2D>();
                Camera.main.gameObject.GetComponent<CameraController>().RightSentinel = cell;
            }
        }

		cells = temp.ToArray ();

		// create fremen
		GameObject fremen = Instantiate (fremenPrefab);

		fremen.transform.position = this.transform.position + new Vector3(gameSize/2, 0,0);
        fremen.GetComponent<GridBehaviour>().xPos = gameSize / 2;

		Camera.main.transform.position += new Vector3 (gameSize / 2, 0, 0);
		gameOn = true;
		this.gameSize = gameSize;
	}

	public int getGameSize(){
		return gameOn ? gameSize : -1;
	}

	public GridBehaviour getObjectAt(int xPos){
		if (xPos >= gameSize || xPos < 0) {
			return null;
		}
		return cells [xPos].getObject ();
	}

	public void free(int start, int size){
		for (int i = start; i < gameSize && i < start + size; i++) {
			cells [i].occupied = false;
			cells [i].setObject (null);
		}
	}
}
