using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public bool Switch;

    private Vector3 _currentTarget;
    [SerializeField] private Transform _waypointA;
    [SerializeField] private Transform _waypointB;

    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _fixedTarget;
    [SerializeField] private Transform _movingTargetPosition;
    [SerializeField] private Transform _fixedTargetTowardPlayerPosition;
    [SerializeField] private Transform _fixedTargetTowardBgPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedOnSwitch;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _impactSound;

    private void Start()
    {
        _currentTarget = _waypointA.transform.position;
    }

    private void Update()
    {
        if (!_movingTargetPosition)
        {
            throw new UnityException();
        }

        MoveMovingTarget();
        MoveTest();
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayAudioClip(_impactSound);
    }

    public void MoveMovingTarget()
    {
        iTween.MoveTo(_movingTarget, iTween.Hash("position", _movingTargetPosition, "speed", _speed, "loopType", "pingPong", "easetype", "linear"));
    }

    public void MoveFixedTargetTowardPlayer()
    {
        if (Switch == false)
        {
            print("Target moves toward player");
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardPlayerPosition, "speed", _speedOnSwitch, "easetype", "linear"));
            Switch = true;
            print("Switch = " + Switch);
        }
    }

    public void MoveFixedTargetTowardBg()
    {
        if (Switch)
        {
            print("Target moves toward Bg");
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardBgPosition, "speed", _speedOnSwitch, "easetype", "linear"));
            Switch = false;
            print("Switch = " + Switch);
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (!_audioSource)
        {
            throw new UnityException();
        }

        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }

    private void MoveTest()
    {
        // Calculer la distance entre le gameobject et le point cible
        float _distance = Vector3.Distance(transform.position, _currentTarget);

        // Si la distance est inférieure à la vitesse de déplacement, le gameobject atteint le point cible
        if (_distance <= _speed * Time.deltaTime)
        {
            transform.position = _currentTarget;

            // Changement de cible
            if (_currentTarget == _waypointB.position)
            {
                _currentTarget = _waypointA.position;
            }
            else
            {
                _currentTarget = _waypointB.position;
            }
        }
        else
        {
            // Déplacement du gameobject vers la cible
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }
    }
}
