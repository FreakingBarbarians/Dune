using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : GridBehaviour
{

    public string GetName()
    {
        return "bird";
    }

    // Use this for initialization
    void Start()
    {
        float yPos = Random.Range(1.0f, 2.0f);
        transform.Translate(new Vector3(0, yPos, 0));
        Debug.Log("Bird range: " + yPos);

        float zRot = Random.Range(-50.0f, 15.0f);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameObject.Destroy(this.gameObject);
    }
}
