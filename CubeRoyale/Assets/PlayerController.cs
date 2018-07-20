using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float LookSenitivity = 30f;

    [SerializeField]
    private float jumpForce = 1000f;

    [Header("String settings")]

    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    void Update()
    {
        if (PauseMenu.isOn)
        {
            return;
        }




        //calculate movement vector 
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHor = transform.right * xMov; //(1, 0, 0)
        Vector3 moveVer = transform.forward * zMov; //(0, 0, 1)

        Vector3 velocity = (moveHor + moveVer).normalized * speed;

        motor.Move(velocity);

        //calculate rotation vector
        //turning
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rot = new Vector3(0f, yRot, 0f) * LookSenitivity;
        motor.Rotate(rot);

        //UpAndDown

        float xRot = Input.GetAxisRaw("Mouse Y");
        float camRotX = xRot * LookSenitivity;
        motor.RotateCamera(camRotX);


        //jump

        Vector3 force = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            force = Vector3.up * jumpForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        motor.Jump(force);

    }

    private void  SetJointSettings(float jSpring)
    {
        joint.yDrive = new JointDrive
        {

            positionSpring = jointSpring,
            maximumForce = jointMaxForce

        };
    }
}
