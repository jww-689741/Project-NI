using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private bool repeaterLock; // 연사 제한
    private bool clickLock = false; // 클릭 제한
    private float clickTime; // 연속 클릭 시간 체크 시간값
    private float currentHp; // 현재 HP저장 필드
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
    private RaycastHit hit;
    private Vector3 movementVector;
    private Vector3 muzzlePoint; // 총알 발사 시작지점
    private Vector3 targetPoint; // 총알 발사 목표지점

    private float rebound = -1f; // 범위를 벗어났을 때 이동 방향 변화량

    private void Start()
    {
        var status = GetComponent<PlayerStatusManager>();
        repeaterLock = true; // 연사 활성화
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // 메인 카메라 컴포넌트를 가져옴
        control += camera.GetComponent<CameraManager>().RotateCamera;
        currentHp = status.GetHP();
        Debug.Log(currentHp);
    }

    private void Update()
    {
        control();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어의 스탯을 가져옴
        var pStatus = GetComponent<PlayerStatusManager>();
        // 적 발사체 스탯 가져오기
        // 컴포넌트명은 달라질 수 있음
        var eDirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
        var eHowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();
        var eSpinnerBulletStatus = other.GetComponent<SpinnerBulletStausManager>();
        if (other.CompareTag("EnemyBullet"))
        {
            // 피격당한 발사체의 공격력 - 플레이어의 방어력만큼의 데미지를 현재 체력에서 감산
            if (eDirectBulletStatus != null)
            {
                currentHp -= (eDirectBulletStatus.GetAttackDamage() - pStatus.GetDefense());
                Debug.Log(currentHp);
            }
            else if (eHowitzerBulletStatus != null)
            {
                currentHp -= (eHowitzerBulletStatus.GetAttackDamage() - pStatus.GetDefense());
                Debug.Log(currentHp);
            }
            else if (eSpinnerBulletStatus != null)
            {
                currentHp -= (eSpinnerBulletStatus.GetAttackDamage() - pStatus.GetDefense());
                Debug.Log(currentHp);
            }
        }
    }

    // 조준
    private void Aiming()
    {

        var cameraState = CameraManager.cameraState; // 카메라 상태값
        var playerPosition = transform.position;

        // 화면 상의 마우스 포인터 위치로 레이캐스팅
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                if (cameraState == 0) targetPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z); // 백뷰 시점의서의 조준
                else if (cameraState == 1) targetPoint = new Vector3(hit.point.x, playerPosition.y, hit.point.z); // 탑뷰 시점의서의 조준
                else if (cameraState == 2) targetPoint = new Vector3(playerPosition.x, hit.point.y, hit.point.z); // 사이드뷰 시점의서의 조준
            }
        }
    }

    // 탄환 발사
    private void Shot()
    {
        StartCoroutine("Repeater");
    }

    // 플레이어 이동
    private void Movement()
    {
        var speed = GetComponent<PlayerStatusManager>().GetMoveSpeed(); // 이동속도 스탯 데이터 접근
        var cameraState = CameraManager.cameraState; // 카메라 상태값
        var playerPosition = transform.position;

        // 플레이어가 이동하는 방향과 속도를 삽입한 벡터
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if (cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * speed; // 백뷰 시점에서의 이동
        else if (cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * speed; // 탑뷰 시점에서의 이동
        else if (cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * speed; // 사이드뷰 시점에서의 이동

        if (playerPosition.y >= 20)
        {
            playerRigidbody.velocity = movementVector;
            rebound = -1f;
        }
        else if (playerPosition.y < 20)
        {
            if (cameraState == 0) playerRigidbody.velocity = new Vector3(movementX, rebound, 0) * speed;
            else if (cameraState == 2) playerRigidbody.velocity = new Vector3(0, rebound, movementX) * speed;
            rebound += 0.01f;
        }
    }

    // 탄환 발사 코루틴
    IEnumerator Repeater()
    {
        var attackSpeed = GetComponent<PlayerStatusManager>().GetAttackSpeed(); // 공격속도 스탯 데이터 접근
        if (Time.time - clickTime > attackSpeed) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                var targetBullet = ObjectManager.instance.GetBullet("DirectBullet");
                var attackSpeedToBullet = targetBullet.GetComponent<DirectBullet>().GetAttackSpeedToBullet();
                SetBullet(targetBullet, "DirectBullet"); // 탄환 발사
                yield return new WaitForSeconds(attackSpeedToBullet + attackSpeed); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(1) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // 탄환 발사
                yield return new WaitForSeconds(attackSpeed); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(1)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(4) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // 탄환 발사
                yield return new WaitForSeconds(attackSpeed); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(4)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(3) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // 탄환 발사
                yield return new WaitForSeconds(attackSpeed); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(3)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }

    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        var bulletTf = bullet.transform; // 탄환의 transform값
        var playerTf = this.transform; // 플레이어의 transform값
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z); // 위치 지정
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
            Debug.Log("123");
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
    }
}
