using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] float _bulletForce = 20f;
    [SerializeField] InputActionReference _shootAction;
    [SerializeField] AudioSource _audioSource;

    void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += ShootBullet;

    }

    void ShootBullet(InputAction.CallbackContext obj)
    {
        Debug.Log("Fired !");

        GameObject bulletToSpawn = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletToSpawn.GetComponent<Rigidbody>();
        _audioSource.Play();
        bulletRigidbody.AddForce(_bulletSpawnPosition.forward * _bulletForce, ForceMode.Impulse);

        // TODO : Make a pool
    }
}