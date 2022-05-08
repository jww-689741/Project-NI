using UnityEngine;

public class Sara : MonoBehaviour
{
    public float movementSpeed; // �̵��ӵ�
    public GameObject explosion; // �ı� ȿ��
    public GameObject hit; // �ǰ� ȿ��

    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private float currentHp; // ���� HP���� �ʵ�

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()    
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position; // �ʱ� ��ǥ�� ��ǥ ��ǥ�� �Է�, ������ �����ϱ� ���� �ʿ�
        currentHp = status.GetHP();
        movementSpeed = status.GetMoveSpeed();
        LinearMovement(Random.value > 0.5f);
    }

    void Update()
    {
        //move();
        CheckHP();
        CheckCameraPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����� ������ ������
        var saraStatus = GetComponent<SaraStatusManager>();
        // �� �߻�ü ���� ��������
        // ������Ʈ���� �޶��� �� ����
        var DirectBulletStatus = other.GetComponent<DirectBulletStatusManager>();
        var HowitzerBulletStatus = other.GetComponent<HowitzerBulletStatusManager>();

        if(other.gameObject.transform.parent != null) // �θ� ������Ʈ�� ���� ��
        {
            Transform parent = other.gameObject.transform.parent; // �θ� ������Ʈ ��������

            DirectBulletStatus = parent.GetComponent<DirectBulletStatusManager>(); // �θ� ������Ʈ ��ũ��Ʈ ��������
            HowitzerBulletStatus = parent.GetComponent<HowitzerBulletStatusManager>(); // �θ� ������Ʈ ��ũ��Ʈ ��������

            if (parent.CompareTag("FX")) // �θ��� �±װ� FX�� ��
            {
                other.gameObject.SetActive(false); // �浹�� źȯ ������Ʈ ��Ȱ��ȭ

                // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
                if (DirectBulletStatus != null)
                {
                    Instantiate(hit, this.transform.position, this.transform.rotation);
                    currentHp -= (DirectBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
                else if (HowitzerBulletStatus != null)
                {
                    currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                    Debug.Log(currentHp);
                }
            }
        }
        else if (other.CompareTag("FX"))
        {
            other.gameObject.SetActive(false); // �浹�� źȯ ������Ʈ ��Ȱ��ȭ

            // �ǰݴ��� �߻�ü�� ���ݷ� - �÷��̾��� ���¸�ŭ�� �������� ���� ü�¿��� ����
            if (DirectBulletStatus != null)
            {
                Instantiate(hit, this.transform.position, this.transform.rotation);
                currentHp -= (DirectBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                Debug.Log(currentHp);
            }
            else if (HowitzerBulletStatus != null)
            {
                currentHp -= (HowitzerBulletStatus.GetAttackDamage() - saraStatus.GetDefense());
                Debug.Log(currentHp);
            }
        }
    }

    // �̵� �޼ҵ�
    private void move()
    {
        enemyTransform.LookAt(player); // �÷��̾ �ٶ󺸰� �Ѵ�.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // ���� ��ǥ���� ��ǥ ��ǥ�� ������ �ӵ��� �̵�
    }

    // �÷��̾� ���� ��ǥ ��ǥ ���� �޼ҵ�
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // �����̵�
    void LinearMovement(bool directionFlag)
    {
        if (directionFlag)
        {
            ChangeTargetToPlayer(-50, -5, 5); // �÷��̾� ���� �տ��� �������� �̵��ϱ�, �÷��̾� �þ� ������
        }
        else
        {
            ChangeTargetToPlayer(50, -5, 5); // �÷��̾� ���� �տ��� ���������� �̵��ϱ�, �÷��̾� �þ� ������
        }
    }

    // ü���� 0 ���� �� �� ������Ʈ ��Ȱ��ȭ
    void CheckHP()
    {
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            // �ı� ȿ��
            Instantiate(explosion, this.transform.position, this.transform.rotation);
        }
    }

    void CheckCameraPosition()
    {
        if(CameraManager.cameraState == 0) // ���
        {
            // �ƹ��͵� ����
        } 
        else if (CameraManager.cameraState == 1) // ž��
        {
            transform.position = new Vector3(enemyTransform.position.x, player.position.y, enemyTransform.position.z); // y �� �÷��̾� ��ǥ�� ����
            position = new Vector3(position.x, player.position.y, position.z);
        }
        else if (CameraManager.cameraState == 2) // ���̵��
        {
            transform.position = new Vector3(player.position.x, enemyTransform.position.y, enemyTransform.position.z); // x �� �÷��̾� ��ǥ�� ����
            position = new Vector3(player.position.x, position.y, position.z);
        }
    }
}
