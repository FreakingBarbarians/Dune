using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour {

    protected float animTime;
    protected float cur;

    public void set(float animTime) {
        this.animTime = animTime;
    }

    public void animate(float playbackTime) {
        this.cur = playbackTime;
    }
}
