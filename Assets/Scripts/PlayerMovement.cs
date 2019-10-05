using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public StencilWriter writer;

    float moveSpeed = 2f;

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
        var oldPosition = transform.position;

        transform.Translate(movement);

        var magnitude = movement.magnitude;
        for (int i = 0; i < Mathf.CeilToInt(magnitude); i++)
        {
            var lerp = Vector3.Lerp(oldPosition, transform.position, (float)i / magnitude);
            writer.Reveal(lerp, StencilWriter.UpdateKind.Reveal);
        }
    }
}
