using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    [CreateAssetMenu(fileName = "New Level Data", menuName = "Data/Level Data", order = 1)]
    public class LevelDataSO : ScriptableObject
    {
        public int GroundCount;
        public float LevelForwardSpeed;
        public float MinDistBetweenGrounds;
        public float MaxDistBetweenGrounds;

        // Will be used with object pooling.
        public List<GameObject> planePrefbs;

    }

}
