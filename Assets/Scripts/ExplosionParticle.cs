using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    [SerializeField]
    public StageControl stageControl;

    [SerializeField]
    public StencilWriter stencilWriter;

    Vector3 direction;
    Vector3 adjacentPosition1;
    Vector3 adjacentPosition2;

    public Vector3 Direction
    {
        set
        {
            direction = value;
            if (Mathf.Approximately(value.x, 0f) || Mathf.Approximately(value.y, 0f))
            {
                adjacentPosition1 = Vector3.Cross(direction, Vector3.forward);
                adjacentPosition2 = Vector3.Cross(direction, Vector3.back);
            }
            else
            {
                adjacentPosition1 = new Vector3(-value.x, 0f, 0f);
                adjacentPosition1 = new Vector3(0f, -value.y, 0f);
            }
        }
    }

    void Update()
    {
        if (transform.position.x > stageControl.stageScale
    || transform.position.x < -stageControl.stageScale
    || transform.position.y > stageControl.stageScale
    || transform.position.y < -stageControl.stageScale)
        {
            Destroy(gameObject);
        }

        stencilWriter.Reveal(transform.position, StencilWriter.UpdateKind.Reveal);
        stencilWriter.Reveal(transform.position + adjacentPosition1, StencilWriter.UpdateKind.Reveal);
        stencilWriter.Reveal(transform.position + adjacentPosition2, StencilWriter.UpdateKind.Reveal);
        transform.Translate(direction);
    }
}
