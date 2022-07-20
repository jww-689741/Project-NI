using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �׽�Ʈ �Ϸ� �� Debug.Log ��� �����ֱ� �ٶ�

public class StageUIEvent : MonoBehaviour
{
    public GameObject popup; // �˾� ������

    // ������ ��ư
    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    // ������ ���
    public void QuitGameCancle()
    {
        Debug.Log("Quit Cancle");
        popup.SetActive(false);
    }

    // �������� �̾��ϱ�
    public void StageContinue()
    {
        Debug.Log("Stage Continue");
    }

    // �������� �ٽ��ϱ�
    public void StageRestart()
    {
        Debug.Log("Stage Restart");
    }

    // �������� ����
    public void StageQuit()
    {
        Debug.Log("Stage Quit");
        popup.SetActive(true);
    }

    // ���� �ɼ�
    public void Settings()
    {
        Debug.Log("Settings");
    }
}
