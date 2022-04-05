using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player; // ������ �÷��̾� ������Ʈ�� ��ġ
    public float trackingSpeed; // ī�޶� ���� �ӵ�

    private Vector3 cameraDistance; // ī�޶�� �÷��̾ ������ �Ÿ�
    private float timer;

    private float runningTime; // ����ð�
    private int cameraState; // ī�޶� ���� ���� �÷��� ( 0 : ���, 1 : ž��, 2 : ���̵�� )

    private void Awake()
    {
        cameraState = 0; // �⺻ ī�޶� ����
        timer = 0;
    }
    private void FixedUpdate()
    {
        TrackingCamera();
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            LotateCamera();
        }
    }

    // �÷��̾� ��ġ�� ���� ī�޶� ���� ���
    private void TrackingCamera()
    {
        if(cameraState == 0)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x,player.position.y + 3f,player.position.z -10f), Time.smoothDeltaTime * trackingSpeed);
        }
        if (cameraState == 1)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x, player.position.y + 30f, player.position.z + 10f), Time.smoothDeltaTime * trackingSpeed);
        }
        if (cameraState == 2)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x + 30f, player.position.y, player.position.z + 20f), Time.smoothDeltaTime * trackingSpeed);
        }
    }

    // ī�޶� ��ȯ
    private void LotateCamera()
    {
        if (Random.Range(0, 3) == 0 && cameraState != 0)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x, player.position.y + 3f, player.position.z - 10f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            cameraState = 0;
            Debug.Log("SetBackview");
        }
        else if (Random.Range(0, 3) == 1 && cameraState != 1)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x, player.position.y + 30f, player.position.z + 10f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(90, 0, 0);
            cameraState = 1;
            Debug.Log("SetTopview");
        }
        else if (Random.Range(0, 3) == 2 && cameraState != 2)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(player.position.x + 30f, player.position.y, player.position.z + 20f), Time.smoothDeltaTime * trackingSpeed);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localRotation = Quaternion.Euler(0, -90, 0);
            cameraState = 2;
            Debug.Log("SetSideview");
        }
    }


}
