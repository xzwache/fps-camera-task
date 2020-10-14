using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControll : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] internal KeyCode _moveLeftButton1 = KeyCode.A;
    [SerializeField] internal KeyCode _moveLeftButton2 = KeyCode.LeftArrow;
    
    [SerializeField] internal KeyCode _moveRightButton1 = KeyCode.D;
    [SerializeField] internal KeyCode _moveRightButton2 = KeyCode.RightArrow;
    
    [SerializeField] internal KeyCode _moveForwardButton1 = KeyCode.W;
    [SerializeField] internal KeyCode _moveForwardButton2 = KeyCode.UpArrow;
    
    [SerializeField] internal KeyCode _moveBackButton1 = KeyCode.S;
    [SerializeField] internal KeyCode _moveBackButton2 = KeyCode.DownArrow;

    [Space] [Header("Movement configuration")] 
    [SerializeField] internal float _MovementSpeed = 1.0f;

    [Space] [SerializeField] internal float _MouseSensetivity = 10f;
}
