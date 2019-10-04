using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 1f;

    void Start()
    {
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        if (x > 0f) x = 1f;
        else if (x < 0f) x = -1f;
        else x = 0f;
        var y = Input.GetAxis("Vertical");
        if (y > 0f) y = 1f;
        else if (y < 0f) y = -1f;
        else y = 0f;

        var movement = new Vector3(x, y, 0f).normalized * moveSpeed;

        transform.Translate(movement);
    }
}
