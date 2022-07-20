using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject pauseUi;

    public int hasbullets;     //������ źȯ
    public int cartridgeCount = 80; // ź�� ī��Ʈ

    public static PlayerManager instance; // �̱���

    private bool repeaterLock; // ���� ����
    private bool clickLock = false; // Ŭ�� ����
    private bool pauseFlag = false; // �Ͻ����� �÷���
    private int layerMask;
=======

    private bool repeaterLock; // ���� ����
    private bool clickLock = false; // Ŭ�� ����
>>>>>>> origin/Jms
    private float clickTime; // ���� Ŭ�� �ð� üũ �ð���
    private float currentHp; // ���� HP���� �ʵ�
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
<<<<<<< HEAD
    private GameObject nearObject = null;  //������ �ִ� ������
=======
>>>>>>> origin/Jms
    private RaycastHit hit;
    private Vector3 movementVector;
    private Vector3 muzzlePoint; // �Ѿ� �߻� ��������
    private Vector3 targetPoint; // �Ѿ� �߻� ��ǥ����

    private float rebound = -1f; // ������ ����� �� �̵� ���� ��ȭ��

<<<<<<< HEAD
    private void Awake()
    {
        instance = this;
        layerMask = (1 << LayerMask.NameToLayer("Terrain")) + (1 << LayerMask.NameToLayer("AimPoint"));
    }

=======
>>>>>>> origin/Jms
    private void Start()
    {
        var status = GetComponent<PlayerStatusManager>();
        repeaterLock = true; // ���� Ȱ��ȭ
        control = Aiming;
        control += Movement;
        control += Shot;
        playerRigidbody = this.GetComponent<Rigidbody>();
        camera = Camera.main; // ���� ī�޶� ������Ʈ�� ������
        control += camera.GetComponent<CameraManager>().RotateCamera;
<<<<<<< HEAD
        control += Pause;
        currentHp = status.GetHP();
    }

    private void FixedUpdate()
=======
        currentHp = status.GetHP();
        Debug.Log(currentHp);
    }

    private void Update()
>>>>>>> origin/Jms
    {
        control();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾��� ������ ������
        var pStatus = GetComponent<PlayerStatusManager>();
        // �� �߻�ü ���� ��������
        // ������Ʈ���� �޶��� �� ����
        var eDirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
        var eHowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();
        var eSpinnerBulletStatus = other.GetComponent<SpinnerBulletStausManager>();
<<<<<<< HEAD
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Bullet"))
=======

        if (other.CompareTag("FX"))
>>>>>>> origin/Jms
        {
            // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
            if (eDirectBulletStatus != null)
            {
                currentHp -= (eDirectBulletStatus.GetAttackDamage() - pStatus.GetDefense());
<<<<<<< HEAD
                other.gameObject.SetActive(false);
=======
                Debug.Log(currentHp);
>>>>>>> origin/Jms
            }
            else if (eHowitzerBulletStatus != null)
            {
                currentHp -= (eHowitzerBulletStatus.GetAttackDamage() - pStatus.GetDefense());
<<<<<<< HEAD
                other.gameObject.SetActive(false);
=======
                Debug.Log(currentHp);
>>>>>>> origin/Jms
            }
            else if (eSpinnerBulletStatus != null)
            {
                currentHp -= (eSpinnerBulletStatus.GetAttackDamage() - pStatus.GetDefense());
<<<<<<< HEAD
                other.gameObject.SetActive(false);
=======
                Debug.Log(currentHp);
>>>>>>> origin/Jms
            }
        }
    }

<<<<<<< HEAD
    public float GetHp()
    {
        return currentHp;
    }

=======
>>>>>>> origin/Jms
    // ����
    private void Aiming()
    {

        var cameraState = CameraManager.cameraState; // ī�޶� ���°�
        var playerPosition = transform.position;
<<<<<<< HEAD

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit,1000, layerMask))
=======
        var aimingLayer = 1 << LayerMask.NameToLayer("AimPoint"); // ���� ����Ʈ �ݶ��̴� ���̾� Ư��

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, aimingLayer))
>>>>>>> origin/Jms
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                if (cameraState == 0) targetPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z); // ��� �����Ǽ��� ����
                else if (cameraState == 1) targetPoint = new Vector3(hit.point.x, playerPosition.y, hit.point.z); // ž�� �����Ǽ��� ����
                else if (cameraState == 2) targetPoint = new Vector3(playerPosition.x, hit.point.y, hit.point.z); // ���̵�� �����Ǽ��� ����
            }
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
        var playerPosition = transform.position;

        // �÷��̾ �̵��ϴ� ����� �ӵ��� ������ ����
        float movementX = Input.GetAxis("XMove"); // A, D
        float movementY = Input.GetAxis("YMove"); // W, S
        if (cameraState == 0) movementVector = new Vector3(movementX, movementY, 0) * speed; // ��� ���������� �̵�
        else if (cameraState == 1) movementVector = new Vector3(movementX, 0, movementY) * speed; // ž�� ���������� �̵�
        else if (cameraState == 2) movementVector = new Vector3(0, movementY, movementX) * speed; // ���̵�� ���������� �̵�

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

