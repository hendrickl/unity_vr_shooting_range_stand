using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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
