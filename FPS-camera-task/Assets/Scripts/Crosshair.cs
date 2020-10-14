using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
    
    [SerializeField] private Camera _camera;
    [SerializeField] private MeshRenderer _crosshairRenderer;
    private RenderTexture _renderTexture;

    private void Start() {
        _renderTexture = new RenderTexture(512,512, 16);

        _camera.targetTexture = _renderTexture;

        _crosshairRenderer.material.mainTexture = _renderTexture;
    }
}
