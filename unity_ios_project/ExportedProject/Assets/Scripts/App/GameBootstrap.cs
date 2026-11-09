using System;
using System.Collections.Generic;
using UnityEngine;
using Dota.Client;

namespace Dota.App
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; set; }

        public enum GameMode
        {
            SoloVsBots = 0,
            CustomMatch = 1,
            HeroDemo = 2
        }

        public enum GameState
        {
            MainMenu = 0,
            HeroPicker = 1,
            InMatch = 2
        }

        public string mapName = "dota";
        public GameObject heroPrefab;
        public string heroName = "npc_dota_hero_nevermore";
        public int heroTeam = 2;
        public Transform spawnPoint;
        public DotaCameraRig cameraRig;
        public bool botEnabled = true;
        public string autoPick;
        public string autoBotPick;
        public int botDifficulty = 2;

        public bool Started { get; set; }
        public bool InMainMenu { get; set; } = true;
        public GameMode CurrentMode { get; set; } = GameMode.SoloVsBots;
        public GameState CurrentState { get; set; } = GameState.MainMenu;

        public GameObject playerHeroInstance;
        public GameObject debugUiCanvas;

        public static readonly string[] PlayableHeroes = new string[]
        {
            "npc_dota_hero_nevermore",
            "npc_dota_hero_pudge",
            "npc_dota_hero_sniper",
            "npc_dota_hero_skeletonking"
        };

        private void Awake()
        {
            Instance = this;
            InMainMenu = true;
            CurrentState = GameState.MainMenu;
        }

        private void Start()
        {
            // Frame 1: Ensure Main Camera is active with SolidColor clear
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                mainCam.clearFlags = CameraClearFlags.SolidColor;
                mainCam.backgroundColor = new Color(0.05f, 0.45f, 0.85f, 1.0f);
            }

            if (cameraRig == null)
            {
                cameraRig = FindObjectOfType<DotaCameraRig>();
            }

            Vector3 initialPos = spawnPoint != null ? spawnPoint.position : new Vector3(-170.18f, 9.75f, -170.18f);
            if (cameraRig != null)
            {
                cameraRig.SnapTo(initialPos);
            }

            // Load and instantiate DebugUICanvas from Resources
            OpenMainMenu();

            Invoke(nameof(TriggerDiagnosticDump), 4.0f);
        }

        public void OpenMainMenu()
        {
            InMainMenu = true;
            CurrentState = GameState.MainMenu;

            if (debugUiCanvas == null)
            {
                GameObject canvasPrefab = Resources.Load<GameObject>("DebugUICanvas");
                if (canvasPrefab != null)
                {
                    debugUiCanvas = Instantiate(canvasPrefab);
                    debugUiCanvas.SetActive(true);
                }
            }
            else
            {
                debugUiCanvas.SetActive(true);
            }

            Debug.Log($"[GameBootstrap] Main Menu Active. Mode: {CurrentMode}, Hero: {heroName}");
        }

        public void SelectGameMode(int mode)
        {
            SelectGameMode((GameMode)mode);
        }

        public void SelectGameMode(GameMode mode)
        {
            CurrentMode = mode;
            Debug.Log($"[GameBootstrap] Game Mode selected: {CurrentMode}");
        }

        public void SelectHero(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                heroName = name;
                Debug.Log($"[GameBootstrap] Hero selected: {heroName}");
            }
        }

        public void StartFindMatch()
        {
            LaunchMatch(CurrentMode, heroName);
        }

        public void StartHeroDemo()
        {
            LaunchMatch(GameMode.HeroDemo, heroName);
        }

        public void LaunchMatch(GameMode mode, string hero)
        {
            CurrentMode = mode;
            heroName = hero;
            InMainMenu = false;
            CurrentState = GameState.InMatch;
            Started = true;

            if (debugUiCanvas != null)
            {
                debugUiCanvas.SetActive(false);
            }

            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : new Vector3(-170.18f, 9.75f, -170.18f);

            // Instantiate Player Hero
            if (playerHeroInstance == null && heroPrefab != null)
            {
                playerHeroInstance = Instantiate(heroPrefab, spawnPos, Quaternion.identity);
                playerHeroInstance.SetActive(true);
            }

            if (cameraRig != null && playerHeroInstance != null)
            {
                cameraRig.target = playerHeroInstance.transform;
                cameraRig.SnapTo(spawnPos);
            }

            Debug.Log($"[GameBootstrap] Match Started! Mode: {CurrentMode}, Hero: {heroName}, Camera Rig Locked.");
        }

        private void TriggerDiagnosticDump()
        {
            DotaSceneDiagnostic.ExecuteDump();
        }
    }
}
