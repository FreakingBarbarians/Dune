using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	public static ScoreDisplay instance;
	public Text scoredisplayer;

	public void Start(){
		if (instance != null) {
			Debug.Log ("YOU DUMB DUMB THE SCORE DISPLAY IS A SINGLETON WTF MAN. kk I die now");
			Destroy (this.gameObject);
		}

		instance = this;
	}

	public void notify(){
		scoredisplayer.text = "Score: " + ScoreManager.instance.score;
	}
}
