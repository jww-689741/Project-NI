using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerChild : MonoBehaviour
{
    private void Update()
    {
        var status = GetComponentInParent<SpinnerBulletStausManager>(); // 부모의 스탯 컴포넌트를 가져옴
        var HoldingTime = 4; // 자탄의 발사방향
        for (int i = 0; i < HoldingTime; i++)
        {
            this.gameObject.transform.GetChild(i).Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed()); // 자탄 발사
        }
    }
}
