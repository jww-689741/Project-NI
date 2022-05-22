using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject hit; // �ǰ� ȿ��
    public GameObject explosion; // �ı� ȿ��
    public float repeaterInterval = 2f;
    public GameObject LaserEffect; // ������ ������Ʈ

    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private float currentHp; // ���� HP���� �ʵ�
    private float timer = 0; // �� ���� ���� �ִ� Ÿ�̸�
    public static int score; // ���ھ�
    private float attackTime = 5f; //������ ���� �ð�
    private float attackTimeCalc = 5f; //������ ���� �ð� ���

    private delegate void Control();
    Control control;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<BossStatusManager>();
        currentHp = status.GetHP();
        score = 0;
        player = GameObject.FindWithTag("Player").transform;

        control += CheckCameraPosition;
        control += CheckActiveCondition;

        StartCoroutine(LaserOff());
    }

    void Update()
    {
        control();

        if (enemyTransform.position.z > player.position.z) // ���� �÷��̾� ���� ���濡 ���� ���� ���� ����
        {
            if (timer > repeaterInterval)
            {
                StartCoroutine("Repeater", "DirectBullet");
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    IEnumerator LaserOff ()   // ������ OFF
    { 
        while(true)
        {
            yield return null;
            if(LaserEffect.activeInHierarchy)
            {
                attackTimeCalc -= Time.deltaTime;
                if(attackTimeCalc <= 0)
                {
                    attackTimeCalc = attackTime;
                    LaserEffect.SetActive(false);
                }
            }
        } 
    }


    // �� ��ü �� ����
    void CheckCameraPosition()
    {
        Vector3 enemyPosition = enemyTransform.position; // �ڱ� �ڽ��� ��ǥ�� ����

        if (CameraManager.cameraState == 0) // ���
        {
            // �ƹ��͵� ����, Ȥ�� ���� �־�� 
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
        // ����� ������ ������
        var bossStatus = GetComponent<BossStatusManager>();
        // �� �߻�ü ���� ��������
        // ������Ʈ���� �޶��� �� ����
        Transform parent = other.gameObject.transform.parent; // �θ� ������Ʈ ��������
        var DirectBulletStatus = parent.GetComponent<DirectBulletStatusManager>(); // �θ� ������Ʈ ��ũ��Ʈ ��������
        var HowitzerBulletStatus = parent.GetComponent<HowitzerBulletStatusManager>(); // �θ� ������Ʈ ��ũ��Ʈ ��������
        var MissleStatus = parent.GetComponent<MissileStatusManager>(); // �θ� ������Ʈ ��ũ��Ʈ ��������

        if (other.gameObject.transform.parent != null) // �θ� ������Ʈ�� ���� ��
        {
            if (parent.CompareTag("Bullet") || parent.CompareTag("Missile")) // �θ��� �±װ� FX�� ��
            {
                other.gameObject.SetActive(false); // �浹�� źȯ ������Ʈ ��Ȱ��ȭ

                // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
                if (DirectBulletStatus != null)
                {
                    Instantiate(hit, this.transform.position, this.transform.rotation);
                    currentHp -= (DirectBulletStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (HowitzerBulletStatus != null)
                {
                    currentHp -= (HowitzerBulletStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (MissleStatus != null)
                {
                    currentHp -= (MissleStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
            }
            else if (other.CompareTag("Bullet") || parent.CompareTag("Missile"))
            {
                DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
                HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();

                other.gameObject.SetActive(false); // �浹�� źȯ ������Ʈ ��Ȱ��ȭ

                // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
                if (DirectBulletStatus != null)
                {
                    Instantiate(hit, this.transform.position, this.transform.rotation);
                    currentHp -= (DirectBulletStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (HowitzerBulletStatus != null)
                {
                    currentHp -= (HowitzerBulletStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (MissleStatus != null)
                {
                    currentHp -= (MissleStatus.GetAttackDamage() - bossStatus.GetDefense());
                    Debug.Log(currentHp);
                }
            }
        }

        CheckHP();
    }

    void CheckHP()
    {
        var status = GetComponent<BossStatusManager>();
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            score += 6974;
            Debug.Log(score + "t");
            // �ı� ȿ��
            Instantiate(explosion, this.transform.position, this.transform.rotation);
        }
    }

    IEnumerator Repeater(string name)
    {
        SetBullet(ObjectManager.instance.GetBullet(name), name); // źȯ �߻�
        yield return new WaitForSeconds(repeaterInterval); // ����ð���ŭ ���
    }

    // źȯ ������Ʈ ��ġ ����, ȸ������ ����, ������Ʈ Ȱ��ȭ, ���� �߻� ���� �۵�
    private void SetBullet(GameObject bullet, string name)
    {
        if (bullet == null) return; // �޾ƿ� źȯ�� ���� ��� ��ȯ

        Vector3 bulletPosition = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f));

        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (this.transform.position.z - 1.5f)); // ��ġ ����
        var directionVector = (player.position - bulletPosition).normalized; // źȯ�� �߻� �� ���⺤�� ����
        bullet.transform.localRotation = Quaternion.LookRotation(directionVector);
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
                bullet.transform.GetChild(i).transform.position = bullet.transform.position;  //�ڽ� ������Ʈ �θ�� ������ ��ġ�� �̵�
            }
            bullet.GetComponent<BuckShot>().StartCoroutine("Shot", directionVector); // źȯ ���� ���� �ڷ�ƾ ����
        }
    }
}
