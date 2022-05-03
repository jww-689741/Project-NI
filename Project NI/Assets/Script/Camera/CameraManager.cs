using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static int cameraState; // 카메라 시점 상태 플래그 ( 0 : 백뷰, 1 : 탑뷰, 2 : 사이드뷰 )

    public Transform player; // 추적할 플레이어 오브젝트의 위치
    public float trackingSpeed; // 카메라 추적 속도

    private void Awake()
    {
        cameraState = 0; // 기본 카메라 시점
    }
    private void FixedUpdate()
    {
        TrackingCamera();
    }

    // 플레이어 위치에 따라 카메라 추적 기능
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

    // 카메라 전환
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
