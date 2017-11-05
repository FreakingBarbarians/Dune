using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressBar : MonoBehaviour {

    protected float max;
    protected float cur;

    public void set(float max) {
        this.max = max;
        cur = max;
    }

    public void progress(float currentProgress)
    {
        cur = currentProgress;
    }
}