<<<<<<< HEAD
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseFlag)
            {
                Time.timeScale = 0;
                pauseUi.SetActive(true);
                pauseFlag = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseUi.SetActive(false);
                pauseFlag = false;
            }
        }
    }

=======
>>>>>>> origin/Jms
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Repeater()
    {
        var attackSpeed = GetComponent<PlayerStatusManager>().GetAttackSpeed(); // ���ݼӵ� ���� ������ ����
        if (Time.time - clickTime > attackSpeed) clickLock = false;
        if (Input.GetMouseButtonDown(0) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
<<<<<<< HEAD
            if (hasbullets == 0)  // ������ �ִ� ź�� �⺻�϶�
            {
                while (repeaterLock)
                {
                    var targetBullet = ObjectManager.instance.GetBullet("DirectBullet");
                    var attackSpeedToBullet = targetBullet.GetComponent<DirectBullet>().GetAttackSpeedToBullet();
                    SetBullet(targetBullet, "DirectBullet"); // źȯ �߻�
                    yield return new WaitForSeconds(attackSpeedToBullet + attackSpeed); // ����ð���ŭ ���
                    if (hasbullets != 0)
                        repeaterLock = false;


                }
            }

            if (hasbullets == 1)  // ������ �ִ� ź�� ����϶�
            {
                while (repeaterLock)
                {
                    SetBullet(ObjectManager.instance.GetBullet("HowitzerBullet"), "HowitzerBullet"); // źȯ �߻�
                    --cartridgeCount;
                    if (cartridgeCount == 0) hasbullets = 0;
                    yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
                    if (hasbullets != 1)
                        repeaterLock = false;
                }
            }
            if (hasbullets == 2) // ������ �ִ� ź�� �����϶�
            {
                while (repeaterLock)
                {
                    SetBullet(ObjectManager.instance.GetBullet("Buckshot"), "Buckshot"); // źȯ �߻�
                    --cartridgeCount;
                    if (cartridgeCount == 0) hasbullets = 0;
                    yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
                    if (hasbullets != 2)
                        repeaterLock = false;
                }
            }
            if (hasbullets == 3) // ������ �ִ� ź�� ���ǳ��϶�
            {
                while (repeaterLock)
                {
                    SetBullet(ObjectManager.instance.GetBullet("SpinnerBullet"), "SpinnerBullet"); // źȯ �߻�
                    --cartridgeCount;
                    if (cartridgeCount == 0) hasbullets = 0;
                    yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
                    if (hasbullets != 3)
                        repeaterLock = false;
                }
            }

            if (hasbullets == 4) // ������ �ִ� ź�� ü�̼��϶�
            {
                while (repeaterLock)
                {
                    SetBullet(ObjectManager.instance.GetBullet("ChaserBullet"), "ChaserBullet"); // źȯ �߻�
                    --cartridgeCount;
                    if (cartridgeCount == 0) hasbullets = 0;
                    yield return new WaitForSeconds(attackSpeed); // ����ð���ŭ ���
                    if (hasbullets != 4)
                        repeaterLock = false;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
=======
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
            if (Time.time - clickTime < attackSpeed) clickLock = true;
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
>>>>>>> origin/Jms
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
<<<<<<< HEAD
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z + 3); // ��ġ ����
=======
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z); // ��ġ ����
>>>>>>> origin/Jms
        var directionVector = (targetPoint - bulletTf.position).normalized; // źȯ�� �߻� �� ���⺤�� ����

        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        bulletTf.localRotation = Quaternion.LookRotation(directionVector);
        bullet.SetActive(true); // Ȱ��ȭ
        if (name == "DirectBullet")
        {
            bullet.GetComponent<DirectBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "HowitzerBullet")
        {
            bullet.GetComponent<HowitzerBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "Buckshot")
        {
            for (int i = 0; i < 5; i++)
            {
                bulletTf.GetChild(i).transform.position = bulletTf.position;  //�ڽ� ������Ʈ �θ�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
        else if (name == "SpinnerBullet")
        {
            // ��ź ��ġ �缳��
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bullet.gameObject.transform.GetChild(i).GetChild(j).transform.position = bulletTf.position;
                }
            }
            bullet.GetComponent<SpinnerBullet>().StartCoroutine("Shot", Vector3.forward); // źȯ ���� ���� �ڷ�ƾ ����
        }
<<<<<<< HEAD
        else if (name == "ChaserBullet")
        {
            bullet.GetComponent<ChaserBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }

    private void Interation()
    {
        if (nearObject != null)
        {
            if (nearObject.tag == "Bullet")
            {
                ItemManager item = nearObject.GetComponent<ItemManager>();
                hasbullets = item.value;
                Debug.Log(hasbullets);
                cartridgeCount = 80;
                nearObject.SetActive(false);
            }
        }
    }
    // ������ ������Ʈ ����
    private void OnTriggerStay(Collider other)  // �������� ������ ���
    {
        if (other.tag == "Bullet")
        {
            nearObject = other.gameObject;
            Interation();
        }

        if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }

    }
    // ������ ������Ʈ ����
    private void OnTriggerExit(Collider other)  // �������� �������� ���� ���
    {
        if (other.tag == "Bullet")
            nearObject = null;

        if (other.tag == "Item")
            nearObject = null;

=======
>>>>>>> origin/Jms
    }
}
