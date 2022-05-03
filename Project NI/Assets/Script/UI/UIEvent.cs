using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// �׽�Ʈ �Ϸ� �� Debug.Log ��� �����ֱ� �ٶ�

public class UIEvent : MonoBehaviour
{
    public GameObject gameQuitPopUp;
    public GameObject settingWindow;
    public GameObject upgradeWindow;

    // �˾� Ȱ��ȭ
    public void PopUpActivation()
    {
        gameQuitPopUp.SetActive(true);
    }

    // �˾� �ݱ� ��ư
    public void PopUpExit()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false); // Ŭ���� ��ư�� �θ� UI�� ��Ȱ��ȭ
    }

    // ������ ��ư
    public void QuitGame()
    {
        Application.Quit(); // ���� ����
    }

    // ������ ���
    public void QuitGameCancle()
    {
        gameQuitPopUp.SetActive(false);
    }

    public void SettingActivation()
    {
        settingWindow.SetActive(true);
    }

    public void UpgradeActivation()
    {
        upgradeWindow.SetActive(true);
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
    }

}
