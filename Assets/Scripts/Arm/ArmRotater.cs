using UnityEngine;
using UnityEngine.InputSystem;

public class ArmRotater : MonoBehaviour
{
    // left arm
    [SerializeField] SpriteRenderer bicepLeft;
    ArticulationBody bicepLeftJoint;
    [SerializeField] SpriteRenderer forearmLeft;
    ArticulationBody forearmLeftJoint;
    [SerializeField] SpriteRenderer handLeft;
    ArticulationBody handLeftJoint;
    bool moveHandLeft;
    float wristForceLeft;
    bool moveForearmLeft;
    float elbowForceLeft;
    bool moveBicepLeft;
    float shoulderForceLeft;

    // right arm
    [SerializeField] SpriteRenderer bicepRight;
    ArticulationBody bicepRightJoint;
    [SerializeField] SpriteRenderer forearmRight;
    ArticulationBody forearmRightJoint;
    [SerializeField] SpriteRenderer handRight;
    ArticulationBody handRightJoint;
    bool moveHandRight;
    float wristForceRight;
    bool moveForearmRight;
    float elbowForceRight;
    bool moveBicepRight;
    float shoulderForceRight;

    bool armsFrozen;

    // Audio fields
    [SerializeField] AudioSource armAudioSource; 
    [Tooltip("Angular speed that maps to intensity = 1.0")]
    [SerializeField] float maxAngularSpeed = 10f;
    [Tooltip("Pitch when intensity = 0")]
    [SerializeField] float minPitch = 0.5f;
    [Tooltip("Pitch when intensity = 1")]
    [SerializeField] float maxPitch = 2f;
    [Tooltip("If true, pitch will be set directly to normalized 0..1 value instead of mapping to min/max pitch")]
    [SerializeField] bool useNormalizedPitch0to1 = false;
    [Tooltip("Minimum intensity to consider as moving")]
    [SerializeField] float playThreshold = 0.01f;

    void Start()
    {
        bicepLeftJoint = bicepLeft != null ? bicepLeft.GetComponent<ArticulationBody>() : null;
        forearmLeftJoint = forearmLeft != null ? forearmLeft.GetComponent<ArticulationBody>() : null;
        handLeftJoint = handLeft != null ? handLeft.GetComponent<ArticulationBody>() : null;

        bicepRightJoint = bicepRight != null ? bicepRight.GetComponent<ArticulationBody>() : null;
        forearmRightJoint = forearmRight != null ? forearmRight.GetComponent<ArticulationBody>() : null;
        handRightJoint = handRight != null ? handRight.GetComponent<ArticulationBody>() : null;
    }

    void Update()
    {
        if (moveBicepLeft && bicepLeftJoint != null)
            bicepLeftJoint.AddForce(bicepLeft.transform.up * 500 * shoulderForceLeft);
        if (moveForearmLeft && forearmLeftJoint != null)
            forearmLeftJoint.AddForce(forearmLeft.transform.up * 100 * elbowForceLeft);
        if (moveHandLeft && handLeftJoint != null) handLeftJoint.AddForce(handLeft.transform.up * 100 * wristForceLeft);

        if (moveBicepRight && bicepRightJoint != null)
            bicepRightJoint.AddForce(bicepRight.transform.up * 500 * shoulderForceRight);
        if (moveForearmRight && forearmRightJoint != null)
            forearmRightJoint.AddForce(forearmRight.transform.up * 100 * elbowForceRight);
        if (moveHandRight && handRightJoint != null)
            handRightJoint.AddForce(handRight.transform.up * 100 * wristForceRight);

        UpdateArmAudio();
    }

    void UpdateArmAudio()
    {
        if (armAudioSource == null) return;

        float intensity = CalculateMovementIntensity(); // 0..1

        if (!armsFrozen && intensity > playThreshold)
        {
            if (!armAudioSource.isPlaying) armAudioSource.Play();

            if (useNormalizedPitch0to1)
                armAudioSource.pitch = Mathf.Clamp01(intensity);
            else
                armAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, intensity);
        }
        else
        {
            if (armAudioSource.isPlaying) armAudioSource.Stop();
        }
    }

    float CalculateMovementIntensity()
    {
        float maxSpeed = 0f;
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(bicepLeftJoint));
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(forearmLeftJoint));
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(handLeftJoint));
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(bicepRightJoint));
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(forearmRightJoint));
        maxSpeed = Mathf.Max(maxSpeed, GetAngularSpeed(handRightJoint));

        if (maxAngularSpeed <= 0f) return 0f;
        return Mathf.Clamp01(maxSpeed / maxAngularSpeed);
    }

    static float GetAngularSpeed(ArticulationBody joint)
    {
        if (joint == null) return 0f;
        // angularVelocity is a Vector3 (radians per second)
        return joint.angularVelocity.magnitude;
    }

void OnElbowLeft(InputValue value)
    {
        if (armsFrozen || value == null) return;
        moveForearmLeft = !moveForearmLeft;
        elbowForceLeft = value.Get<float>();
    }
    
    void OnShoulderLeft(InputValue value)
    {
        if (armsFrozen || value == null) return;
        moveBicepLeft = !moveBicepLeft;
        shoulderForceLeft = value.Get<float>();
    }
    
    void OnElbowRight(InputValue value)
    {
        if (armsFrozen || value == null) return;
        moveForearmRight = !moveForearmRight;
        elbowForceRight = value.Get<float>();
    }
    
    void OnShoulderRight(InputValue value)
    {
        if (armsFrozen || value == null) return;
        moveBicepRight = !moveBicepRight;
        shoulderForceRight = value.Get<float>();
    }

    public void OnStop(InputValue value)
    {
        if (value != null && value.isPressed) ToggleArmsFrozen();
    }

    public void ToggleArmsFrozen()
    {
        armsFrozen = !armsFrozen;

        if (armsFrozen)
        {
            if (bicepLeftJoint != null) bicepLeftJoint.Sleep();
            if (forearmLeftJoint != null) forearmLeftJoint.Sleep();
            if (handLeftJoint != null) handLeftJoint.Sleep();

            if (bicepRightJoint != null) bicepRightJoint.Sleep();
            if (forearmRightJoint != null) forearmRightJoint.Sleep();
            if (handRightJoint != null) handRightJoint.Sleep();
        }
        else
        {
            if (bicepLeftJoint != null) bicepLeftJoint.WakeUp();
            if (forearmLeftJoint != null) forearmLeftJoint.WakeUp();
            if (handLeftJoint != null) handLeftJoint.WakeUp();

            if (bicepRightJoint != null) bicepRightJoint.WakeUp();
            if (forearmRightJoint != null) forearmRightJoint.WakeUp();
            if (handRightJoint != null) handRightJoint.WakeUp();
        }

        // stop audio immediately when frozen
        if (armAudioSource != null && armsFrozen && armAudioSource.isPlaying) armAudioSource.Stop();
        
        //play spotlight sound effect
        SoundManager.Instance.Play("Spotlight", SoundType.SFX);
    }
}