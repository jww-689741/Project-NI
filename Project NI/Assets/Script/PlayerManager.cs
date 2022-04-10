using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float repeaterInterval; // 연사속도
    public float movementSpeed; // 이동속도

    private bool repeaterLock; // 연사 여부
    private bool clickLock = false;
    private float clickTime;
    private float time;
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
    private RaycastHit hit;
    private Vector3 movementVector;
    private Vector3[] shotPoint;

    private void Start()
    {
        repeaterLock = true; // 연사 활성화
        shotPoint = new Vector3[2];
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // 메인 카메라 컴포넌트를 가져옴
    }

    private void Update()
    {
        control();
        time += Time.deltaTime;
    }

    // 조준
    private void Aiming() {

        // 화면 상의 마우스 포인터 위치로 레이캐스팅
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if(CameraManager.cameraState == 0) shotPoint[0] = new Vector3(hit.point.x, hit.point.y, hit.point.z); // 백뷰 시점의서의 조준
            else if (CameraManager.cameraState == 1) shotPoint[0] = new Vector3(hit.point.x, 0, hit.point.z); // 탑뷰 시점의서의 조준
            else if (CameraManager.cameraState == 2) shotPoint[0] = new Vector3(0, hit.point.y, hit.point.z); // 사이드뷰 시점의서의 조준
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
        // 플레이어가 이동하는 방향과 속도를 삽입한 벡터
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if(CameraManager.cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * movementSpeed; // 백뷰 시점에서의 이동
        else if (CameraManager.cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * movementSpeed; // 탑뷰 시점에서의 이동
        else if (CameraManager.cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * movementSpeed; // 사이드뷰 시점에서의 이동
        playerRigidbody.velocity = movementVector;
    }

    // 탄환 발사 코루틴
    IEnumerator Repeater()
    {
        if (Time.time - clickTime > repeaterInterval) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("DirectBullet"), "DirectBullet"); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(1) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(1)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(4) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(4)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetMouseButtonDown(3) && !clickLock) // 마우스 좌클릭 유지중일 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
        else if (Input.GetMouseButtonUp(3)) // 마우스 좌클릭에서 손을 뗄때
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // 마우스 좌클릭에서 손을 뗄때
        }
        if (Input.GetKeyDown(KeyCode.E) && !clickLock) // 키보드 E키를 눌렀을 때
        {
            clickTime = Time.time;
            repeaterLock = true; // 연사 활성화
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("ChaserBullet"), "ChaserBullet"); // 탄환 발사
                yield return new WaitForSeconds(repeaterInterval); // 연사시간만큼 대기
            }
        }
        else if (Input.GetKeyUp(KeyCode.E)) // 키보드 E키에서 손을 뗄때
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false;
        }

    }

    // 탄환 오브젝트 위치 지정, 회전각도 설정, 오브젝트 활성화, 실제 발사 로직 작동
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // 받아올 탄환이 없을 경우 반환
        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 1.5f)); // 위치 지정
        shotPoint[1] = new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z);
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
        else if(name == "SpinnerBullet"){
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    bullet.gameObject.transform.GetChild(i).GetChild(j).transform.position = bullet.transform.position;
                }
            }
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
        else if(name == "ChaserBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // 탄환 동작 로직 코루틴 시작
        }
    }
}
