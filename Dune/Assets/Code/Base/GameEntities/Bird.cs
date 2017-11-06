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
        float yPos = Random.Range(1.0f, 2.0f);
        transform.Translate(new Vector3(0, yPos, 0));
        Debug.Log("Bird range: " + yPos);

        float zRot = Random.Range(-50.0f, 15.0f);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
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
