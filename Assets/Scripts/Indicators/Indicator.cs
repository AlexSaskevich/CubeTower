using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    protected virtual void ShowIndicator(Vector3 target, float hideDistance)
    {
        Vector3 direction = CalculateDirection(target, transform.position);

        if (CheckHideDistance(direction, hideDistance) == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        transform.GetChild(0).gameObject.SetActive(true);
        float angle = CalculateAngle(direction);
        SetRotation(angle);
    }

    private Vector3 CalculateDirection(Vector3 to, Vector3 from)
    {
        return to - from;
    }

    private bool CheckHideDistance(Vector3 direction, float hideDistance)
    {
        return direction.magnitude > hideDistance;
    }

    private float CalculateAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }

    private void SetRotation(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}