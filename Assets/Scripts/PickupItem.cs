using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour { 
    Transform playerTransform;
    [SerializeField] private Item item;
    [SerializeField] private float speed = 5;
    [SerializeField] private float pickUpDistance = 1.5f;
    [SerializeField] private float magnetDistance = 3.0f;

    private void Start() {
        playerTransform = PlayerManager.Instance.transform;
    }

    private void Update() {
        if (!IsInMagnetDistance()) return;

        if (!PlayerManager.Instance._inventoryManager.HasEmptySlot()) return;
        
        MoveTowardsPlayer();

        if (!IsInPickUpDistance()) return;

        PickUp();
    }

    private void PickUp() {
        bool hasBeenPickedUp = PlayerManager.Instance._inventoryManager.AddItem(item);
        if (hasBeenPickedUp) Destroy(gameObject);
    }

    private bool IsInPickUpDistance() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= pickUpDistance;
    }

    private void MoveTowardsPlayer() {
        transform.position = Vector3.MoveTowards(
            transform.position,
            playerTransform.position,
            speed * Time.deltaTime
        );
    }

    private bool IsInMagnetDistance() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= magnetDistance;
    }
}
