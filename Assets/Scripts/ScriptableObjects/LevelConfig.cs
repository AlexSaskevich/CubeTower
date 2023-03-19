using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Create Level Config")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _cubeCount;

    public int CubeCount => _cubeCount;
}