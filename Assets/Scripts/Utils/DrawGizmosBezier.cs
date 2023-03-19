using UnityEngine;

public class DrawGizmosBezier : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _targetJumpPoint;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        int sigmentsNumber = 20;
        Vector3 preveousePoint = _startPoint.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float parameter = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(_startPoint.position, _p1.position, _p2.position, _targetJumpPoint.position, parameter);

            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }
    }
}
