using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoTarget : MonoBehaviour
{
    public float lifespan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;

        if (lifespan < 0)
            Destroy(gameObject);
    }
}
