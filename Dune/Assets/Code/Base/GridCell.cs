using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCell : MonoBehaviour {
	public int xPos;
    public bool occupied = false;

	public Sprite[] sprites;

	private SpriteRenderer rendy;
    private GridBehaviour obj = null;

	void Start(){
		rendy = GetComponent<SpriteRenderer> ();
		rendy.sprite = Utils.getRandomEntry<Sprite>(sprites);
	}

	public GridBehaviour getObject(){
		return obj;
	}

	public void setObject(GridBehaviour obj){
		this.obj = obj;
	}
}
