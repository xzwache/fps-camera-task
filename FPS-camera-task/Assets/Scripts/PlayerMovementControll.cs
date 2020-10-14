using UnityEngine;

public class PlayerMovementControll : MovementControll 
{
    private Transform _transform;
    [SerializeField]
    private Transform _head;
    private float rotationX = 0.0f;

    private KeyCode _moveForwardKey;
    private KeyCode _moveBackKey;
    private KeyCode _moveLeftKey;
    private KeyCode _moveRightKey;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _transform = transform;
        ConfigureKeys();
    }

    private void ConfigureKeys() {
        if (_moveForwardButton1 != KeyCode.None && _moveForwardButton2 == KeyCode.None) {
            _moveForwardKey = _moveForwardButton1;
        } else if (_moveForwardButton2 != KeyCode.None && _moveForwardButton1 == KeyCode.None)
            _moveForwardKey = _moveForwardButton2;
        
        
        if (_moveBackButton1 != KeyCode.None && _moveBackButton2 == KeyCode.None) {
            _moveBackKey = _moveBackButton1;
        } else if (_moveBackButton2 != KeyCode.None && _moveBackButton1 == KeyCode.None) _moveBackKey = _moveBackButton2;
        
        if (_moveLeftButton1 != KeyCode.None && _moveLeftButton2 == KeyCode.None) {
            _moveLeftKey = _moveLeftButton1;
        } else if (_moveLeftButton2 != KeyCode.None && _moveLeftButton1 == KeyCode.None) _moveLeftKey = _moveLeftButton2; 
        
        
        
        if (_moveRightButton1 != KeyCode.None && _moveRightButton2 == KeyCode.None) {
            _moveRightKey = _moveRightButton1;
        } else if ( _moveRightButton2 != KeyCode.None && _moveRightButton1 == KeyCode.None)
            _moveRightKey = _moveRightButton2;
    }
    
    
    private void Update() {
        RotateMouse();
        Move();
    }

    private void RotateMouse() {
        float mouseX = Input.GetAxis("Mouse X") * _MouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _MouseSensetivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        
        _head.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
        _transform.Rotate(Vector3.up * mouseX);
    }

    private void Move() {
        var forwardMove = _transform.TransformDirection(Vector3.forward) * Time.deltaTime * _MovementSpeed;
        var sideMove = _transform.TransformDirection(Vector3.left) * Time.deltaTime * _MovementSpeed;
        
        if (Input.GetKey(_moveForwardKey)) {
            _transform.position += forwardMove;
        } else if (Input.GetKey(_moveBackKey)) {
            _transform.position -= forwardMove;
        }
        
        if (Input.GetKey(_moveLeftKey)) {
            _transform.position += sideMove;
        } else if (Input.GetKey(_moveRightKey)) {
            _transform.position -= sideMove;
        }

    }
}
