using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

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
        /*
        bool isLow = forearm.transform.localRotation.z >= 0.0f;

        bicep.flipY = isLow;
        forearm.flipY = isLow;
        hand.flipY = isLow;

        // flip second arm similarly if needed
        bicep2.flipY = isLow;
        forearm2.flipY = isLow;
        hand2.flipY = isLow;
        */
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
    }

    /*void OnWristLeft(InputValue value)
    {
        moveHandLeft = !moveHandLeft;
        wristForceLeft = value.Get<float>();
    }*/

    void OnElbowLeft(InputValue value)
    {
        moveForearmLeft = !moveForearmLeft;
        elbowForceLeft = value.Get<float>();
    }

    void OnShoulderLeft(InputValue value)
    {
        moveBicepLeft = !moveBicepLeft;
        shoulderForceLeft = value.Get<float>();
    }

    /*void OnWristRight(InputValue value)
    {
        moveHandRight = !moveHandRight;
        wristForceRight = value.Get<float>();
    }*/

    void OnElbowRight(InputValue value)
    {
        moveForearmRight = !moveForearmRight;
        elbowForceRight = value.Get<float>();
    }

    void OnShoulderRight(InputValue value)
    {
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
    }
}