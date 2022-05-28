using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    public Transform endLimit;
    private float offset;
    private float startX;
    private float endX;
    private float viewPortHalfWidth;

    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        viewPortHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewPortHalfWidth;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;

        if (desiredX > startX && desiredX < endX){
            this.transform.position = new Vector3(desiredX, this.transform.position.y,this.transform.position.z);
        }
        
    }
}
