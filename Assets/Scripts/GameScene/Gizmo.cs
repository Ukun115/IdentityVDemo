using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour
{

	//�M�Y���̃T�C�Y
	[SerializeField] float gizmoSize = 0.3f;
	//�M�Y���̐F
	[SerializeField] Color gizmoColor = Color.yellow;

	void OnDrawGizmos()
	{
		//�M�Y���̃J���[��ݒ�
		Gizmos.color = gizmoColor;
		//�M�Y���̋��̂�`��
		Gizmos.DrawWireSphere(transform.position, gizmoSize);
	}
}