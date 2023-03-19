using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    private void Start()
    {
        var cubes = FindObjectsOfType(typeof(Cube));
        Debug.Log(cubes.Length);
    }
}