using System.Collections;
using UnityEngine;

public class Zoom : MonoBehaviour {
    
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomTreshold;
    [SerializeField] private float _zoomSpeed = 5f;
    
    private float _defaultZoomValue;

    private bool zoomed = false;
    
    private void Start() {
        if (_zoomTreshold == 0.0f) _zoomTreshold = _camera.fieldOfView / 2.0f;
        _defaultZoomValue = _camera.fieldOfView;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) zoomed = !zoomed;

        if (zoomed) {
            MakeZoom(_camera.fieldOfView, _zoomTreshold);
        } else MakeZoom(_camera.fieldOfView, _defaultZoomValue);
    }

    private void MakeZoom(float from, float to) {
        _camera.fieldOfView = Mathf.Lerp(from, to, Time.deltaTime * _zoomSpeed);
    }
}
