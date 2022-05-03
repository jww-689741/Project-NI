using UnityEngine;

public class Sara : MonoBehaviour
{
    public float movementSpeed; // 이동속도
    public GameObject explosion; // 파괴 효과
    public GameObject hit; // 피격 효과

    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트
    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private float currentHp; // 현재 HP저장 필드

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()    
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position; // 초기 좌표를 목표 좌표에 입력, 대형을 유지하기 위해 필요
        currentHp = status.GetHP();
        movementSpeed = status.GetMoveSpeed();
        LinearMovement(Random.value > 0.5f);
    }

    void Update()
    {
        //move();
        CheckHP();
        CheckCameraPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 사라의 스탯을 가져옴
        var saraStatus = GetComponent<SaraStatusManager>();
        // 적 발사체 스탯 가져오기
        // 컴포넌트명은 달라질 수 있음
        var DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
        var HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();

        if(other.gameObject.transform.parent != null) // 부모 오브젝트가 있을 때
        {
            Transform parent = other.gameObject.transform.parent; // 부모 오브젝트 가져오기

            DirectBulletStatus = parent.GetComponent<DirectBulletStatusManager>(); // 부모 오브젝트 스크립트 가져오기
            HowitzerBulletStatus = parent.GetComponent<HowitzerBulletStatusManager>(); // 부모 오브젝트 스크립트 가져오기

            if (parent.CompareTag("FX")) // 부모의 태그가 FX일 때
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
        }
        else if (other.CompareTag("FX"))
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
    }

    // 이동 메소드
    private void move()
    {
        enemyTransform.LookAt(player); // 플레이어를 바라보게 한다.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // 선형이동
    void LinearMovement(bool directionFlag)
    {
        if (directionFlag)
        {
            ChangeTargetToPlayer(-50, -5, 5); // 플레이어 시점 앞에서 왼쪽으로 이동하기, 플레이어 시야 밖으로
        }
        else
        {
            ChangeTargetToPlayer(50, -5, 5); // 플레이어 시점 앞에서 오른쪽으로 이동하기, 플레이어 시야 밖으로
        }
    }

    // 체력이 0 이하 일 때 오브젝트 비활성화
    void CheckHP()
    {
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            // 파괴 효과
            Instantiate(explosion, this.transform.position, this.transform.rotation);
        }
    }

    void CheckCameraPosition()
    {
        if(CameraManager.cameraState == 0) // 백뷰
        {
            // 아무것도 안함
        } 
        else if (CameraManager.cameraState == 1) // 탑뷰
        {
            transform.position = new Vector3(enemyTransform.position.x, player.position.y, enemyTransform.position.z); // y 축 플레이어 좌표로 고정
            position = new Vector3(position.x, player.position.y, position.z);
        }
        else if (CameraManager.cameraState == 2) // 사이드뷰
        {
            transform.position = new Vector3(player.position.x, enemyTransform.position.y, enemyTransform.position.z); // x 축 플레이어 좌표로 고정
            position = new Vector3(player.position.x, position.y, position.z);
        }
    }
}
