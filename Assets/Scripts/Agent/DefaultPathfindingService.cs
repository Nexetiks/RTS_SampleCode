using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public class DefaultPathfinding : IPathfinding
    {
        public List<Vector3> GetPath(Vector3 start, Vector3 end)
        {
            return new List<Vector3>
            {
                end
            };
        }
    }
}