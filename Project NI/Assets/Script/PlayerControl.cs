using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
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
            StartCoroutine("Repeater");
        }
        else if (Input.GetMouseButtonUp(0)) repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
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
    IEnumerator Repeater()
    {
        repeaterLock = true; // ���� Ȱ��ȭ
        while (repeaterLock)
        {
            SetBullet(ObjectManager.instance.GetBullet("Test")); // źȯ �߻�
            yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
        }
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z + 0.6f)); // ��ġ ����
        shotPoint[1] = new Vector3(bullet.transform.position.x, bullet.transform.position.y, bullet.transform.position.z);
        bullet.transform.rotation = this.transform.rotation; // ȸ���� ����
        bullet.SetActive(true); // Ȱ��ȭ
        bullet.GetComponent<Bullet>().StartCoroutine("Shot", shotPoint); // źȯ ���� ���� �ڷ�ƾ ����
    }

}
