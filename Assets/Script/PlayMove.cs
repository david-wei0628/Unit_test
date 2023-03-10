using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{
    public GameObject PlayerMove;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 20, 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            this.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 20);
        }

        if (Cursor.lockState.ToString() == "Locked")
        {
            PlayRat();
        }

    }

    void PlayRat()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        if (Input.GetAxis("Mouse X") != 0)
        {
            this.transform.Rotate(Vector3.up * mouseX);
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 20f); // 讓頭部旋轉在90度
        }
    }
}
