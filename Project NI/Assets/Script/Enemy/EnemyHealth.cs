using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject hit; // 피격 효과
    public GameObject explosion; // 파괴 효과
    public int score; // 스코어

    private Vector3 position = Vector3.zero; 
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트
    private float currentHp; // 현재 HP저장 필드
    private string itemToDrop;
    private float playerDamageCoefficient; // 플레이어 데미지 계수
    private delegate void Control();
    Control control;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        playerDamageCoefficient = player.GetComponent<PlayerStatusManager>().GetAttackDamage() / 100; // 플레이어 데미지 계수 가져오기, /100은 현재 플레이어 대미지 계수가 정수라 확률로 바꾸는 작업
        currentHp = status.GetHP();
        score = 0;

        control = CheckCameraPosition;
        control += CheckActiveCondition;

        Item();
    }

    void Update()
    {
        control();
    }

    // 적 개체 축 고정
    void CheckCameraPosition()
    {
        Vector3 enemyPosition = enemyTransform.position;

        if (CameraManager.cameraState == 0) // 백뷰
        {
            
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
        var saraStatus = GetComponent<SaraStatusManager>(); // 사라의 스탯을 가져옴
        var DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>(); // 직사 스크립트 가져오기
        var HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>(); // 곡사 스크립트 가져오기

        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false); // 충돌한 탄환 오브젝트 비활성화

            // 피격당한 발사체의 공격력 - 플레이어의 방어력만큼의 데미지를 현재 체력에서 감산
            if (DirectBulletStatus != null)
            {
                Instantiate(hit, this.transform.position, this.transform.rotation); // 피격 효과
                currentHp -= Calculation(DirectBulletStatus.GetAttackDamage(), playerDamageCoefficient, saraStatus.GetDefense());
            }
            else if (HowitzerBulletStatus != null)
            {
                currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
            }
        }
        else if (other.CompareTag("Missile"))
        {
            var MissleStatus = GetComponent<MissileStatusManager>(); // 미사일 스크립트 가져오기

            if (MissleStatus != null)
            {
                currentHp -= (MissleStatus.GetAttackDamage() - saraStatus.GetDefense());
            }
        }

        CheckHP();
    }

    void CheckHP()
    {
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            Instantiate(explosion, this.transform.position, this.transform.rotation); // 파괴 효과

            DropItem();
        }
    }

    float Calculation(float hitDamage, float damageCoefficient, float defense) // 탄 데미지, 데미지 계수, 방어력
    {
        // 방어력을 데미지 경감률로 계산, 데미지를 플레이어 계수 만큼 더하는 방식
        return (1 - defense / 100) * (hitDamage + (hitDamage * damageCoefficient)); // (1 - 경감률) * (데미지 + (데미지 * 계수))
    }


    void DropItem()
    {
        if (ItemDropProbability())
        {
            if (itemToDrop == null || itemToDrop == "")
                return;
            else
            {
                GameObject item = ObjectManager.instance.GetItem(itemToDrop); // 아이템 받아오기

                item.transform.position = this.transform.position; // 해당 오브젝트의 위치에 아이템 놓기
                item.SetActive(true); // 아이템 활성화
            }
        }
    }

    // 아이템 드랍 확률
    bool ItemDropProbability()
    {
        // var itemStatus = GetComponent<>();
        bool persentage = Random.value <= 0.2; // 임시 20%
        return persentage;
    }

    void Item()
    {
        float number = Random.value;

        if (number < 0.2) itemToDrop = "BuckItem";
        else if (number < 0.4) itemToDrop = "BuckItem";
        else if (number < 0.6) itemToDrop = "BuckItem";
        else if (number < 0.8) itemToDrop = "BuckItem";
        else if (number <= 1) itemToDrop = "BuckItem";
    }
}