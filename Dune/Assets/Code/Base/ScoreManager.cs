using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	public int score;

	public GameObject scorePrefab;

	void Start () {
		if (instance != null) {
			Debug.Log ("HEY HEY HEY MAN WTF THS IS A SINGLETON " + GetType ().Name + " will now self-destruct");
			Destroy (this.gameObject);
		}
		instance = this;
		score = 0;		
	}

	public void addScore(int amt){
		score += amt;
		ScoreDisplay.instance.notify ();
	}

	public void CreateScoreObject(int amt, Vector3 pos){
		GameObject score = Instantiate (scorePrefab);
		score.transform.position = pos;
		score.GetComponent<ScoreObject> ().setScore (amt);
	}

}
