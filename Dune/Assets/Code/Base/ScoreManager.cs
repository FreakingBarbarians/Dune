using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	public int score;

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

}
