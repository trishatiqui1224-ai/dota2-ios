using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dota.Gameplay
{
    [Serializable]
    public class Camp
    {
        public string name;
        public int type;
        public Vector3 position;
        public float yaw;
        public int minType;
        public int maxType;
        public int forced;
        public int maxUpgrades;
        public int aggroType;
        public int pullType;
        public int batchLimit;
        public Bounds box;
    }

    public class NeutralCamps : MonoBehaviour
    {
        public List<Camp> camps = new List<Camp>();
        public float firstSpawn = 60f;
        public float interval = 60f;

        public static NeutralCamps Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
        }
    }
}
