using UnityEngine;

public class Sara : MonoBehaviour
{
    private Vector3 position = Vector3.zero; // 목표 지점 벡터 값, 이 값으로 오브젝트가 움직인다.
    private Transform player; // 플레이어 좌표를 가져오기 위한 오브젝트
    private Transform enemyTransform; // 자기 자신의 좌표를 저장
    private float movementSpeed; // 이동속도
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

    // 이동 메소드
    private void move()
    {
        enemyTransform.LookAt(player); // 플레이어를 바라보게 한다.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // 현재 좌표에서 목표 좌표로 기제된 속도로 이동
    }

    // 플레이어 기준 목표 좌표 변경 메소드
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // 선형이동
    void LinearMovement(bool directionFlag)
    {
        if (directionFlag)
        {
            ChangeTargetToPlayer(-50, Random.Range(-10, 11), -30); // 플레이어 시점 앞에서 왼쪽으로 이동하기, 플레이어 시야 밖으로
        }
        else
        {
            ChangeTargetToPlayer(50, Random.Range(-10, 11), -30); // 플레이어 시점 앞에서 오른쪽으로 이동하기, 플레이어 시야 밖으로
        }
    }
}
