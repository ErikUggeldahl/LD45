using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilWriter : MonoBehaviour
{
    [SerializeField]
    public Material backgroundMaterial;

    [SerializeField]
    public StageControl stageControl;

    Texture2D stencil;

    int resolution;

    public enum UpdateKind
    {
        Reveal,
        Hide
    }

    void Awake()
    {
        resolution = (int)stageControl.stageScale * 2;
    }

    struct Update
    {
        public int x, y;
    }

    List<Update> updateOn = new List<Update>(50);
    List<Update> updateOff = new List<Update>(50);

    void Start()
    {
        stencil = new Texture2D(resolution, resolution, TextureFormat.Alpha8, false);
        stencil.filterMode = FilterMode.Point;
        stencil.wrapMode = TextureWrapMode.Clamp;

        Color[] colors = new Color[stencil.width * stencil.height];
        for (int i = 0; i < stencil.width * stencil.height; i++)
        {
            colors[i] = Color.clear;
        }
        stencil.SetPixels(colors);
        stencil.Apply();
        backgroundMaterial.SetTexture("_Stencil", stencil);
    }

    Update WorldToTextureSpace(Vector2 worldPosition)
    {
        return new Update
        {
            x = resolution / 2 - Mathf.RoundToInt(worldPosition.x),
            y = resolution / 2 - Mathf.RoundToInt(worldPosition.y)
        };
    }

    public void Reveal(Vector2 position, UpdateKind reveal)
    {
        var update = WorldToTextureSpace(position);
        if (reveal == UpdateKind.Reveal) updateOn.Add(update);
        else updateOff.Add(update);

    }

    void LateUpdate()
    {
        foreach (var update in updateOn)
        {
            stencil.SetPixel(update.x, update.y, Color.white);
        }
        updateOn.Clear();

        foreach (var update in updateOff)
        {
            stencil.SetPixel(update.x, update.y, Color.clear);
        }
        updateOff.Clear();

        stencil.Apply();
        backgroundMaterial.SetTexture("_Stencil", stencil);
    }
}
