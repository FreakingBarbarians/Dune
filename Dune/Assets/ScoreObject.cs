using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreObject : MonoBehaviour {

	public Text displayObject;
	private int score;
	private float timer  = 1f;

	private float alpha;

	Color col = Color.cyan;

	void Start(){
		alpha = Random.value - 0.5f;
		alpha *= 2;
	}

	void Update(){
		col.b = (float)score/200f;

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Destroy (this.gameObject);
		}
			
		displayObject.color = col;
		transform.position += new Vector3 (alpha, 1, 0) * Time.deltaTime;
	}

	public void setScore(int score){
		this.score = score;
		displayObject.text = score.ToString ();
	}
}
