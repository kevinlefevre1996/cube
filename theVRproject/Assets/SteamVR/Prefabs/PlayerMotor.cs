using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private Vector3 jump = Vector3.zero;

    [SerializeField]
    private float camRotLim = 85f;

    private float currentCamRotX = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velo)
    {
        velocity = velo;
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    public void RotateCamera(float rotX)
    {
        cameraRotationX = rotX;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (jump != Vector3.zero)
        {
            rb.AddForce(jump * Time.fixedDeltaTime, ForceMode.Acceleration);

        }
    }

    void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        if (cam != null)
        {
            //cam.transform.Rotate(-cameraRotation);
            currentCamRotX -= cameraRotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -camRotLim, camRotLim);

            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f,0f);

        }

        
    }

    public void Jump(Vector3 force)
    {
        jump = force;
    }


}
