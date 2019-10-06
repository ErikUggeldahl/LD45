using System.Collections;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    [SerializeField]
    public StencilWriter stencilWriter;

    [SerializeField]
    public GameObject brushPrefab;

    [SerializeField]
    public GameObject explosionPrefab;

    public float stageScale = 25f;

    void Start()
    {
        StartCoroutine(SpawnBrush());
        StartCoroutine(SpawnExplosion());
    }

    IEnumerator SpawnBrush()
    {
        while (true)
        {
            var x = Random.Range(-stageScale, stageScale);
            var y = Random.Range(-stageScale, stageScale);

            var brush = Instantiate(brushPrefab, new Vector3(x, y), Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnExplosion()
    {
        while(true)
        {
            var x = Random.Range(-stageScale, stageScale);
            var y = Random.Range(-stageScale, stageScale);

            var explosion = Instantiate(explosionPrefab, new Vector3(x, y), Quaternion.identity);
            var explosionScript = explosion.GetComponent<Explosion>();
            explosionScript.stageControl = this;
            explosionScript.stencilWriter = stencilWriter;

            yield return new WaitForSeconds(10f);
        }
    }
}
