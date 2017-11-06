using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : GridBehaviour {
	[Range(0,5)]
	public float moveTime;

    public GameObject child;

    public int originalPos;

    public float distanceMax;
    public float distanceCur;

    public ProgressBar closenessProgress;

	private float timer;

	private Vector3 oldPos;
	private Vector3 newPos;

	private int mode = 0;

	private bool moving;

	[Range(0,1)]
	public float approachRange;

	public float killTime;
	public bool killing = false;

	public Animator anim;

    private void Start()
    {
        oldPos = transform.position;
		anim = GetComponent<Animator> ();
    }

    void Update(){
        
        if (moving && !killing) {
            Vector3 deltapos = newPos - oldPos;
            transform.position = oldPos + deltapos * (timer / moveTime);

            timer += Time.fixedDeltaTime;
            if (timer >= moveTime) {
                transform.position = newPos;
				if (RythmManager.instance.rythmScore >= RythmManager.instance.maxRhythm / 2) {
					MoveTowardsPlayer ();
				} else {
					moveRandom ();
				}
            }
        }

        distanceCur += Time.fixedDeltaTime;
        closenessProgress.progress(distanceCur);

        if (distanceCur >= distanceMax) {
            OnDeath();
        }

		if (distanceCur >= approachRange * distanceMax) {
			anim.SetBool ("Approach", true);
		}

		if (killing) {
			transform.position += new Vector3 (0, -1, 0) * Time.deltaTime;
		}
		
	}

	private void moveRandom(){
        oldPos = newPos;
        timer = 0;
        int go;

        // can't move left
        if (xPos == 0 || WormManager.instance.wormCells[xPos - 1].occupied)
        {
            go = UnityEngine.Random.Range(0, 2);
        }
        // can't move right
        else if (xPos + size >= WormManager.instance.wormCells.Length- 1 || WormManager.instance.wormCells[xPos + size].occupied)
        {
            go = UnityEngine.Random.Range(-1, 1);
        }
        else {
            go = UnityEngine.Random.Range(-1, 2);
        }

        // we're moving right
        if (go == 1) {
            WormManager.instance.wormCells[xPos].occupied = false;
            WormManager.instance.wormCells[xPos].setObject(null);
            WormManager.instance.wormCells[xPos + size].occupied = true;
            WormManager.instance.wormCells[xPos + size].setObject(this);
            xPos++;
        }
        else if (go == -1) {
            WormManager.instance.wormCells[xPos - 1].occupied = true;
            WormManager.instance.wormCells[xPos - 1].setObject(this);
            WormManager.instance.wormCells[xPos + size - 1].occupied = false;
            WormManager.instance.wormCells[xPos + size - 1].setObject(null);
            xPos--;
        }

        newPos = transform.position + new Vector3(go, 0, 0);

        moving = true;
	}

	private void MoveTowardsPlayer(){
		oldPos = newPos;
		timer = 0;
		int go;

		if (Fremen.instance.xPos - xPos > 0) {
			go = 1;
		} else if (Fremen.instance.xPos - xPos < 0){
			go = -1;
		} else {
			go = 0;
		}

		// can't move left
		if ((xPos == 0 || WormManager.instance.wormCells[xPos - 1].occupied) && go == -1)
		{
			go = 0;
		}
		// can't move right
		else if ((xPos + size >= WormManager.instance.wormCells.Length || WormManager.instance.wormCells[xPos + size].occupied) && go == 1)
		{
			go = 0;
		}

		xPos += go;
		newPos = transform.position + new Vector3(go, 0, 0);
		moving = true;

	}
    public void set(int xPos, float distanceMax, int size) {
        Debug.Log(size);
        this.xPos = xPos;
        this.originalPos = xPos;
        this.distanceMax = distanceMax;
        this.distanceCur = 0;
        this.size = size;

        closenessProgress.set(distanceMax);
        closenessProgress.progress(distanceCur);

        child.transform.localScale = new Vector3(size, size, 1);
        child.transform.localPosition = new Vector3((size-1)*0.5f, 0.5f + size * 0.4f, 0); // scaling for wormies
        moveRandom();
    }

    public override void OnDeath()
    {
		// transform.position = newPos;
		child.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
        for (int i = 0; i < size; i++) {
            GridBehaviour gridObj;
            gridObj = GridManager.instance.getObjectAt(i + xPos);
            if (gridObj != null) {
                gridObj.OnDeath();
            }
        }

        if (Fremen.instance.xPos >= xPos && Fremen.instance.xPos <= xPos + size) {
            Fremen.instance.gameObject.GetComponent<GridBehaviour>().OnDeath();
            Fremen.instance.gameObject.SetActive(false);
            // Lose The Game
        }

        WormManager.instance.free(xPos, size);
        GameObject.Destroy(this.gameObject, 3f);
		killing = true;
    }
}