using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CarController : MonoBehaviour
{
    // Tuning values for power, steering, and braking
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float brakeForce = 3000f;

    // Wheel colliders for physics and visual wheel transforms
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    public Transform frontLeftTransform, frontRightTransform, rearLeftTransform, rearRightTransform;

    // Input values to track player commands
    private float steerInput = 0f;
    private float throttleInput = 0f;
    private bool isBraking = false;
    private bool isDrifting = false;
    private bool wasDrifting = false;
    private bool isSpinning = false;
    private bool needsRespawn = false;

    // Audio clips and source for driving and braking sounds
    public AudioClip driveSound;
    public AudioClip brakeSound;
    private AudioSource audioSource;
    private bool isDrivingSoundPlaying = false;
    private bool isBrakingSoundPlaying = false;

    // Reference to new Input System action map
    private CarControls controls;

    private void Awake()
    {
        controls = new CarControls();

        // Get or add AudioSource component for playing sounds
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Setup input callbacks for steering, throttle, brake, drift, and attack
        controls.Driving.Steer.performed += ctx => steerInput = ctx.ReadValue<float>();
        controls.Driving.Steer.canceled += _ => steerInput = 0f;

        controls.Driving.Throttle.performed += ctx => throttleInput = ctx.ReadValue<float>();
        controls.Driving.Throttle.canceled += _ => throttleInput = 0f;

        controls.Driving.Brake.performed += _ => isBraking = true;
        controls.Driving.Brake.canceled += _ => isBraking = false;

        controls.Driving.Drift.performed += _ => isDrifting = true;
        controls.Driving.Drift.canceled += _ => isDrifting = false;

        controls.Driving.Attack.performed += _ => SpinAttack();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void FixedUpdate()
    {

        ApplySteering();
        ApplyMotor();
        ApplyBrakes();
        ApplyDrift();
        UpdateWheelTransforms();

        HandleDriveSound();
        HandleBrakeSound();
    }

    // Applies torque to rear wheels based on throttle input
    void ApplyMotor()
    {
        rearLeftWheel.motorTorque = throttleInput * maxMotorTorque;
        rearRightWheel.motorTorque = throttleInput * maxMotorTorque;
    }

    // Applies steering angle to front wheels based on steering input
    void ApplySteering()
    {
        float steer = steerInput * maxSteeringAngle;
        frontLeftWheel.steerAngle = steer;
        frontRightWheel.steerAngle = steer;
    }

    // Applies braking torque to all wheels if braking
    void ApplyBrakes()
    {
        float brake = isBraking ? brakeForce : 0f;
        frontLeftWheel.brakeTorque = brake;
        frontRightWheel.brakeTorque = brake;
        rearLeftWheel.brakeTorque = brake;
        rearRightWheel.brakeTorque = brake;
    }

    // Adjusts wheel friction to simulate drifting effect
    void ApplyDrift()
    {
        if (isDrifting && !wasDrifting)
        {
            SetFriction(0.01f); // Low friction for drifting
            wasDrifting = true;
        }
        else if (!isDrifting && wasDrifting)
        {
            SetFriction(1.0f); // Reset friction
            wasDrifting = false;
        }
    }

    // Sets sideways friction stiffness for all wheels
    void SetFriction(float stiffness)
    {
        SetWheelFriction(frontLeftWheel, stiffness);
        SetWheelFriction(frontRightWheel, stiffness);
        SetWheelFriction(rearLeftWheel, stiffness);
        SetWheelFriction(rearRightWheel, stiffness);
    }

    // Helper to set friction on a single wheel
    void SetWheelFriction(WheelCollider wheel, float stiffness)
    {
        WheelFrictionCurve friction = wheel.sidewaysFriction;
        friction.stiffness = stiffness;
        wheel.sidewaysFriction = friction;
    }

    // Updates the visual position and rotation of the wheel meshes
    void UpdateWheelTransforms()
    {
        UpdateWheel(frontLeftWheel, frontLeftTransform);
        UpdateWheel(frontRightWheel, frontRightTransform);
        UpdateWheel(rearLeftWheel, rearLeftTransform);
        UpdateWheel(rearRightWheel, rearRightTransform);
    }

    // Gets current world position and rotation from wheel collider and applies it to the mesh
    void UpdateWheel(WheelCollider col, Transform trans)
    {
        col.GetWorldPose(out Vector3 pos, out Quaternion rot);
        trans.position = pos;
        trans.rotation = rot;
    }

    // Starts the spin attack animation coroutine if not already spinning
    void SpinAttack()
    {
        if (!isSpinning)
        {
            StartCoroutine(Spin360());
        }
    }

    // Coroutine to smoothly spin the car 360 degrees over 0.5 seconds
    IEnumerator Spin360()
    {
        isSpinning = true;

        float spinDuration = 0.5f;
        float elapsed = 0f;
        float startY = transform.eulerAngles.y;
        float endY = startY + 360f;

        while (elapsed < spinDuration)
        {
            float t = elapsed / spinDuration;
            float currentY = Mathf.Lerp(startY, endY, t);
            Vector3 currentEuler = transform.eulerAngles;
            transform.eulerAngles = new Vector3(currentEuler.x, currentY, currentEuler.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endY % 360f, transform.eulerAngles.z);
        isSpinning = false;
    }

    // Handles playing or stopping the driving sound based on throttle input
    private void HandleDriveSound()
    {
        if (Mathf.Abs(throttleInput) > 0.01f)
        {
            if (!isDrivingSoundPlaying)
            {
                audioSource.clip = driveSound;
                audioSource.loop = true;
                audioSource.Play();
                isDrivingSoundPlaying = true;
            }
        }
        else
        {
            if (isDrivingSoundPlaying)
            {
                audioSource.Stop();
                isDrivingSoundPlaying = false;
            }
        }
    }

    // Plays brake sound when braking starts
    private void HandleBrakeSound()
    {
        if (isBraking && !isBrakingSoundPlaying)
        {
            audioSource.PlayOneShot(brakeSound);
            isBrakingSoundPlaying = true;
        }
        else if (!isBraking)
        {
            isBrakingSoundPlaying = false;
        }
    }
}