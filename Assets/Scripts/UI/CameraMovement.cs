using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSpeed = 40f;
    public float camBorder = 30f;

    // Define terrain boundaries
    private float minX = -20.57537f;
    private float maxX = 54.3f;
    private float minZ = -37.51917f;
    private float maxZ = 35.56809f;

    void Update()
    {
        Vector3 pos = transform.position;

        if ((Input.GetKey("up") || Input.mousePosition.y >= Screen.height - camBorder) && pos.x > minX)
        {
            transform.Translate(Vector3.left * camSpeed * Time.deltaTime, Space.World);
        }

        if ((Input.GetKey("down") || Input.mousePosition.y <= camBorder) && pos.x < maxX)
        {
            transform.Translate(Vector3.right * camSpeed * Time.deltaTime, Space.World);
        }

        if ((Input.GetKey("left") || Input.mousePosition.x <= camBorder) && pos.z > minZ)
        {
            transform.Translate(Vector3.back * camSpeed * Time.deltaTime, Space.World);
        }

        if ((Input.GetKey("right") || Input.mousePosition.x >= Screen.width - camBorder) && pos.z < maxZ)
        {
            transform.Translate(Vector3.forward * camSpeed * Time.deltaTime, Space.World);
        }

        // Clamp the camera's position to stay within the terrain boundaries
        pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
