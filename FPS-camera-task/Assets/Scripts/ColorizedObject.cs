using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class ColorizedObject : MonoBehaviour {
    private Color _defaultColor;
    private MeshRenderer _renderer;

    public Color DefaultColor {
        get => _defaultColor;
    }

    public void SetColor(Color color) {
        _renderer.sharedMaterial.color = color;
    }

    protected void Awake() {
        OnStart();
    }

    protected virtual void OnStart() {
        Reset();
    }

    public void Reset() {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.sharedMaterial.color;
    }
}
