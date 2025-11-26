using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ArmRotater : MonoBehaviour
{
    [SerializeField] SpriteRenderer bicep;
    ArticulationBody bicepJoint;
    [SerializeField] SpriteRenderer forearm;
    ArticulationBody forearmJoint;
    [SerializeField] SpriteRenderer hand;
    ArticulationBody handJoint;
    bool moveHand; float wristForce;
    bool moveForearm; float elbowForce;
    bool moveBicep; float shoulderForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bicepJoint = bicep.GetComponent<ArticulationBody>();
        forearmJoint = forearm.GetComponent<ArticulationBody>();
        handJoint = hand.GetComponent<ArticulationBody>();
    }

    void Update()
    {
        bool isLow = forearm.transform.localRotation.z <= 0.0f;

        bicep.flipX = isLow;
        forearm.flipX = isLow;
        hand.flipX = isLow;

        if (moveBicep) bicepJoint.AddForce(bicep.transform.right * 100 * shoulderForce);
        if (moveForearm) forearmJoint.AddForce(forearm.transform.right * 100 * elbowForce);
        if (moveHand) handJoint.AddForce(hand.transform.right * 100 * wristForce);
    }

    void OnWrist(InputValue value)
    {
        moveHand = !moveHand;
        wristForce = value.Get<float>();
    }

    void OnElbow(InputValue value)
    {
        moveForearm = !moveForearm;
        elbowForce = value.Get<float>();
    }

    void OnShoulder(InputValue value)
    {
        moveBicep = !moveBicep;
        shoulderForce = value.Get<float>();
    }
}
