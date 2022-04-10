using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float repeaterInterval; // ����ӵ�
    public float movementSpeed; // �̵��ӵ�

    private bool repeaterLock; // ���� ����
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
        repeaterLock = true; // ���� Ȱ��ȭ
        shotPoint = new Vector3[2];
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // ���� ī�޶� ������Ʈ�� ������
    }

    private void Update()
    {
        control();
        time += Time.deltaTime;
    }

    // ����
    private void Aiming() {

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if(CameraManager.cameraState == 0) shotPoint[0] = new Vector3(hit.point.x, hit.point.y, hit.point.z); // ��� �����Ǽ��� ����
            else if (CameraManager.cameraState == 1) shotPoint[0] = new Vector3(hit.point.x, 0, hit.point.z); // ž�� �����Ǽ��� ����
            else if (CameraManager.cameraState == 2) shotPoint[0] = new Vector3(0, hit.point.y, hit.point.z); // ���̵�� �����Ǽ��� ����
        }
    }


    // źȯ �߻�
    private void Shot()
    {
        StartCoroutine("Repeater");
    }

    // �÷��̾� �̵�
    private void Movement()
    {
        // �÷��̾ �̵��ϴ� ����� �ӵ��� ������ ����
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if(CameraManager.cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * movementSpeed; // ��� ���������� �̵�
        else if (CameraManager.cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * movementSpeed; // ž�� ���������� �̵�
        else if (CameraManager.cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * movementSpeed; // ���̵�� ���������� �̵�
        playerRigidbody.velocity = movementVector;
    }

    // źȯ �߻� �ڷ�ƾ
    IEnumerator Repeater()
    {
        if (Time.time - clickTime > repeaterInterval) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("DirectBullet"), "DirectBullet"); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(1) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(1)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(4) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(4)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(3) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(3)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetKeyDown(KeyCode.E) && !clickLock) // Ű���� EŰ�� ������ ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("ChaserBullet"), "ChaserBullet"); // źȯ �߻�
                yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
            }
        }
        else if (Input.GetKeyUp(KeyCode.E)) // Ű���� EŰ���� ���� ����
        {
            if (Time.time - clickTime < repeaterInterval) clickLock = true;
            repeaterLock = false;
        }

    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ
        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 1.5f)); // ��ġ ����
        shotPoint[1] = new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z);
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        if (name == "DirectBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //�ڽ� ������Ʈ �θ�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if(name == "SpinnerBullet"){
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    bullet.gameObject.transform.GetChild(i).GetChild(j).transform.position = bullet.transform.position;
                }
            }
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if(name == "ChaserBullet")
        {
            bullet.GetComponent<BulletManager>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }
}
