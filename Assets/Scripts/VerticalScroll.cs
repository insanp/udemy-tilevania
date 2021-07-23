using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    public float amplitude = 2f;
    public float frequency = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yScroll = Mathf.Sin(Time.time * 0.5f) * amplitude;
        Debug.Log(yScroll);
        transform.position = new Vector2(0f, yScroll);
    }
}
