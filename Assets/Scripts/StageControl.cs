using UnityEngine;

[ExecuteInEditMode]
public class StageControl : MonoBehaviour
{
    [SerializeField]
    public Camera stageCamera;

    [SerializeField]
    public Transform background;

    public float stageScale = 25f;

    void Start()
    {
        stageCamera.orthographicSize = stageScale;
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            stageCamera.orthographicSize = stageScale;
            background.localScale = new Vector3(stageScale / 5f, 1f, stageScale / 5f);
        }
    }
}
