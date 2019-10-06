using UnityEngine;

[ExecuteInEditMode]
public class StageControlEditor : MonoBehaviour
{
    [SerializeField]
    public StageControl stageControl;

    [SerializeField]
    public Camera stageCamera;

    [SerializeField]
    public Transform background;

    void Start()
    {
        stageCamera.orthographicSize = stageControl.stageScale;
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            stageCamera.orthographicSize = stageControl.stageScale;
            background.localScale = new Vector3(stageControl.stageScale / 5f, 1f, stageControl.stageScale / 5f);
        }
    }
}
