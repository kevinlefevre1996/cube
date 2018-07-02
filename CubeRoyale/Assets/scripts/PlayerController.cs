using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float LookSenitivity = 3f;

    private PlayerMotor motor;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
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
        Vector3 camRot = new Vector3(xRot, 0f, 0f) * LookSenitivity;
        motor.RotateCamera(camRot);

    }
}
