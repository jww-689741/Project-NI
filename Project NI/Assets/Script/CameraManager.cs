using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player; // 추적할 플레이어 오브젝트의 위치
    public float trackingSpeed; // 카메라 추적 속도

    private Vector3 cameraDistance; // 카메라와 플레이어가 떨어진 거리
    private float timer;

    private float runningTime; // 진행시간
    private int cameraState; // 카메라 시점 상태 플래그 ( 0 : 백뷰, 1 : 탑뷰, 2 : 사이드뷰 )

    private void Awake()
    {
        cameraState = 0; // 기본 카메라 시점
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

    // 플레이어 위치에 따라 카메라 추적 기능
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

    // 카메라 전환
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
