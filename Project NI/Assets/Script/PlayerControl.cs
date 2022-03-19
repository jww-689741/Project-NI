using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float repeaterInterval; // 연사 속도
    
    private bool repeaterLock; // 연사 여부

    private void Start()
    {
        repeaterLock = true; // 연사 활성화
    }

    private void Update()
    {
        Control(); // 실시간 조작 감지
    }

    // 플레이어 조작
    public void Control()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 유지중일 때
        {
            StartCoroutine("Repeater");
        }
        else if (Input.GetMouseButtonUp(0)) repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
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
