using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float pixelation;

    // Start is called before the first frame update
    void Start()
    {
        float height = Screen.height;
        float width = Screen.width;
        float xScale = width / pixelation;
        float yScale = height / pixelation;
        Debug.Log(xScale);
        Debug.Log(yScale);

        material.SetFloat("_ScaleX", xScale);
        material.SetFloat("_ScaleY", yScale);
    }
}
