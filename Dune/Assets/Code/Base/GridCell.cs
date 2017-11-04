using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCell : MonoBehaviour {
	public int xPos;
	public bool occupied;

	public Sprite[] sprites;

	private SpriteRenderer rendy;

	void Start(){
		occupied = false;
		rendy = GetComponent<SpriteRenderer> ();
		rendy.sprite = Utils.getRandomEntry<Sprite>(sprites);
	}

}
