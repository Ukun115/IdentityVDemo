using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�E�g�Q�[���Ō��߂����[�U�[�̃f�[�^��ۑ�����ꏊ
/// </summary>
public class UserSettingData : MonoBehaviour
{
    //�T�o�C�o�[�w�c���ǂ���
    bool m_survivorCamp = true;
    //���[�U�[��ID
    int m_id = 0;
    //���[�U�[���[��
    string m_role = "���[����";

    void Start()
    {
        //�V�[���J�ڂ��Ă����̃Q�[���I�u�W�F�N�g���j�󂳂�Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
            Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }

    //�T�o�C�o�[�w�c���ǂ����̃v���p�e�B
    public bool GetSetIsSurvivorCamp
    {
        get { return m_survivorCamp; }
        set { m_survivorCamp = value; }
    }

    //���[�U�[ID�̃v���p�e�B
    public int GetSetPlayerId
    {
        get { return m_id; }
        set { m_id = value; }
    }

    //���[�U�[���[���̃v���p�e�B
    public string GetSetPlayerRole
    {
        get { return m_role; }
        set { m_role = value; }
    }
}