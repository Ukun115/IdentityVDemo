using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTable : MonoBehaviour
{
    //��E�\���\������Ă��邩�ǂ���
    bool m_onRoleTable = false;
    //��E�\���X���C�h�ł����Ԃ��ǂ���
    bool m_isSlide = false;
    //��E�\�X���C�h�͈�(Min)
    const float c_minSlideRange = 80.0f;
    //��E�\�X���C�h�͈�(Max)
    const float c_maxSlideRange = 1100.0f;
    //�X���C�h�̑��x
    const float c_slideSpeed = 40.0f;
    //
    [SerializeField] GameObject m_roleTableGameObject = null;

    void Update()
    {
        //�X���C�h���ł�����
        if(m_isSlide)
        {
            //��E�\��\���I
            if(m_onRoleTable)
            {
                if (m_roleTableGameObject.transform.position.x < c_maxSlideRange)
                {
                    //�E�ɃX���C�h
                    m_roleTableGameObject.transform.position += new Vector3(c_slideSpeed,0.0f,0.0f);
                }
                else
                {
                    //�X���C�h�ł��Ȃ���Ԃɂ���
                    m_isSlide = false;
                }
            }
            //��E�\�����܂��I
            else
            {
                if (m_roleTableGameObject.transform.position.x > c_minSlideRange)
                {
                    //���ɃX���C�h
                    m_roleTableGameObject.transform.position -= new Vector3(c_slideSpeed, 0.0f, 0.0f);
                }
                else
                {
                    //�X���C�h�ł��Ȃ���Ԃɂ���
                    m_isSlide = false;
                }
            }
        }
    }

    //��E�\�̃{�^�����������Ƃ��ɌĂ΂��֐�
    //�@�\�F��E�\���X���C�h������
    public void OnClickRoleTableButton()
    {
        //�f�o�b�N
        Debug.Log("��E�\�̃X���C�h�{�^���������ꂽ");

        //��E�\���\������Ă���Ƃ��A
        if(m_onRoleTable)
        {
            //��E�\�����܂�
            m_onRoleTable = !m_onRoleTable;
            m_isSlide = true;

        }
        //��E�\���\������Ă��Ȃ��Ƃ��A
        else
        {
            //��E�\��\��������
            m_onRoleTable = !m_onRoleTable;
            m_isSlide = true;
        }
    }
}
