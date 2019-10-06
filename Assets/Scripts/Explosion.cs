using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticle;

    [SerializeField]
    public StageControl stageControl;

    [SerializeField]
    public StencilWriter stencilWriter;

    void OnTriggerEnter2D(Collider2D col)
    {
        SpawnParticle(Vector3.up);
        SpawnParticle(Vector3.down);
        SpawnParticle(Vector3.left);
        SpawnParticle(Vector3.right);
        SpawnParticle(new Vector3(1f, 1f, 0));
        SpawnParticle(new Vector3(1f, -1f, 0));
        SpawnParticle(new Vector3(-1f, 1f, 0));
        SpawnParticle(new Vector3(-1f, -1f, 0));
        Destroy(gameObject);
    }

    void SpawnParticle(Vector3 direction)
    {
        var particle = Instantiate(explosionParticle, transform.position + direction, Quaternion.identity);
        var particleScript = particle.GetComponent<ExplosionParticle>();
        particleScript.Direction = direction;
        particleScript.stageControl = stageControl;
        particleScript.stencilWriter = stencilWriter;
    }
}
