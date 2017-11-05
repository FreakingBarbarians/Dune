using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 

public class Fremen : GridBehaviour {

    public static Fremen instance;

	public int facing = 1;

	[Range(0,1)]
	// time it takes to move 1 tile
	public float moveTime;

	// time spent moving
	private float elapsedTime;

	// direction of movement
	public int moveDirection;

	public bool moving = false;
	public bool interacting = false;

	private Vector3 newPos;
	private Vector3 oldPos;

    public OneShotAnimation walkingAnimation;

	void Start() {
        if (instance != null) {
            Debug.Log("This town ain't big enough for the two of us...");
            GameObject.Destroy(this.gameObject);
        }

        instance = this;
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

		if (Input.GetKeyDown(KeyCode.Space)){
			startInteracting ();
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			stopInteracting ();
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

	private void startInteracting(){
		// interact!
		// continuously play the interaction animation, should be interruptable...

		IGridInteractable interactable = (IGridInteractable) GridManager.instance.getObjectAt (xPos);

		if (interactable == null) {
			return;
		}

		interacting = true;
		interactable.StartInteract ();
		Debug.Log ("INTERACT WITH ME");
	}

	private void stopInteracting(){
		interacting = false;

		IGridInteractable interactable = (IGridInteractable) GridManager.instance.getObjectAt (xPos);

		if (interactable == null) {
			return;
		}

		interactable.StopInteract();	
	}

	public override void moveLeft(){
		if (xPos == 0 || moving) {
			return;
		}

		oldPos = transform.position;
		newPos = transform.position + new Vector3 (-1, 0, 0);
		moving = true;
		elapsedTime = 0;
		xPos--;
		transform.localScale = new Vector3 (-1, 1, 1);
        
        stopInteracting ();

        // @TODO: Notify some manager when a step was taken to detect rhythmy!!!
        RythmManager.instance.step();
        walkingAnimation.playOnce();
    }

	public override void moveRight(){
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
        
        stopInteracting ();

        // @TODO: Notify some manager when a step was taken to detect rhythmy!!!
        // I CANT SPELL RHYTHM FUCK IT OK
        RythmManager.instance.step();
        walkingAnimation.playOnce();
    }
}
