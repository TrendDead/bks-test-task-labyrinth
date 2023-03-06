using UnityEngine;
using System;

/// <summary>
/// Контроллер игрока
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Action EndLevel = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            EndLevel?.Invoke();
        }
    }
}
