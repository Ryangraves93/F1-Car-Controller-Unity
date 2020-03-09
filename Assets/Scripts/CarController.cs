using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle; 

    // Wheel collider references on each wheel
    public WheelCollider frontLeftWheel, frontRightWheel;
    public WheelCollider rearLeftWheel, rearRightWheel;
    // Transform of each wheel for rotation 
    public Transform frontLeftWheelT, frontRightWheelT;
    public Transform rearLeftWheelT, rearRightWheelT;

    public float maxSteerAngle = 30; //Steer radius 
    public float motorForce = 50; //Vehicle speed

    public void GetInput()//Deals with input through Unity's input
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }
    private void Steer () //Calculates steer angle for front two wheels
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontRightWheel.steerAngle = m_steeringAngle;
        frontLeftWheel.steerAngle = m_steeringAngle;
    }
    private void Accelerate () //Takes the torque from the wheel collider component and multiples by our motorForce variable
    {
        frontRightWheel.motorTorque = m_verticalInput * motorForce;
        frontLeftWheel.motorTorque = m_verticalInput * motorForce;
    }
    private void UpdateWheelPoses () //Updating wheel rotation
    {
        UpdateWheelPose(frontLeftWheel, frontLeftWheelT);
        UpdateWheelPose(frontRightWheel, frontRightWheelT);
        UpdateWheelPose(rearRightWheel, rearRightWheelT);
        UpdateWheelPose(rearLeftWheel, rearLeftWheelT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform) //updates the wheel rotation with the collider 
                                                                                //rotation to simiulate wheel rotation
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
