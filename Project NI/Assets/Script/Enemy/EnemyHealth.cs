using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject hit; // �ǰ� ȿ��
    public GameObject explosion; // �ı� ȿ��
    public int score; // ���ھ�

    private Vector3 position = Vector3.zero; 
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private float currentHp; // ���� HP���� �ʵ�
    private string itemToDrop;
    private float playerDamageCoefficient; // �÷��̾� ������ ���
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
        playerDamageCoefficient = player.GetComponent<PlayerStatusManager>().GetAttackDamage() / 100; // �÷��̾� ������ ��� ��������, /100�� ���� �÷��̾� ����� ����� ������ Ȯ���� �ٲٴ� �۾�
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

    // �� ��ü �� ����
    void CheckCameraPosition()
    {
        Vector3 enemyPosition = enemyTransform.position;

        if (CameraManager.cameraState == 0) // ���
        {
            
        }
        else if (CameraManager.cameraState == 1) // ž��
        {
            transform.position = new Vector3(enemyPosition.x, player.position.y, enemyPosition.z); // y �� �÷��̾� ��ǥ�� ����
            position = new Vector3(position.x, player.position.y, position.z);
        }
        else if (CameraManager.cameraState == 2) // ���̵��
        {
            transform.position = new Vector3(player.position.x, enemyPosition.y, enemyPosition.z); // x �� �÷��̾� ��ǥ�� ����
            position = new Vector3(player.position.x, position.y, position.z);
        }
    }

    // �÷��̾� ���� 20��ŭ �ڷ� ���� ��Ȱ��ȭ
    private void CheckActiveCondition()
    {
        Vector3 enemyPosition = enemyTransform.position; // �ڱ� �ڽ��� ��ǥ�� ����

        if (enemyPosition.z < player.position.z - 20)
        {
            this.gameObject.SetActive(false);
        }
    }

    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        var saraStatus = GetComponent<SaraStatusManager>(); // ����� ������ ������
        var DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>(); // ���� ��ũ��Ʈ ��������
        var HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>(); // ��� ��ũ��Ʈ ��������

        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false); // �浹�� źȯ ������Ʈ ��Ȱ��ȭ

            // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
            if (DirectBulletStatus != null)
            {
                Instantiate(hit, this.transform.position, this.transform.rotation); // �ǰ� ȿ��
                currentHp -= Calculation(DirectBulletStatus.GetAttackDamage(), playerDamageCoefficient, saraStatus.GetDefense());
            }
            else if (HowitzerBulletStatus != null)
            {
                currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
            }
        }
        else if (other.CompareTag("Missile"))
        {
            var MissleStatus = GetComponent<MissileStatusManager>(); // �̻��� ��ũ��Ʈ ��������

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
            Instantiate(explosion, this.transform.position, this.transform.rotation); // �ı� ȿ��

            DropItem();
        }
    }

    float Calculation(float hitDamage, float damageCoefficient, float defense) // ź ������, ������ ���, ����
    {
        // ������ ������ �氨���� ���, �������� �÷��̾� ��� ��ŭ ���ϴ� ���
        return (1 - defense / 100) * (hitDamage + (hitDamage * damageCoefficient)); // (1 - �氨��) * (������ + (������ * ���))
    }


    void DropItem()
    {
        if (ItemDropProbability())
        {
            if (itemToDrop == null || itemToDrop == "")
                return;
            else
            {
                GameObject item = ObjectManager.instance.GetItem(itemToDrop); // ������ �޾ƿ���

                item.transform.position = this.transform.position; // �ش� ������Ʈ�� ��ġ�� ������ ����
                item.SetActive(true); // ������ Ȱ��ȭ
            }
        }
    }

    // ������ ��� Ȯ��
    bool ItemDropProbability()
    {
        // var itemStatus = GetComponent<>();
        bool persentage = Random.value <= 0.2; // �ӽ� 20%
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