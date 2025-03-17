using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data", order = 0)]
    public class PlayerDataSO : ScriptableObject
    {

        public float SideMoveSpeed;
        public float JumpStrength;
        public float JumpCooldown;

    }

    [CreateAssetMenu(fileName = "New Level Data", menuName = "Data/Level Data", order = 1)]
    public class LevelDataSO : ScriptableObject
    {

        public float LevelForwardSpeed;
        //Will be used with object pooling.
        public List<GameObject> planePrefbs;

    }

}
