using UnityEngine;

public class Sara : MonoBehaviour
{
    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private float movementSpeed; // �̵��ӵ�
    private bool direction;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position;
        movementSpeed = status.GetMoveSpeed();
        direction = Random.value > 0.5f;
    }

    void Update()
    {
        move();
        LinearMovement(direction);
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
            ChangeTargetToPlayer(-50, Random.Range(-10, 11), -30); // �÷��̾� ���� �տ��� �������� �̵��ϱ�, �÷��̾� �þ� ������
        }
        else
        {
            ChangeTargetToPlayer(50, Random.Range(-10, 11), -30); // �÷��̾� ���� �տ��� ���������� �̵��ϱ�, �÷��̾� �þ� ������
        }
    }
}
