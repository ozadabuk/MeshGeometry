/* Copyrighted by Oguzcan Adabuk Chicago, IL, 2018*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float moveSpeed = 10f;
    public float rotateSpeed = 50f;

    public Transform camParent;
    public Transform camTransfrom;

    void Start () {
		
	}
    
    void Update () {

        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse ScrollWheel") * moveSpeed * 30 * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.position += moveX * camTransfrom.right;
        transform.position += moveY * camTransfrom.up;
        transform.position += moveZ * camTransfrom.forward;

        float rotateY = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float rotateX = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        camTransfrom.Rotate(-rotateX, 0, 0);
        camParent.Rotate(0, rotateY, 0);
	}
}
