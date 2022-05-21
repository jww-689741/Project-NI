using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// �׽�Ʈ �Ϸ� �� Debug.Log ��� �����ֱ� �ٶ�

public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    private bool pauseFlag = false;

    // UI Ȱ��ȭ & ��Ȱ��ȭ
    public void ActivateUI(GameObject target)
    {
        target.SetActive(true);
    }

    // UI ��Ȱ��ȭ
    public void DisableUI(GameObject target)
    {
        target.SetActive(false);
    }

    // �� ��ȯ
    public void SceneTransition(string target)
    {
        SceneManager.LoadScene(target);
    }

    // ���� ����
    public void QuitApplication()
    {
        Application.Quit();
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

    // �Ͻ�����
    private void Pause(GameObject target)
    {
        if (!pauseFlag)
        {
            Time.timeScale = 0;
            target.SetActive(true);
            pauseFlag = true;
        }
        else
        {
            Time.timeScale = 1;
            target.SetActive(false);
            pauseFlag = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(pauseUI);
        }
    }

}
