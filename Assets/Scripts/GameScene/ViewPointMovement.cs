using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中のカメラの視点移動
/// </summary>
public class ViewPointMovement : MonoBehaviour
{
    [SerializeField] Transform m_player = null;         //プレイヤー
    [SerializeField] Transform m_playerPivot = null;    //プレイヤーの基点

    //カメラ上下移動の最大、最小角度
    [Range(-0.999f, -0.5f)]
    float m_maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    float m_minYAngle = 0.5f;


    void Update()
    {
        //マウスのX,Y軸がどれほど移動したかを取得する
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");

        //Y軸を更新する(プレイヤーを回転)
        //取得したX軸の変更をプレイヤーのY軸に反映する
        m_player.transform.Rotate(0, x_Rotation, 0);

        //Y軸の設定
        float nowAngle = m_playerPivot.transform.localRotation.x;
        //最大値、または最小値を超えた場合、カメラをそれ以上動かないようにする
        //プレイヤーの中身が見えたり、カメラが一回転しないようにするのを防ぐ
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
        //操作しているとZ軸がだんだん動いていくので、0に設定する。
        m_playerPivot.eulerAngles = new Vector3(m_playerPivot.eulerAngles.x, m_playerPivot.eulerAngles.y, 0.0f);
    }
}
