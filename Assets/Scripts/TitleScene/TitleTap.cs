using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g����ʂ��^�b�v���ꂽ��w�c�I������V�[���֑J��
/// </summary>
public class TitleTap : MonoBehaviour
{
    //���O���߃V�[���ɑJ�ڂ��邩�ǂ�������
    bool m_isGoDecideNameScene = false;

    //���Z�b�g�{�^��
    [SerializeField]GameObject m_resetButtonObject = null;

    void Start()
    {
        //FirstPlay�̃L�[�����݂��Ȃ��ꍇ�̓V�[���J�ڐ�𖼑O���߃V�[���ɍs���悤�ɂ���
        if(!PlayerPrefs.HasKey("FirstPlay"))
        {
            m_isGoDecideNameScene = true;
            //FirstPlay�̃L�[�ɒl�����邱�ƂŁA��x�Ƃ��̃l�X�g�������s���Ȃ��悤�ɂ���
            PlayerPrefs.SetInt("FirstPlay", 1);
        }

        //FPS��30�ɌŒ�
        Application.targetFrameRate = 30;
        //�E�B���h�E�T�C�Y��ݒ�
        //(�t���X�N���[����1/4�T�C�Y�ŃE�B���h�E�\��)
        Screen.SetResolution(1920/4, 1080/4, false, 60);
    }

    void Update()
    {
        //���Z�b�g�{�^���������ꂽ�Ƃ��͉�ʃ^�b�v������s��Ȃ�
        //EventSystem.current.IsPointerOverGameObject()�̓{�^�����^�b�v���ꂽ��true��Ԃ��֐�
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //��ʃ^�b�v���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //�Q�[������v���C���A
            if (m_isGoDecideNameScene)
            {
                //���O���߃V�[���ɑJ��
                SceneManager.LoadScene("DecideNameScene");
            }
            //�Q�[���v���C�P��ڈȍ~
            else
            {
                //�w�c���߃V�[���ɑJ��
                SceneManager.LoadScene("CampSelectScene");
            }
        }
    }

    //�{�^������������f�[�^������������֐�
    public void OnClickDataReset()
    {
        m_isGoDecideNameScene = true;
        //FirstPlay�̃L�[�ɒl�����邱�ƂŁA��x�Ƃ��̃l�X�g�������s���Ȃ��悤�ɂ���
        PlayerPrefs.SetInt("FirstPlay", 1);

        //�P��{�^����������{�^���������ĂQ��ډ�����Ȃ��悤�ɂ���B
        m_resetButtonObject.SetActive(false);

        //�f�o�b�N
        Debug.Log("�f�[�^�����Z�b�g���܂����B");
    }
}