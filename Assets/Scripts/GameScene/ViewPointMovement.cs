using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����̃J�����̎��_�ړ�
/// </summary>
public class ViewPointMovement : MonoBehaviour
{
    [SerializeField] Transform m_player = null;         //�v���C���[
    [SerializeField] Transform m_playerPivot = null;    //�v���C���[�̊�_

    //�J�����㉺�ړ��̍ő�A�ŏ��p�x
    [Range(-0.999f, -0.5f)]
    float m_maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    float m_minYAngle = 0.5f;


    void Update()
    {
        //�}�E�X��X,Y�����ǂ�قǈړ����������擾����
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");

        //Y�����X�V����(�v���C���[����])
        //�擾����X���̕ύX���v���C���[��Y���ɔ��f����
        m_player.transform.Rotate(0, x_Rotation, 0);

        //Y���̐ݒ�
        float nowAngle = m_playerPivot.transform.localRotation.x;
        //�ő�l�A�܂��͍ŏ��l�𒴂����ꍇ�A�J����������ȏ㓮���Ȃ��悤�ɂ���
        //�v���C���[�̒��g����������A�J���������]���Ȃ��悤�ɂ���̂�h��
        if (-y_Rotation != 0)
        {
            if (0 < y_Rotation)
            {
                if (m_minYAngle <= nowAngle)
                {
                    m_playerPivot.transform.Rotate(-y_Rotation, 0, 0);
                }
            }
            else
            {
                if (nowAngle <= m_maxYAngle)
                {
                    m_playerPivot.transform.Rotate(-y_Rotation, 0, 0);
                }
            }
        }
        //���삵�Ă����Z�������񂾂񓮂��Ă����̂ŁA0�ɐݒ肷��B
        m_playerPivot.eulerAngles = new Vector3(m_playerPivot.eulerAngles.x, m_playerPivot.eulerAngles.y, 0.0f);
    }
}
