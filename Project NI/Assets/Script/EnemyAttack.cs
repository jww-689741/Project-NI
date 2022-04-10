using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float repeaterInterval;
    public float enemyShotSpeed;

    private Vector3[] shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        shotPoint = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Repeater", "DirectBullet");
    }

    IEnumerator Repeater(string name)
    {
        if (this.gameObject.name == "DirectBullet")
        {
            while (true)
            {
                SetBullet(ObjectManager.instance.GetBullet(name), name); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        shotPoint[0] = player.position; // 목표지점, 플레이어
        shotPoint[1] = bulletPosition; // 시작지점, 적

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // 위치 지정
        bullet.transform.rotation = this.transform.rotation; // 회전각 지정
        bullet.SetActive(true); // 활성화

        if (name == "DirectBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //자식 오브젝트 부모와 동일한 위치로 이동
            }
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
    }

    // 폐기 예정
    private Vector3 CalculatingExtendedDistance(Vector3 startPoint, Vector3 endPoint, float distance)
    {
        float posZ = Mathf.Sqrt(Mathf.Pow(startPoint.z - endPoint.z, 2) + Mathf.Pow(startPoint.y - endPoint.y, 2));
        float posX = Mathf.Abs(startPoint.x - endPoint.x); // 두 점의 x 차이
        float posY = (distance * Mathf.Abs(startPoint.y - endPoint.y)) / Mathf.Abs(startPoint.z - endPoint.z);

        float targetX = (posX * distance) / posZ; // x, z값으로 계산
        float targetY = (posY * distance) / posZ; // y, z값으로 계산
        float targetZ = distance; // 매개변수로 받은 거리

        if(startPoint.x - endPoint.x < 0)
        {
            targetX *= -1;
        }
        else if (startPoint.y - endPoint.y < 0)
        {
            targetY *= -1;
        }
        else if (startPoint.z - endPoint.z < 0)
        {
            targetZ *= -1;
        }

        return new Vector3(targetX, targetY, targetZ);
    }
}
