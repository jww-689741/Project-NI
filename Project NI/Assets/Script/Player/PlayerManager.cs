using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private bool repeaterLock; // ���� ����
    private bool clickLock = false; // Ŭ�� ����
    private float clickTime; // ���� Ŭ�� �ð� üũ �ð���
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
    private RaycastHit hit;
    private Vector3 movementVector;
    private Vector3 muzzlePoint; // �Ѿ� �߻� ��������
    private Vector3 targetPount; // �Ѿ� �߻� ��ǥ����

    private void Start()
    {
        repeaterLock = true; // ���� Ȱ��ȭ
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // ���� ī�޶� ������Ʈ�� ������
    }

    private void Update()
    {
        control();
    }

    // ����
    private void Aiming() {

        var cameraState = CameraManager.cameraState; // ī�޶� ���°�

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if(cameraState == 0) targetPount = new Vector3(hit.point.x, hit.point.y, hit.point.z); // ��� �����Ǽ��� ����
            else if (cameraState == 1) targetPount = new Vector3(hit.point.x, 0, hit.point.z); // ž�� �����Ǽ��� ����
            else if (cameraState == 2) targetPount = new Vector3(0, hit.point.y, hit.point.z); // ���̵�� �����Ǽ��� ����
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
        var speed = GetComponent<PlayerStatusManager>().GetMoveSpeed(); // �̵��ӵ� ���� ������ ����
        var cameraState = CameraManager.cameraState; // ī�޶� ���°�

        // �÷��̾ �̵��ϴ� ����� �ӵ��� ������ ����
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if(cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * speed; // ��� ���������� �̵�
        else if (cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * speed; // ž�� ���������� �̵�
        else if (cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * speed; // ���̵�� ���������� �̵�
        playerRigidbody.velocity = movementVector;
    }

    // źȯ �߻� �ڷ�ƾ
    IEnumerator Repeater()
    {
        var attackSpeed = GetComponent<PlayerStatusManager>().GetAttackSpeed(); // ���ݼӵ� ���� ������ ����
        if (Time.time - clickTime > attackSpeed) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                var targetBullet = ObjectManager.instance.GetBullet("DirectBullet");
                var attackSpeedToBullet = targetBullet.GetComponent<DirectBullet>().GetAttackSpeedToBullet();
                SetBullet(targetBullet, "DirectBullet"); // źȯ �߻�
                yield return new WaitForSeconds(attackSpeedToBullet + attackSpeed); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(1) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // źȯ �߻�
                yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(1)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(4) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // źȯ �߻�
                yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(4)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
        if (Input.GetMouseButtonDown(3) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            while (repeaterLock)
            {
                SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // źȯ �߻�
                yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
            }
        }
        else if (Input.GetMouseButtonUp(3)) // ���콺 ��Ŭ������ ���� ����
        {
            if (Time.time - clickTime < attackSpeed) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }

    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        var bulletTf = bullet.transform; // źȯ�� transform��
        var playerTf = this.transform; // �÷��̾��� transform��
        var directionVector = (targetPount - muzzlePoint).normalized; // źȯ�� �߻� �� ���⺤�� ����

        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, (playerTf.position.z + 1.5f)); // ��ġ ����
        muzzlePoint = new Vector3(bulletTf.position.x, bulletTf.position.y, bulletTf.position.z);

        bulletTf.rotation = playerTf.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        if (name == "DirectBullet")
        {
            bullet.GetComponent<DirectBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "HowitzerBullet")
        {
            Debug.Log("123");
            bullet.GetComponent<HowitzerBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bulletTf.GetChild(i).transform.position = bulletTf.position;  //�ڽ� ������Ʈ �θ�1�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if(name == "SpinnerBullet"){
            // ��ź ��ġ �缳��
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    bullet.gameObject.transform.GetChild(i).GetChild(j).transform.position = bulletTf.position;
                }
            }
            bullet.GetComponent<SpinnerBullet>().StartCoroutine("Shot", false); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }
}
