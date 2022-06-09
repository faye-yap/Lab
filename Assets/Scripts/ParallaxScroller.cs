using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{

    public Renderer[] layers;
    public float[] speedMultiplier;
    private float previousXPosMario;
    private float previousXPosCamera;
    public Transform mario;
    public Transform mainCam;
    private float[] offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new float[layers.Length];
        for (int i = 0; i<layers.Length;i++){
            offset[i] = 0.0f;
        }
        previousXPosMario = mario.transform.position.x;
        previousXPosCamera = mainCam.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(previousXPosCamera - mainCam.transform.position.x) > 0.001f){
            for(int i = 0;i<layers.Length;i++){
                if(offset[i] > 1.0f || offset[i] < -1.0f){
                    offset[i] = 0.0f;

                }
                float newOffset = mario.transform.position.x - previousXPosMario;
                offset[i] = offset[i] + newOffset *speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(offset[i],0);
            }
        }
        previousXPosMario = mario.transform.position.x;
        previousXPosCamera = mainCam.transform.position.x;
    }
}
