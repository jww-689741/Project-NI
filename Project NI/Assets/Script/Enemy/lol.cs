using System.Collections;
using UnityEngine;

public class LoL : MonoBehaviour
{
    public GameObject hit; // 피격 효과
    public GameObject explosion; // 파괴 효과
    public float repeaterInterval = 2f;

    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트
    private float currentHp; // 현재 HP저장 필드
    private float timer = 0; // 적 공격 간격 주는 타이머
    private delegate void Control();
    Control control;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<SaraStatusManager>();
        currentHp = status.GetHP();
        player = GameObject.FindWithTag("Player").transform;

        control += CheckCameraPosition;
        control += CheckActiveCondition;
    }

    void Update()
    {
        control();

        if (enemyTransform.position.z > player.position.z) // 적이 플레이어 보다 전방에 있을 때만 공격 실행
        {
            if (timer > repeaterInterval)
            {
                StartCoroutine("Repeater", "DirectBullet");
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    // 적 개체 축 고정
    void CheckCameraPosition()
    {
        Vector3 enemyPosition = enemyTransform.position; // 자기 자신의 좌표를 저장

        if (CameraManager.cameraState == 0) // 백뷰
        {
            // 아무것도 안함, 혹시 몰라 넣어놈 
        }
        else if (CameraManager.cameraState == 1) // 탑뷰
        {
            transform.position = new Vector3(enemyPosition.x, player.position.y, enemyPosition.z); // y 축 플레이어 좌표로 고정
            position = new Vector3(position.x, player.position.y, position.z);
        }
        else if (CameraManager.cameraState == 2) // 사이드뷰
        {
            transform.position = new Vector3(player.position.x, enemyPosition.y, enemyPosition.z); // x 축 플레이어 좌표로 고정
            position = new Vector3(player.position.x, position.y, position.z);
        }
    }

    // 플레이어 보다 20만큼 뒤로 가면 비활성화
    private void CheckActiveCondition()
    {
        Vector3 enemyPosition = enemyTransform.position; // 자기 자신의 좌표를 저장

        if (enemyPosition.z < player.position.z - 20)
        {
            this.gameObject.SetActive(false);
        }
    }

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 사라의 스탯을 가져옴
        var saraStatus = GetComponent<SaraStatusManager>();
        // 적 발사체 스탯 가져오기
        // 컴포넌트명은 달라질 수 있음
        Transform parent = other.gameObject.transform.parent; // 부모 오브젝트 가져오기
        var DirectBulletStatus = parent.GetComponent<DirectBulletStatusManager>(); // 부모 오브젝트 스크립트 가져오기
        var HowitzerBulletStatus = parent.GetComponent<HowitzerBulletStatusManager>(); // 부모 오브젝트 스크립트 가져오기

        if (other.gameObject.transform.parent != null) // 부모 오브젝트가 있을 때
        {
            if (parent.CompareTag("Bullet")) // 부모의 태그가 FX일 때
            {
                other.gameObject.SetActive(false); // 충돌한 탄환 오브젝트 비활성화

                // 피격당한 발사체의 공격력 - 플레이어의 방어력만큼의 데미지를 현재 체력에서 감산
                if (DirectBulletStatus != null)
                {
                    Instantiate(hit, this.transform.position, this.transform.rotation);
                    currentHp -= (DirectBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (HowitzerBulletStatus != null)
                {
                    currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
            } 
            else if (other.CompareTag("Bullet"))
            {
                DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
                HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();

                other.gameObject.SetActive(false); // 충돌한 탄환 오브젝트 비활성화

                // 피격당한 발사체의 공격력 - 플레이어의 방어력만큼의 데미지를 현재 체력에서 감산
                if (DirectBulletStatus != null)
                {
                    Instantiate(hit, this.transform.position, this.transform.rotation);
                    currentHp -= (DirectBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (HowitzerBulletStatus != null)
                {
                    currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
            }
        }

        CheckHP();
    }

    void CheckHP()
    {
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            // 파괴 효과
            Instantiate(explosion, this.transform.position, this.transform.rotation);
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