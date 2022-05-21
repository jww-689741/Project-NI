using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int cartridgeCount = 80; // 탄약 카운트

    public Text contentText; // 탄환 수량 UI 텍스트

    [SerializeField]
    private Stat pDamage; // 플레이어 공격력

    [SerializeField]
    private Stat pInterval; // 플레이어 공격속도

    [SerializeField]
    private float pMaxDamage; // 플레이어 공격력 최대값

    [SerializeField]
    private float pSetDamage; // 플레이어 공격력 초기값

    [SerializeField]
    private float pMaxInterval; // 플레이어 공격속도 최대값

    [SerializeField]
    private float pSetInterval; // 플레이어 공격속도 초기값

    private Vector3 targetPoint; // 총알 발사 목표지점
    public int hasbullets;     //소지한 탄환
    private float clickTime; // 연속 클릭 시간 체크 시간값
    private bool clickLock; // 클릭 제한
    private bool repeaterLock; // 연사 제한
    private GameObject nearObject = null;  //가까이 있는 아이템

    private void Start()
    {
        pDamage.SetDefaultStat(pSetDamage, pMaxDamage);
        pInterval.SetDefaultStat(pSetInterval, pMaxInterval);
    }
    private void Update()
    {
        Aiming();
        SetBulletCount();
        if (Time.time - clickTime > pInterval.currentValue) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            Shot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickTime < pInterval.currentValue) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
    }
    
    public float GetAttack()
    {
        return pDamage.currentValue;
    }

    // 조준
    public void Aiming()
    {
        var layerMask = (1 << LayerMask.NameToLayer("Terrain")) + (1 << LayerMask.NameToLayer("AimPoint")); ;
        var cameraState = CameraManager.cameraState; // 카메라 상태값
        var playerPosition = transform.position;

        // 화면 상의 마우스 포인터 위치로 레이캐스팅
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, layerMask))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                if (cameraState == 0) targetPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z); // 백뷰 시점의서의 조준
                else if (cameraState == 1) targetPoint = new Vector3(hit.point.x, playerPosition.y, hit.point.z); // 탑뷰 시점의서의 조준
                else if (cameraState == 2) targetPoint = new Vector3(playerPosition.x, hit.point.y, hit.point.z); // 사이드뷰 시점의서의 조준
            }
        }
    }

    // 발사
    public void Shot()
    {
        StartCoroutine("ShotCoroutine");
    }

    IEnumerator ShotCoroutine()
    {
        if (hasbullets == 0)  // 가지고 있는 탄이 기본일때
        {
            while (repeaterLock)
            {
                var targetBullet = ObjectManager.instance.GetBullet("DirectBullet");
                var attackSpeedToBullet = targetBullet.GetComponent<DirectBullet>().GetAttackSpeedToBullet();
                SetBullet(targetBullet, "DirectBullet"); // 탄환 발사
                yield return new WaitForSeconds(attackSpeedToBullet + pInterval.currentValue); // 연사시간만큼 대기
                if (hasbullets != 0)
                    repeaterLock = false;


            }
        }

        if (hasbullets == 1)  // 가지고 있는 탄이 곡사일때
        {
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // 탄환 발사
                --cartridgeCount;
                if (cartridgeCount == 0) hasbullets = 0;
                yield return new WaitForSeconds(pInterval.currentValue); // 연사시간만큼 대기
                if (hasbullets != 1)
                    repeaterLock = false;
            }
        }
        if (hasbullets == 2) // 가지고 있는 탄이 벅샷일때
        {
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // 탄환 발사
                --cartridgeCount;
                if (cartridgeCount == 0) hasbullets = 0;
                yield return new WaitForSeconds(pInterval.currentValue); // 연사시간만큼 대기
                if (hasbullets != 2)
                    repeaterLock = false;
            }
        }
        if (hasbullets == 3) // 가지고 있는 탄이 스피너일때
        {
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // 탄환 발사
                --cartridgeCount;
                if (cartridgeCount == 0) hasbullets = 0;
                yield return new WaitForSeconds(pInterval.currentValue); // 연사시간만큼 대기
                if (hasbullets != 3)
                    repeaterLock = false;
            }
        }

        if (hasbullets == 4) // 가지고 있는 탄이 체이서일때
        {
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("ChaserBullet"), "ChaserBullet"); // 탄환 발사
                --cartridgeCount;
                if (cartridgeCount == 0) hasbullets = 0;
                yield return new WaitForSeconds(pInterval.currentValue); // 연사시간만큼 대기
                if (hasbullets != 4)
                    repeaterLock = false;
            }
        }
    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        var bulletTf = bullet.transform; // 탄환의 transform값
        var playerTf = this.transform; // 플레이어의 transform값
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z + 3); // 위치 지정
        var directionVector = (targetPoint - bulletTf.position).normalized; // 탄환이 발사 될 방향벡터 연산

        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환

        bulletTf.localRotation = Quaternion.LookRotation(directionVector);
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
                bulletTf.GetChild(i).transform.position = bulletTf.position;  //자식 오브젝트 부모와 동일한 위치로 이동
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "SpinnerBullet")
        {
            // 자탄 위치 재설정
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bullet.gameObject.transform.GetChild(i).GetChild(j).transform.position = bulletTf.position;
                }
            }
            bullet.GetComponent<SpinnerBullet>().StartCoroutine("Shot", Vector3.forward); // 탄환 동작 로직 코루틴 시작
        }
        else if (name == "ChaserBullet")
        {
            bullet.GetComponent<ChaserBullet>().StartCoroutine("Shot", directionVector); // 탄환 동작 로직 코루틴 시작
        }
    }

    // 탄환 소모 카운트 증감
    private void SetBulletCount()
    {
        if (hasbullets == 0)
        {
            contentText.text = "X ∞";
        }
        else if (hasbullets == 1)
        {
            contentText.text = "X " + cartridgeCount;
        }
        else if (hasbullets == 2)
        {
            contentText.text = "X " + cartridgeCount;
        }
        else if (hasbullets == 3)
        {
            contentText.text = "X " + cartridgeCount;
        }
        else if (hasbullets == 4)
        {
            contentText.text = "X " + cartridgeCount;
        }
    }

    private void Interation()
    {
        if (nearObject != null)
        {
            if (nearObject.tag == "BulletItem")
            {
                ItemManager item = nearObject.GetComponent<ItemManager>();
                hasbullets = item.value;
                Debug.Log(hasbullets);
                cartridgeCount = 80;
                nearObject.SetActive(false);
            }
        }
    }
    // 아이템 오브젝트 감지
    private void OnTriggerStay(Collider other)  // 아이템이 감지될 경우
    {
        if (other.tag == "BulletItem")
        {
            nearObject = other.gameObject;
            Interation();
        }

        if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }

    }
    // 아이템 오브젝트 감지
    private void OnTriggerExit(Collider other)  // 아이템이 감지되지 않을 경우
    {
        if (other.tag == "BulletItem")
            nearObject = null;

        if (other.tag == "Item")
            nearObject = null;

    }
}
