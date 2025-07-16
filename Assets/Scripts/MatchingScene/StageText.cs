using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageText : MonoBehaviour
{
    //�X�e�[�W��
    [SerializeField] string[] m_stageName = { "" };
    //�X�e�[�W���e�L�X�g
    [SerializeField] Text m_stageNameText = null;
    //�����_���Ō��肵���X�e�[�W�ԍ�
    int m_stageNumber = 0;

    void Start()
    {
        //�X�e�[�W�������_���Ō���
        m_stageNumber = Random.Range(0, m_stageName.Length);
        m_stageNameText.text = m_stageName[m_stageNumber];
    }
}