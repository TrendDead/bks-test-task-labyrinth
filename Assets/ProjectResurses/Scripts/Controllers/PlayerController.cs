using UnityEngine;
using System;

/// <summary>
/// ���������� ������
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Action EndLevel = delegate { };

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ������ ����������� ���� ������ ����
    /// </summary>
    private void Update()
    {
        if(_rigidbody.velocity == Vector3.zero)
        {
            _rigidbody.velocity = Vector3.up * 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            EndLevel?.Invoke();
        }
    }
}
