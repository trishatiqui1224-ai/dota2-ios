using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dota.Gameplay
{
    [Serializable]
    public class Lane
    {
        public string name;
        public int team;
        public Vector3 spawn;
        public List<Vector3> path = new List<Vector3>();
    }

    [Serializable]
    public class CreepDef
    {
        public string npc;
        public int team;
        public int ranged;
    }

    public class LaneSpawner : MonoBehaviour
    {
        public List<Lane> lanes = new List<Lane>();
        public List<CreepDef> creeps = new List<CreepDef>();
        public float waveInterval = 30f;
        public int maxPerLane = 12;
        public bool spawnOnStart = true;
        public bool Paused = false;

        private void Start()
        {
        }
    }
}
