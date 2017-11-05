using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool CanGoLeft = true;
    public bool CanGoRight = true;

    public GameObject LeftSentinel;
    public GameObject RightSentinel;

    public void Update()
    {
        Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(gameObject.GetComponent<Camera>());
        if (GeometryUtility.TestPlanesAABB(cameraPlanes, LeftSentinel.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("LEFT SENTINEL");
            CanGoLeft = false;
        }
        else {
            CanGoLeft = true;
        }

        if (GeometryUtility.TestPlanesAABB(cameraPlanes, RightSentinel.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Right SENTINEL");
            CanGoRight = false;
        }
        else {
            CanGoRight = true;
        }

        Vector3 deltapos = Fremen.instance.transform.position - transform.position;

        if (!CanGoLeft && deltapos.x < 0) {
            return;
        }

        if (!CanGoRight && deltapos.x > 0) {
            return;
        }

        transform.position = new Vector3(Fremen.instance.transform.position.x, Fremen.instance.transform.position.y, transform.position.z);



    }
}
