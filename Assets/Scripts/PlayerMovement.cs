using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public StencilWriter writer;

    float moveSpeed = 1f;

    void Update()
    {
        var children = GetComponentsInChildren<Transform>();
        var oldPositions = new Vector3[children.Length];

        // Store all old positions
        for (int i = 0; i < children.Length; i++)
        {
            oldPositions[i] = children[i].position;
        }

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var movement = new Vector3(x, y, 0f).normalized * moveSpeed;

        transform.Translate(movement);

        var magnitude = movement.magnitude;

        // Paint all new positions, lerping as needed
        for (int i = 0; i < children.Length; i++)
        {
            for (int j = 0; j < Mathf.CeilToInt(magnitude); j++)
            {
                var lerp = Vector3.Lerp(oldPositions[i], children[i].position, (float)j / magnitude);
                writer.Reveal(lerp, StencilWriter.UpdateKind.Reveal);
            }
        }
    }
}
