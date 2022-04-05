using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float repeaterInterval; // ����ӵ�
    public float movementSpeed; // �̵��ӵ�

    private bool repeaterLock; // ���� ����
    private delegate void Control();
    Control control;
    private Rigidbody playerRigidbody;
    private Camera camera;
    private RaycastHit hit;
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
    }

    // ����
    private void Aiming() {

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            shotPoint[0] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }


    // źȯ �߻�
    private void Shot()
    {

        if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ�� �������� ��
        {
            StartCoroutine("Repeater", "DirectBullet");
        }
        else if (Input.GetMouseButtonUp(0)) repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� �������� ��
        {
            StartCoroutine("Repeater", "HowitzerBullet");
        }
        else if (Input.GetMouseButtonUp(1)) repeaterLock = false; // ���콺 ��Ŭ������ ���� ����

        if (Input.GetMouseButtonDown(4)) // ���콺 �߰���ư �������϶�
        {
            StartCoroutine("Repeater", "Buckshot");
        }
        else if (Input.GetMouseButtonUp(4)) repeaterLock = false; // ���콺 �߰���ư���� ���� ����
    }

    // �÷��̾� �̵�
    private void Movement()
    {
        // �÷��̾ �̵��ϴ� ����� �ӵ��� ������ ���� - ���콺 �� ��Ʈ�� ���� �ʿ�
        float movementX = Input.GetAxis("XMove");
        float movementY = Input.GetAxis("YMove");
        float movementZ = Input.GetAxis("ZMove");
        Vector3 movementVector = new Vector3(movementX, movementY, movementZ) * movementSpeed;
        playerRigidbody.velocity = movementVector;
    }

    // źȯ ���� �ڷ�ƾ
    IEnumerator Repeater(string name)
    {
        repeaterLock = true; // ���� Ȱ��ȭ
        while (repeaterLock)
        {
            SetBullet(ObjectManager.instance.GetBullet(name), name); // źȯ �߻�
            yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
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
    }
}
