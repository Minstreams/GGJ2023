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
                transform.position += Map.GetDirection(path[0].ToWorldPos() - transform.position) * Time.deltaTime * speed;
            }

            if (target != null)
            {
                Astar.FindingPath(Pos, target.Pos, path);
            }
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            using (new GizmosColorScope(Color.cyan))
            {
                Vector2Int lastPos = Pos;
                foreach (var p in path)
                {
                    Gizmos.DrawLine(p.ToWorldPos() + Vector3.one * 0.5f, lastPos.ToWorldPos() + Vector3.one * 0.5f);
                    lastPos = p;
                }
            }
        }
    }
}
