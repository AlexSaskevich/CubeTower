using UnityEngine;

public class Cube : MonoBehaviour
{
    public float Size { get; private set; }

    private void Start()
    {
        Size = transform.GetChild(0).localScale.x * 2;
    }
}