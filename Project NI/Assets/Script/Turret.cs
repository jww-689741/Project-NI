using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float repeaterInterval; // 연사속도
    private bool repeaterLock; // 연사 여부
    public float degree; //회전 각도
    private delegate void Control();
    Control control;
    void Start()
    {
        repeaterLock = true;
        control = Shot;
    }

    // Update is called once per frame
    void Update()
    {
        control();
        transform.Rotate(Vector3.up * Time.deltaTime * degree);
    }
    private void Shot()
    {
       /* if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 유지중일 때
        {
            StartCoroutine("Repeater1");
        }
        else if (Input.GetMouseButtonUp(1)) repeaterLock = false; // 마우스 우클릭에서 손을 뗄때*/
        StartCoroutine("Repeater");
    }

    IEnumerator Repeater()
    {
        repeaterLock = true; // 연사 활성화
        while (repeaterLock)
        {
            for(int i= 0; i<4;i++)
            {
                SetBullet(ObjectManager.instance.GetBullet("Test"),i); // 탄환 발사
                //yield return new WaitForSeconds(repeaterInterval);
            }
            yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
        }
    }
    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet,int direction)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환
        if (direction == 0)  // 정면 발사
        {
            bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 1.5f)); // 위치 지정
        }
        else if (direction == 1) //좌측 발사
        {
            bullet.transform.position = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, (this.transform.position.z)); // 위치 지정
        }
        else if (direction == 2) //우측 발사
        {
            bullet.transform.position = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, (this.transform.position.z)); // 위치 지정
        }
        else if (direction == 3) //후면 발사
        {
            bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // 위치 지정
        }
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화
        bullet.GetComponent<Bullet4>().StartCoroutine("Shot",direction); // 탄환 동작 로직 코루틴 시작
    }
    
}