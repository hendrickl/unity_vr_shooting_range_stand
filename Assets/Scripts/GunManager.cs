using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunManager : MonoBehaviour
{
    private int _munitionStock = 10;
    [SerializeField] private TMP_Text _munitionsText;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] float _bulletForce = 20f;
    [SerializeField] InputActionReference _shootAction;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipMunition0;

    void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += ShootBullet;
    }

    void ShootBullet(InputAction.CallbackContext obj)
    {
        if (_munitionStock != 0)
        {
            GameObject bulletToSpawn = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bulletToSpawn.GetComponent<Rigidbody>();

            _audioSource.Play();

            bulletRigidbody.AddForce(_bulletSpawnPosition.forward * _bulletForce, ForceMode.Impulse);

            _munitionStock--;
            Debug.Log("Munitions : " + _munitionStock);

            // TODO : Make a pool
        }
        else
        {
            _audioSource.clip = _audioClipMunition0;
            _audioSource.Play();
        }

        _munitionsText.text = _munitionStock.ToString();
    }
}