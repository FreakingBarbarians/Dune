using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : GridBehaviour
{

    [Range(0, 1)]
    public float tickRate;

    private float timer;

    public string GetName()
    {
        return "bird";
    }

    // Use this for initialization
    void Start()
    {
        timer = tickRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            tick();
        }
    }

    private void tick()
    {
        timer = tickRate;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameObject.Destroy(this.gameObject);
    }
}
