using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalProgressBar : ProgressBar {
    public Image rendy;
    public Sprite[] progressionAnim;

    public void Update()
    {

        if (progressionAnim.Length == 0 || max == 0)
        {
            return;
        }

        if (cur > max)
        {
            cur = max;
            rendy.sprite = weirdInterpolation(cur);
        }
        else if (cur < 0)
        {
            cur = 0;
            rendy.sprite = weirdInterpolation(cur);
        }
        else
        {
            rendy.sprite = weirdInterpolation(cur);
        }

    }

    private Sprite weirdInterpolation(float progress)
    {
        return progressionAnim[(int)((cur / max) * ((float)progressionAnim.Length - 1))];
    }
}
