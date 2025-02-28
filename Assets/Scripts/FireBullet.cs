using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 20;

    XRGrabInteractable grabInteractable;
    UnityAction<SelectEnterEventArgs> function;
    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.activated.AddListener(Shoot);
    }


    private void OnDisable()
    {
        grabInteractable.activated.RemoveListener(Shoot);
    }


    private void Shoot(ActivateEventArgs arg)
    {
        Debug.Log("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        
        if(bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = spawnPoint.forward * bulletSpeed;
        }

        Destroy(bullet, 3f);
    }
}
