using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static int cameraState; // ī�޶� ���� ���� �÷��� ( 0 : ���, 1 : ž��, 2 : ���̵�� )

    public Transform player; // ������ �÷��̾� ������Ʈ�� ��ġ
    public float trackingSpeed; // ī�޶� ���� �ӵ�

    private void Awake()
    {
        cameraState = 0; // �⺻ ī�޶� ����
    }
    private void FixedUpdate()
    {
        TrackingCamera();
    }

    // �÷��̾� ��ġ�� ���� ī�޶� ���� ���
    private void TrackingCamera()
    {
        var cameraPosition = this.transform.position;
        if (cameraState == 0)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x,player.position.y + 3f,player.position.z -10f), Time.smoothDeltaTime * trackingSpeed);
        }
        if (cameraState == 1)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x, player.position.y + 30f, player.position.z + 10f), Time.smoothDeltaTime * trackingSpeed);
        }
        if (cameraState == 2)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x + 30f, player.position.y, player.position.z + 20f), Time.smoothDeltaTime * trackingSpeed);
        }
    }

    // ī�޶� ��ȯ
    public void RotateCamera()
    {
        var cameraPosition = this.transform.position;
        var cameraLocalRotation = this.transform.localRotation;
        if (Input.GetKeyDown(KeyCode.Alpha1) && cameraState != 0)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x, player.position.y + 3f, player.position.z - 10f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<BoxCollider>().center = new Vector3(0, 0, 150);
            cameraState = 0;
            Debug.Log("SetBackview");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cameraState != 1)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x, player.position.y + 30f, player.position.z + 10f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(90, 0, 0);
            GetComponent<BoxCollider>().center = new Vector3(0, 0, 30);
            cameraState = 1;
            Debug.Log("SetTopview");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && cameraState != 2)
        {
            this.transform.position = Vector3.Lerp(cameraPosition, new Vector3(player.position.x + 30f, player.position.y, player.position.z + 20f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(0, -90, 0);
            GetComponent<BoxCollider>().center = new Vector3(0, 0, 30);
            cameraState = 2;
            Debug.Log("SetSideview");
        }
    }


}
