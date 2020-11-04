using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private int _speed;
    private int _health;
    private string _fullName;

    public int Speed {
        get => _speed;
        set {
            if (_speed != value) {
                _speed = value;   
                Debug.Log("New player Speed value: " + value);
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
}
