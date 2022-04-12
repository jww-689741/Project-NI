using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteManager : MonoBehaviour
{
    public GameObject prefab;
    public float repeaterInterval; // 연사속도
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > repeaterInterval)
        {
            timer = 0;
            SetBullet(ObjectManager.instance.GetBullet("DirectBullet")); // 탄환 발사
        }
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 0.6f)); // 위치 지정
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화
        bullet.GetComponent<DirectBullet>().StartCoroutine("Shot",Vector3.forward); // 탄환 동작 로직 코루틴 시작
    }
}
