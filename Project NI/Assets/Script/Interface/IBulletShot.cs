using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 본 인터페이스는 탄환 발사와 관련된 추상메소드를 정의 */
public interface IBulletShot
{
<<<<<<< HEAD
    IEnumerator Shot(); // 발사 코루틴으로 선택적으로 회전 방향값을 매개변수로 삽입 가능
    public float GetAttackSpeedToBullet(); // 플레이어의 공격 로직에서 공격 속도를 연산하기 때문에 탄환의 공격속도 값을 넘겨주는 메소드
    public float GetAttackDamageToBullet(); // 탄환의 공격력값 반환
=======
    IEnumerator Shot(Vector3 directionVector); // 발사 코루틴으로 선택적으로 회전 방향값을 매개변수로 삽입 가능
    public float GetAttackSpeedToBullet(); // 플레이어의 공격 로직에서 공격 속도를 연산하기 때문에 탄환의 공격속도 값을 넘겨주는 메소드
>>>>>>> origin/Pks
}
