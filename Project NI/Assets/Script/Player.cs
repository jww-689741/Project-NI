using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;  //이동 스피드
    public float AttackGap; // 총알이 발사되는 간격

    private Transform Vec; //카메라 벡터
    private Vector3 MovePos;  //플레이어 움직임에 대한 변수
    private bool ContinuouFire;

    void Init()
    {
        AttackGap = 0.2f;

        MovePos = Vector3.zero;
        ContinuouFire = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Vec = GameObject.Find("CameraVector").transform;
       
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        KeyCheck();
    }
    void Run()
    {
        int ButtonDown = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) ButtonDown = 1;
        if (Input.GetKey(KeyCode.RightArrow)) ButtonDown = 1;
        if (Input.GetKey(KeyCode.UpArrow)) ButtonDown = 1;
        if (Input.GetKey(KeyCode.DownArrow)) ButtonDown = 1;

        if (ButtonDown != 0)
            Rotation();
        else
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * Speed * ButtonDown);
    }

    void Rotation()
    {
        MovePos.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Quaternion q = Quaternion.LookRotation(Vec.TransformDirection(MovePos));

        if (MovePos != Vector3.zero)
            transform.rotation = q;
    }

    void KeyCheck()
    {
        if (Input.GetButtonDown("Jump"))
            StartCoroutine("NextFire");
        else if (Input.GetButtonUp("Jump"))
            ContinuouFire = false;
    }
    IEnumerator NextFire()
    {
        ContinuouFire = true;
        while (ContinuouFire)
        {
            // 총알을 리스트에서 가져온다.
            BulletInfoSetting(ObjManager.Call().GetObject("Bullet"));
            yield return new WaitForSeconds(AttackGap);                    // 시간지연.
        }
    }
    // 총알정보 셋팅.
    void BulletInfoSetting(GameObject _Bullet)
    {
        if (_Bullet == null) return;

        _Bullet.transform.position = transform.position;                // 총알의 위치 설정
        _Bullet.transform.rotation = transform.rotation;                // 총알의 회전 설정.
        _Bullet.SetActive(true);                                        // 총알을 활성화 시킨다.
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");    // 총알을 움직이게 한다.
    }


}
