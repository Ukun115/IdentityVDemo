using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour
{

	//ギズモのサイズ
	[SerializeField] float gizmoSize = 0.3f;
	//ギズモの色
	[SerializeField] Color gizmoColor = Color.yellow;

	void OnDrawGizmos()
	{
		//ギズモのカラーを設定
		Gizmos.color = gizmoColor;
		//ギズモの球体を描画
		Gizmos.DrawWireSphere(transform.position, gizmoSize);
	}
}