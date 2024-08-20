using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public interface IPathfinding
    {
        public List<Vector3> GetPath(Vector3 start, Vector3 end);
    }
}