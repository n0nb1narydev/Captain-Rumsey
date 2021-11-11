using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;

    public Tilemap theMap;
    private Vector3 bottomLeft;
    private Vector3 topRight;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        // target = PlayerController.instance.transform;
        target = FindObjectOfType<PlatformerController>().transform;
        halfHeight = Camera.main.orthographicSize; // camera height
        halfWidth = halfHeight * Camera.main.aspect; //aspect ratio 9 * 16

        //keeps camera inside bounds
        bottomLeft = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRight = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        
    }


    void LateUpdate()
    {
        //sets camera to player pos
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //keeps camera inside bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x), Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y), transform.position.z);
    }
}
