using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : MonoBehaviour
{

    public static RythmManager instance;

    List<float> stepTimes = new List<float>();
    List<float> deltaTimes = new List<float>();

    int stepTimesSizeLimit = 10;
    int deltaTimesSizeLimit = 9;

    public int rythmScore;


    // Use this for initialization
    void Start()
    {
        instance = this;
        rythmScore = 0;
        Debug.Log("Rythm: " + rythmScore);
        InvokeRepeating("shrinkRythmBar", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void step()
    {
        stepTimes.Add(Time.realtimeSinceStartup);
        if (stepTimes.Count > stepTimesSizeLimit)
        {
            stepTimes.RemoveAt(0);
        }

        for (int i = 0; i < stepTimes.Count; i++)
        {
            Debug.Log(stepTimes[i]);
        }

        //check if rythm bar should grow
        calcDeltaTime();
        growRythmBar();
    }

    public void calcDeltaTime()
    {
        if (stepTimes.Count > 1)
        {
            float delta = (stepTimes[stepTimes.Count - 1]) - (stepTimes[stepTimes.Count - 2]);
            //Debug.Log("Delta");
            //Debug.Log(delta);
            deltaTimes.Add(delta);
            if (deltaTimes.Count > deltaTimesSizeLimit)
            {
                deltaTimes.RemoveAt(0);
            }
        }
    }

    public bool isEqualDelta(float deltaTime, float lastDelta)
    {
        if (Mathf.Abs(lastDelta - deltaTime) < 0.5f)
        {
            return true;
        }
        return false;
    }

    public void growRythmBar()
    {
        if (deltaTimes.Count > 1)
        {
            float lastDelta = deltaTimes[deltaTimes.Count - 1];
            for (int i = 0; i < (deltaTimes.Count - 1); i++)
            {
                bool equalDelta = isEqualDelta(deltaTimes[i], lastDelta);
                Debug.Log(equalDelta);
                if (equalDelta)
                {
                    rythmScore = rythmScore + 1;
                }
            }
        }
    }

    public void shrinkRythmBar()
    {
        if (rythmScore > 0)
        {
            rythmScore = rythmScore - 1;
            Debug.Log("Rhythm: " + rythmScore);
        }
    }
}

