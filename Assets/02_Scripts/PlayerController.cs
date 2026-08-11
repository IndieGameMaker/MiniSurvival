using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float _speed = 5.0f;
    [Range(90f, 1440f)]
    [SerializeField] private float _turnSpeed = 720.0f;

}
