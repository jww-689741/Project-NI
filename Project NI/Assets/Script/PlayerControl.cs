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

    private void Start()
    {
        repeaterLock = true; // 연사 활성화
        control = Shot;
        control += Movement;
    }

    private void Update()
    {
        control();
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
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime,
            0);
        this.GetComponent<Rigidbody>().AddForce(movementVector,ForceMode.VelocityChange);
    } 

    // 탄환 연사 코루틴
    IEnumerator Repeater()
    {
        repeaterLock = true; // 연사 활성화
        while (repeaterLock)
        {
            SetBulletField(ObjectManager.instance.GetBullet("Test")); // 탄환 발사
            yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
        }
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화
    void SetBulletField(GameObject bullet)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        bullet.transform.position = this.transform.position; // 위치 지정
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화
        bullet.GetComponent<Bullet>().StartCoroutine("Shot"); // 탄환 동작 로직 코루틴 시작
    }

}
