using UnityEngine;

namespace Data
{
    
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data", order = 0)]
    public class PlayerDataSO : ScriptableObject
    {

        public float SideMoveSpeed;
        public float JumpStrength;

    }

}
