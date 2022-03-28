using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float repeaterInterval; // 연사속도
    public float movementSpeed; // 이동속도

    private bool repeaterLock; // 연사 여부
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
    private RaycastHit hit;
    private Vector3[] shotPoint;

    private void Start()
    {
        repeaterLock = true; // 연사 활성화
        shotPoint = new Vector3[2];
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // 메인 카메라 컴포넌트를 가져옴
    }

    private void Update()
    {
        control();
    }

    // 조준
    private void Aiming() {

        // 화면 상의 마우스 포인터 위치로 레이캐스팅
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            shotPoint[0] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }


    // 탄환 발사
    private void Shot()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 유지중일 때
        {
            StartCoroutine("Repeater");
        }
        else if (Input.GetMouseButtonUp(0)) repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
    }

    // 플레이어 이동
    private void Movement()
    {
        // 플레이어가 이동하는 방향과 속도를 삽입한 벡터 - 마우스 휠 컨트롤 삽입 필요
        float movementX = Input.GetAxis("XMove");
        float movementY = Input.GetAxis("YMove");
        float movementZ = Input.GetAxis("ZMove");
        Vector3 movementVector = new Vector3(movementX, movementY, movementZ) * movementSpeed;
        playerRigidbody.velocity = movementVector;
    } 

    // 탄환 연사 코루틴
    IEnumerator Repeater()
    {
        repeaterLock = true; // 연사 활성화
        while (repeaterLock)
        {
            SetBullet(ObjectManager.instance.GetBullet("Test")); // 탄환 발사
            yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
        }
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 0.6f)); // 위치 지정
        shotPoint[1] = new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z);
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화
        bullet.GetComponent<Bullet>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
    }

}
