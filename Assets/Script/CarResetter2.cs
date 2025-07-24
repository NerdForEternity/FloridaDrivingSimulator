using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CarResetter : MonoBehaviour
{
    [SerializeField] private Transform resetPoint;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetCar();
        }
    }

    private void ResetCar()
    {
        // Stop all motion
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Temporarily disable physics
        rb.isKinematic = true;

        // Move and rotate to reset point
        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;

        // Reactivate physics on next frame
        StartCoroutine(ReenablePhysics());
    }

    private IEnumerator ReenablePhysics()
    {
        yield return null; // wait 1 frame
        rb.isKinematic = false;
    }
    }