using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementControll))]
public class Player : MonoBehaviour {
    
    private int _speed;
    private int _health;
    private string _fullName;

    private MovementControll _movementControll = default;

    public int Speed {
        get => _speed;
        set {
            if (_speed != value) {
                _speed = value;   
                Debug.Log("New player Speed value: " + value);
                _movementControll.SetMovementSpeed((float)value);
            }
        }
    }

    public int Health {
        get => _health;
        set {
            if (_health != value) {
                _health = value;
                Debug.Log("New player Health value: " + value);   
            }
        }
    }

    public string FullName {
        get => _fullName;
        set {
            if (_fullName != value && string.IsNullOrEmpty(value) == false) {
                _fullName = value;
                Debug.Log("New player Full Name: " + value);   
            }
        }
    }

    private void Start() {
        _movementControll = GetComponent<PlayerMovementControll>();
    }
    
    private void Reset() {
        _movementControll = GetComponent<PlayerMovementControll>();
    }
}
