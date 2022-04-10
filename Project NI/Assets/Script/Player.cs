using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;  //�̵� ���ǵ�
    public float AttackGap; // �Ѿ��� �߻�Ǵ� ����

    private Transform Vec; //ī�޶� ����
    private Vector3 MovePos;  //�÷��̾� �����ӿ� ���� ����
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
            // �Ѿ��� ����Ʈ���� �����´�.
            BulletInfoSetting(ObjManager.Call().GetObject("Bullet"));
            yield return new WaitForSeconds(AttackGap);                    // �ð�����.
        }
    }
    // �Ѿ����� ����.
    void BulletInfoSetting(GameObject _Bullet)
    {
        if (_Bullet == null) return;

        _Bullet.transform.position = transform.position;                // �Ѿ��� ��ġ ����
        _Bullet.transform.rotation = transform.rotation;                // �Ѿ��� ȸ�� ����.
        _Bullet.SetActive(true);                                        // �Ѿ��� Ȱ��ȭ ��Ų��.
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");    // �Ѿ��� �����̰� �Ѵ�.
    }


}
