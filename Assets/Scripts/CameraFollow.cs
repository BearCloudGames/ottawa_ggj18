using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float horizontalBound = 5;
    public float verticalBound = 2;
    public float cameraSpeed = 10;

    private void Update()
    {
        if(Mathf.Abs(target.position.x - transform.position.x) >= horizontalBound || 
            Mathf.Abs(target.position.y - transform.position.y) >= verticalBound)
        {
            Vector3 newPosition = target.position;
            newPosition.z = -10;
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed * Time.deltaTime);
        }
    }
}
