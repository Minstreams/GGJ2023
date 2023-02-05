using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public class BuildingSightTower : Building
    {
        public static HashSet<BuildingSightTower> towerSet = new HashSet<BuildingSightTower>();
        public static void ReconnectAll()
        {
            foreach (var t in towerSet)
            {
                t.Reconnect();
            }
        }

        public LineRenderer lr;
        public Transform p0;

        protected override void OnBuilt()
        {
            towerSet.Add(this);
            ReconnectAll();
        }

        public BuildingSightTower ConnenctedTower { get; set; }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            towerSet.Remove(this);
            ReconnectAll();
        }

        public void Reconnect()
        {
            Disconnect();
            float minDis = float.MaxValue;
            BuildingSightTower minT = null;
            foreach (var t in towerSet)
            {
                if (t == this) continue;
                var d = Vector3.Distance(transform.position, t.transform.position);
                if (d < minDis && t.ConnenctedTower != this)
                {
                    minDis = d;
                    minT = t;
                }
            }

            if (minT != null) ConnectToTower(minT);
        }

        SimpleEvent onConnect;
        SimpleEvent onDisconnect;
        void Disconnect()
        {
            ConnenctedTower = null;
            lr.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            onDisconnect?.Invoke();
        }
        void ConnectToTower(BuildingSightTower t)
        {
            ConnenctedTower = t;
            lr.SetPositions(new Vector3[] { p0.position, t.p0.position });
            onConnect?.Invoke();
        }
    }
}
