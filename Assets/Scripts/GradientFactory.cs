using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GradientFactory : MonoBehaviour
{
    [SerializeField]
    public Color[] colors;

    private void Start()
    {
        SetTexture();
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            SetTexture();
        }
    }

    void SetTexture()
    {
        var texture = new Texture2D(1, colors.Length, TextureFormat.ARGB32, false);

        for (int i = 0; i < colors.Length; i++)
        {
            texture.SetPixel(0, i, colors[i]);
        }

        texture.Apply();

        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;
    }
}
