using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingAnimation : AnimationBase {

    public Sprite[] anim;
    public SpriteRenderer rendy;

    public float animationTime;

    public void Start()
    {
        this.set(animationTime);
    }

    public void Update()
    {
        if (cur > animTime) {
            cur = 0;
        }

        rendy.sprite = weirdInterpolation(cur);

        cur += Time.deltaTime;
    }

    private Sprite weirdInterpolation(float progress)
    {
        if (anim.Length == 0) {
            return null;
        }
        return anim[(int)((cur / animTime) * ((float)anim.Length))];
    }

}
