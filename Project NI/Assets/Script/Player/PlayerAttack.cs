using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int cartridgeCount = 80; // ź�� ī��Ʈ

    public Text contentText; // źȯ ���� UI �ؽ�Ʈ

    [SerializeField]
    private Stat pDamage; // �÷��̾� ���ݷ�

    [SerializeField]
    private Stat pInterval; // �÷��̾� ���ݼӵ�

    [SerializeField]
    private float pMaxDamage; // �÷��̾� ���ݷ� �ִ밪

    [SerializeField]
    private float pSetDamage; // �÷��̾� ���ݷ� �ʱⰪ

    [SerializeField]
    private float pMaxInterval; // �÷��̾� ���ݼӵ� �ִ밪

    [SerializeField]
    private float pSetInterval; // �÷��̾� ���ݼӵ� �ʱⰪ

    private Vector3 targetPoint; // �Ѿ� �߻� ��ǥ����
    public int hasbullets;     //������ źȯ
    private float clickTime; // ���� Ŭ�� �ð� üũ �ð���
    private bool clickLock; // Ŭ�� ����
    private bool repeaterLock; // ���� ����
    private GameObject nearObject = null;  //������ �ִ� ������

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
        if (Input.GetMouseButtonDown(0) && !clickLock) // ���콺 ��Ŭ�� �������� ��
        {
            clickTime = Time.time;
            repeaterLock = true; // ���� Ȱ��ȭ
            Shot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickTime < pInterval.currentValue) clickLock = true;
            repeaterLock = false; // ���콺 ��Ŭ������ ���� ����
        }
    }
    
    public float GetAttack()
    {
        return pDamage.currentValue;
    }

    // ����
    public void Aiming()
    {
        var layerMask = (1 << LayerMask.NameToLayer("Terrain")) + (1 << LayerMask.NameToLayer("AimPoint")); ;
        var cameraState = CameraManager.cameraState; // ī�޶� ���°�
        var playerPosition = transform.position;

        // ȭ�� ���� ���콺 ������ ��ġ�� ����ĳ����
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000, layerMask))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                if (cameraState == 0) targetPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z); // ��� �����Ǽ��� ����
                else if (cameraState == 1) targetPoint = new Vector3(hit.point.x, playerPosition.y, hit.point.z); // ž�� �����Ǽ��� ����
                else if (cameraState == 2) targetPoint = new Vector3(playerPosition.x, hit.point.y, hit.point.z); // ���̵�� �����Ǽ��� ����
            }
        }
    }

    // �߻�
    public void Shot()
    {
        StartCoroutine("ShotCoroutine");
    }

    IEnumerator ShotCoroutine()
    {
        if (hasbullets == 0)  // ������ �ִ� ź�� �⺻�϶�
        {
            while (repeaterLock)
            {
                var targetBullet = ObjectManager.instance.GetBullet("DirectBullet");
                var attackSpeedToBullet = targetBullet.GetComponent<DirectBullet>().GetAttackSpeedToBullet();
                SetBullet(targetBullet, "DirectBullet"); // źȯ �߻�
                yield return new WaitForSeconds(attackSpeedToBullet + pInterval.currentValue); // ����ð���ŭ ���
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
                yield return new WaitForSeconds(pInterval.currentValue); // ����ð���ŭ ���
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
                yield return new WaitForSeconds(pInterval.currentValue); // ����ð���ŭ ���
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
                yield return new WaitForSeconds(pInterval.currentValue); // ����ð���ŭ ���
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
                yield return new WaitForSeconds(pInterval.currentValue); // ����ð���ŭ ���
                if (hasbullets != 4)
                    repeaterLock = false;
            }
        }
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        var bulletTf = bullet.transform; // źȯ�� transform��
        var playerTf = this.transform; // �÷��̾��� transform��
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z + 3); // ��ġ ����
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
        else if (name == "ChaserBullet")
        {
            bullet.GetComponent<ChaserBullet>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }

    // źȯ �Ҹ� ī��Ʈ ����
    private void SetBulletCount()
    {
        if (hasbullets == 0)
        {
            contentText.text = "X ��";
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
    // ������ ������Ʈ ����
    private void OnTriggerStay(Collider other)  // �������� ������ ���
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
    // ������ ������Ʈ ����
    private void OnTriggerExit(Collider other)  // �������� �������� ���� ���
    {
        if (other.tag == "BulletItem")
            nearObject = null;

        if (other.tag == "Item")
            nearObject = null;

    }
}
