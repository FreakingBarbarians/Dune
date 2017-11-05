using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAnimation : AnimationBase {

    public Sprite[] anim;
    public SpriteRenderer rendy;

    public float animationTime;
    public bool shouldAnimate = false;


    public void Start()
    {
        this.set(animationTime);
    }

    public void Update()
    {

        if (shouldAnimate == false) {
            return;
        }

        if (cur >= animTime)
        {
            shouldAnimate = false;
            cur = animTime;
        }

        rendy.sprite = weirdInterpolation(cur);

        cur += Time.deltaTime;
    }

    private Sprite weirdInterpolation(float progress)
    {
        if (anim.Length == 0)
        {
            return null;
        }
        return anim[(int)((cur / animTime) * ((float)anim.Length - 1))];
    }

    public void playOnce() {
        shouldAnimate = true;
        cur = 0;
    }
}
