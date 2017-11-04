using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fremen : GridBehaviour {

	public int facing = 1;

	[Range(0,1)]
	// time it takes to move 1 tile
	public float moveTime;

	// time spent moving
	private float elapsedTime;

	// direction of movement
	public int moveDirection;

	public bool moving = false;

	private Vector3 newPos;
	private Vector3 oldPos;

	void Start() {
		moving = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			moveLeft ();
			Debug.Log ("L");
		}

		if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
			moveRight ();
			Debug.Log ("R");
		}

		if (moving) {

			Vector3 deltapos = newPos - oldPos;

			transform.position = oldPos + deltapos * (elapsedTime / moveTime);

			elapsedTime += Time.fixedDeltaTime;

			// doing this at the end allows us to catch any overshooting.
			if (elapsedTime >= moveTime) {
				moving = false;
				transform.position = newPos;
			}
		}
	}

	public virtual void moveLeft(){
		if (xPos == 0 || moving) {
			return;
		}

		oldPos = transform.position;
		newPos = transform.position + new Vector3 (-1, 0, 0);
		moving = true;
		elapsedTime = 0;
		xPos--;
		transform.localScale = new Vector3 (-1, 1, 1);
		// @TODO: Notify some manager when a step was taken to detect rhythmy!!!
	}

	public virtual void moveRight(){
		// account for base 0'ness
		if (xPos == GridManager.instance.getGameSize() - 1 || moving) {
			return;	
		}

		oldPos = transform.position;
		newPos = transform.position + new Vector3 (1, 0, 0);
		moving = true;
		elapsedTime = 0;
		xPos++;
		transform.localScale = new Vector3 (1, 1, 1);
		// @TODO: Notify some manager when a step was taken to detect rhythmy!!!
		// I CANT SPELL RHYTHM FUCK IT OK
	}
}
