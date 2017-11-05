using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingProgressBar : ProgressBar
{
    [Range(0,1)]
    public float divider;

    public Image rendy;

    public Sprite[] progressionAnim;
    public Sprite[] flashAnim;

    public void Update()
    {

        if (progressionAnim.Length == 0 || flashAnim.Length == 0 || max == 0) {
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
        else {
            rendy.sprite = weirdInterpolation(cur);
        }

    }

    private Sprite weirdInterpolation(float progress) {

        int len = progressionAnim.Length + flashAnim.Length;

        float progMax = max * divider;
        float flashMax = max - progMax;

        // Debug.Log("0 - " + progMax + " - " + flashMax + progMax + " - " + max + " : " + cur);

        if (cur <= progMax)
        {
            return progressionAnim[(int)((cur / progMax) * ((float)progressionAnim.Length - 1))];
        }
        else {
            return flashAnim[(int)(((cur - progMax) / flashMax) * ((float)flashAnim.Length - 1))];
        }
    }

}
