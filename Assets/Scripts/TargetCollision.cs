using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();

        if (gameObject.CompareTag("BodyTarget"))
        {
            Debug.Log("Body Touched : " + other.name + " triggered " + gameObject.name);
        }

        if (gameObject.CompareTag("HeadTarget"))
        {
            Debug.Log("Head Touched " + other.name + " triggered " + gameObject.name);
        }
    }
}
