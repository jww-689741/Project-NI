using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float repeaterInterval = 2f;

    private float timer = 0; // 적 공격 간격 주는 타이머
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {

    }

    void Update()
    {
        if (enemyTransform.position.z > player.position.z + 10) // 적이 플레이어 보다 전방에 있을 때만 공격 실행
        {
            if (timer > repeaterInterval)
            {
                StartCoroutine("Repeater", "DirectBullet");
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    IEnumerator Repeater(string name)
    {
        SetBullet(ObjectManager.instance.GetBullet(name), name); // 탄환 발사
        yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // 위치 지정
        var directionVector = (player.position - bulletPosition).normalized; // 탄환이 발사 될 방향벡터 연산
        bullet.transform.localRotation = Quaternion.LookRotation(directionVector);
        bullet.SetActive(true); // 활성화

        if (name == "DirectBullet")
        {
            bullet.GetComponent<DirectBullet>().StartCoroutine("Shot", directionVector); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<HowitzerBullet>().StartCoroutine("Shot", directionVector); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //자식 오브젝트 부모와 동일한 위치로 이동
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // 탄환 동작 로직 코루틴 시작
        }
    }
}
