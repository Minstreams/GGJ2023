using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IceEngine
{
    public class Enemy : Hurtable
    {
        public MapObject target;
        public float speed = 1;

        List<Vector2Int> path = new List<Vector2Int>();

        void Update()
        {
            if (path.Any() && Pos == path[0]) path.RemoveAt(0);
            if (path.Any())
            {
                var vTarget = path[0].ToWorldPos() - transform.position;
                int w = Ice.Gameplay.map.Width;
                int h = Ice.Gameplay.map.Height;
                while (vTarget.x > w * 0.5f) vTarget.x -= w;
                while (vTarget.x < -w * 0.5f) vTarget.x += w;
                while (vTarget.y > h * 0.5f) vTarget.y -= h;
                while (vTarget.y < -h * 0.5f) vTarget.y += h;

                transform.position += vTarget.normalized * Time.deltaTime * speed;
            }

            if (target != null)
            {
                Astar.FindingPath(Pos, target.Pos, path);
            }
        }
    }
}
