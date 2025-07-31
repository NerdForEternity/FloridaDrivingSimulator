using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CarResetter : MonoBehaviour
{
    [SerializeField] private Transform resetPoint;
    private Rigidbody rb;
    private CarControls controls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new CarControls();
        controls.Driving.Reset.performed += _ => ResetCar();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        // Still support keyboard R
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetCar();
        }
    }

    private void ResetCar()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;
        StartCoroutine(ReenablePhysics());
    }

    private IEnumerator ReenablePhysics()
    {
        yield return null;
        rb.isKinematic = false;
    }
}

