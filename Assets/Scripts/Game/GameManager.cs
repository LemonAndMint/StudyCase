using Data;
using Level;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelDataSO _data;
    public LevelDataSO LevelData{ get => _data; }

    public GroundManager groundManager;

}
