using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
