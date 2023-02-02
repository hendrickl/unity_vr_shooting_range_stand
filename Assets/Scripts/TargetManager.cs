using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _speed;

    void Update()
    {
        MoveTo();
    }

    private void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", _targetPosition, "speed", _speed, "loopType", "pingPong", "easetype", "linear"));
    }
}
