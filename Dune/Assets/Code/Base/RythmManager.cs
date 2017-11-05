using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : MonoBehaviour
{

    // NICE CODE JASMIN & ESAAC 10/10

    public static RythmManager instance;

    public ProgressBar progressy;

    List<float> stepTimes = new List<float>();
    List<float> deltaTimes = new List<float>();

    int stepTimesSizeLimit = 10;
    int deltaTimesSizeLimit = 9;

    [Range(1,1000)]
    public int maxRhythm;

    public int rythmScore;

    // Use this for initialization
    void Start()
    {
        instance = this;
        rythmScore = 0;
        Debug.Log("Rythm: " + rythmScore);
        InvokeRepeating("shrinkRythmBar", 0, 5);
        progressy.set(maxRhythm);
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
        TrySpawn();
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
                // Debug.Log(equalDelta);
                if (equalDelta)
                {
                    rythmScore = rythmScore + 1;
                }
            }
        }
        progressy.progress(rythmScore);
    }
    
    public void shrinkRythmBar()
    {
        if (rythmScore > 0)
        {
            rythmScore = rythmScore - 1;
            // Debug.Log("Rhythm: " + rythmScore);
        }
        progressy.progress(rythmScore);
    }

    public void TrySpawn() {
        if (rythmScore <= maxRhythm * 0.7f) {
            return;
        }

        float chance = UnityEngine.Random.value;
        if (chance <= ((float)rythmScore / maxRhythm)) {
            rythmScore -= maxRhythm / 3;
            rythmScore = Mathf.Max(0, rythmScore);
            progressy.progress(rythmScore);
            WormManager.instance.spawnWorm(); // SLIMY!!!
        }
    }
}

