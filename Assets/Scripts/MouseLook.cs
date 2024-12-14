using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // globals
    [SerializeField] [Range(100f, 300f)] private float rotationSpeed;
    [SerializeField] private Transform playerBodyTransform;
    [SerializeField] [Range(75f, 180f)] [Tooltip("Limits X rotation")] private float maxXrotation;
    private float xRotation = -25f;

    // Start is called before the first frame update
    void Start()
    {
        // cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // read mouse X and Y 
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // limits X rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxXrotation, maxXrotation);

        // apply X & Y rotations
        playerBodyTransform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(Vector3.right * xRotation);
    }
}
