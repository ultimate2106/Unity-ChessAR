﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZ : MonoBehaviour
{
    float _zAxisStart;
    // Start is called before the first frame update
    void Start()
    {
        _zAxisStart = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = transform.localPosition.x;
        float currentY = transform.localPosition.y;
        transform.localPosition = new Vector3(currentX, currentY, _zAxisStart);
    }
}
