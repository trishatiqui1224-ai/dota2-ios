using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

namespace Dota.Port
{
    [Serializable]
    public sealed class OfflineHeroEntry
    {
        public string key;
        public string displayName;
        public string prefabResourceKey = string.Empty;
        public Sprite portrait;
        public Sprite[] abilityIcons = new Sprite[4];
        public string[] abilityNames = new string[4];
    }

    public sealed class OfflineDotaApp : MonoBehaviour
    {
        [SerializeField] private OfflineHeroEntry[] heroRoster;
        [SerializeField] private int defaultHeroIndex;
        [SerializeField] private Sprite menuBackground;
        [SerializeField] private Sprite dotaLogo;
        [SerializeField] private Sprite playButtonArt;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Camera demoCamera;

        private static readonly Color32 Ink = new Color32(10, 9, 11, 255);
        private static readonly Color32 Panel = new Color32(18, 17, 19, 244);
        private static readonly Color32 PanelSoft = new Color32(25, 23, 24, 235);
        private static readonly Color32 Gold = new Color32(211, 158, 75, 255);
        private static readonly Color32 Ember = new Color32(178, 67, 39, 255);
        private static readonly Color32 TextWhite = new Color32(244, 239, 227, 255);
        private static readonly Color32 TextMuted = new Color32(166, 159, 150, 255);
        private static readonly Color32 Line = new Color32(89, 68, 48, 255);
        private const int SkillCount = 4;

        private Transform hero;
        private GameObject heroObject;
        private int selectedHeroIndex;
        private Vector3 heroSpawnPosition;
        private Quaternion heroSpawnRotation;
        private Vector3 cameraOffset;
        private float cameraPitch;
        private Vector3 destination;
        private Vector2 joystickInput;
        private bool tapMoveActive;
        private bool demoStarted;
        private bool trainingMode;
        private bool heroCollectionFromMenu;
        private bool ignorePointerUntilRelease;
        private int touchFinger = -1;
        private float[] cooldownUntil = new float[SkillCount];
        private float[] cooldownDuration = { 4f, 5f, 6f, 18f };
        private float dummyHealth = 100f;
        private const float DummyMaxHealth = 100f;
        private GameObject trainingDummy;
        private Renderer dummyRenderer;
        private Sprite circleSprite;

        private Canvas canvas;
        private GameObject menuBackdrop;
        private GameObject mainPage;
        private GameObject modesPage;
        private GameObject heroesPage;
        private GameObject hudRoot;
        private GameObject joystickRoot;
        private RectTransform joystickRect;
        private RectTransform joystickKnob;
        private Image selectedPortrait;
        private Text selectedName;
        private Text selectedStatus;
        private Button demoButton;
        private Button[] heroCardButtons;
        private Image[] heroCardImages;
        private Image[] cooldownOverlays = new Image[SkillCount];
        private Text[] cooldownLabels = new Text[SkillCount];
        private Button[] abilityButtons = new Button[SkillCount];
        private Image selectedHudPortrait;
        private Image heroHealthFill;
        private Image heroManaFill;
        private Text heroNameHud;
        private Text modeStatusHud;
        private Text heroHealthText;
        private Image dummyHealthFill;
        private Text dummyHealthText;
        private Text toastText;
        private GameObject toastRoot;
        private int toastVersion;

        private void Start()
        {
            if (!spawnPoint || !demoCamera)
            {
                Debug.LogError("[V14Offline] Missing spawn point or demo camera.");
                enabled = false;
                return;
            }

            if (heroRoster == null || heroRoster.Length == 0)
            {
                Debug.LogError("[V14Offline] The recovered hero roster is empty.");
                enabled = false;
                return;
            }

            selectedHeroIndex = Mathf.Clamp(defaultHeroIndex, 0, heroRoster.Length - 1);
            EnsureEventSystem();
            BuildInterface();
            SpawnSelectedHero();
            ShowMainMenu();
            Debug.Log($"[V14Offline] Ready with {heroRoster.Length} recovered hero portraits; mode is offline.");
            StartCoroutine(ReportFirstFrame());
        }

        private IEnumerator ReportFirstFrame()
        {
            yield return new WaitForEndOfFrame();
            Debug.Log($"[V14Offline] First frame completed ({SystemInfo.graphicsDeviceType}).");
        }

        private void Update()
        {
            if (!demoStarted || !hero)
                return;

            UpdateTouchMove();
            UpdateHeroMovement();
            UpdateCooldownUI();
        }

        private void BuildInterface()
        {
            var canvasObject = new GameObject("Dota Mobile Offline UI", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 500;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            menuBackdrop = CreateRoot("Menu Backdrop", canvasObject.transform);
            Image background = CreateImage("APK Dashboard Background", menuBackdrop.transform, Vector2.zero, Vector2.one,
                new Color(1f, 1f, 1f, 0.30f), false, menuBackground);
            background.preserveAspect = false;
            CreateImage("Dark Atmosphere", menuBackdrop.transform, Vector2.zero, Vector2.one,
                new Color32(0, 0, 0, 88), false);

            mainPage = CreateRoot("Main Menu", canvasObject.transform);
            modesPage = CreateRoot("Offline Game Modes", canvasObject.transform);
            heroesPage = CreateRoot("Hero Selection", canvasObject.transform);
            hudRoot = CreateRoot("Offline Demo HUD", canvasObject.transform);

            BuildMainMenu();
            BuildModesPage();
            BuildHeroesPage();
            BuildHud();
            modesPage.SetActive(false);
            heroesPage.SetActive(false);
            hudRoot.SetActive(false);
        }

        private void BuildMainMenu()
        {
            CreateImage("Main Menu Panel", mainPage.transform, new Vector2(0.045f, 0.07f), new Vector2(0.42f, 0.93f), Panel, false);
            CreateImage("Panel Accent", mainPage.transform, new Vector2(0.045f, 0.07f), new Vector2(0.052f, 0.93f), Ember, false);

            Image logo = CreateImage("Dota Logo", mainPage.transform, new Vector2(0.075f, 0.72f), new Vector2(0.36f, 0.92f),
                TextWhite, false, dotaLogo);
            logo.preserveAspect = true;
            CreateText("Game Title", mainPage.transform, new Vector2(0.075f, 0.65f), new Vector2(0.39f, 0.73f),
                "DOTA 2 MOBILE", 31, TextWhite, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText("Offline Build Label", mainPage.transform, new Vector2(0.075f, 0.60f), new Vector2(0.39f, 0.66f),
                "V1.4  /  OFFLINE PORT", 15, Gold, TextAnchor.MiddleLeft, FontStyle.Bold);

            Button play = CreateButton("Play Offline", mainPage.transform, new Vector2(0.075f, 0.43f), new Vector2(0.39f, 0.55f),
                "PLAY OFFLINE", Ember, TextWhite, 21, playButtonArt);
            play.onClick.AddListener(ShowModesPage);

            Button heroes = CreateButton("Hero Collection", mainPage.transform, new Vector2(0.075f, 0.29f), new Vector2(0.39f, 0.41f),
                "HERO COLLECTION", PanelSoft, TextWhite, 20);
            heroes.onClick.AddListener(ShowHeroCollection);

            CreateImage("Footer Rule", mainPage.transform, new Vector2(0.075f, 0.23f), new Vector2(0.39f, 0.235f), Line, false);
            CreateText("Offline Status", mainPage.transform, new Vector2(0.075f, 0.10f), new Vector2(0.39f, 0.22f),
                "LOCAL SESSION READY\nNo account or network required.", 15, TextMuted, TextAnchor.MiddleLeft, FontStyle.Normal);
            CreateText("Build Tag", mainPage.transform, new Vector2(0.075f, 0.055f), new Vector2(0.39f, 0.105f),
                "APK ASSETS  •  iOS METAL", 12, TextMuted, TextAnchor.MiddleLeft, FontStyle.Bold);

            CreateText("Feature Callout", mainPage.transform, new Vector2(0.48f, 0.08f), new Vector2(0.95f, 0.17f),
                "SELECT A HERO  •  ENTER AN OFFLINE SHOWCASE  •  PRACTICE CONTROLS", 15,
                TextWhite, TextAnchor.MiddleRight, FontStyle.Bold);
        }

        private void BuildModesPage()
        {
            CreateImage("Modes Panel", modesPage.transform, new Vector2(0.045f, 0.06f), new Vector2(0.58f, 0.94f), Panel, false);
            AddBackButton(modesPage.transform, ShowMainMenu);
            CreateText("Modes Heading", modesPage.transform, new Vector2(0.09f, 0.79f), new Vector2(0.54f, 0.91f),
                "OFFLINE GAME MODES", 29, TextWhite, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText("Modes Subheading", modesPage.transform, new Vector2(0.09f, 0.74f), new Vector2(0.54f, 0.80f),
                "Choose a local way to explore the recovered heroes.", 15, TextMuted, TextAnchor.MiddleLeft, FontStyle.Normal);

            CreateModeCard("Hero Demo", "View the selected hero and try movement and showcase skills.",
                new Vector2(0.09f, 0.48f), false);
            CreateModeCard("Skill Practice", "Practice attacks and ability effects against a local target.",
                new Vector2(0.09f, 0.20f), true);
            CreateText("Offline Notice", modesPage.transform, new Vector2(0.09f, 0.08f), new Vector2(0.54f, 0.16f),
                "MATCHMAKING AND NETWORK PLAY ARE NOT INCLUDED.", 13, Gold, TextAnchor.MiddleLeft, FontStyle.Bold);
        }

        private void CreateModeCard(string title, string description, Vector2 lowerLeft, bool practice)
        {
            var root = CreateImage(title + " Card", modesPage.transform, lowerLeft,
                new Vector2(0.54f, lowerLeft.y + 0.24f), PanelSoft, false);
            CreateImage(title + " Card Rule", root.transform, new Vector2(0.02f, 0.05f), new Vector2(0.035f, 0.95f),
                practice ? Gold : Ember, false);
            CreateText(title + " Heading", root.transform, new Vector2(0.08f, 0.55f), new Vector2(0.94f, 0.90f),
                title.ToUpperInvariant(), 22, TextWhite, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText(title + " Description", root.transform, new Vector2(0.08f, 0.20f), new Vector2(0.68f, 0.57f),
                description, 15, TextMuted, TextAnchor.MiddleLeft, FontStyle.Normal);
            Button enter = CreateButton(title + " Enter", root.transform, new Vector2(0.73f, 0.25f), new Vector2(0.94f, 0.76f),
                "SELECT", practice ? Gold : Ember, Ink, 15);
            enter.onClick.AddListener(() =>
            {
                trainingMode = practice;
                heroCollectionFromMenu = false;
                ShowHeroesPage();
            });
        }

        private void BuildHeroesPage()
        {
            CreateImage("Hero Selection Panel", heroesPage.transform, new Vector2(0.025f, 0.045f), new Vector2(0.975f, 0.955f), Panel, false);
            AddBackButton(heroesPage.transform, BackFromHeroesPage);
            CreateText("Hero Selection Heading", heroesPage.transform, new Vector2(0.06f, 0.84f), new Vector2(0.69f, 0.94f),
                "SELECT A HERO", 27, TextWhite, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText("Roster Count", heroesPage.transform, new Vector2(0.53f, 0.85f), new Vector2(0.69f, 0.92f),
                $"{heroRoster.Length} RECOVERED PORTRAITS", 12, Gold, TextAnchor.MiddleRight, FontStyle.Bold);

            BuildHeroGrid();

            CreateImage("Hero Details Panel", heroesPage.transform, new Vector2(0.72f, 0.12f), new Vector2(0.95f, 0.83f), PanelSoft, false);
            selectedPortrait = CreateImage("Selected Hero Portrait", heroesPage.transform, new Vector2(0.745f, 0.55f), new Vector2(0.925f, 0.79f),
                TextWhite, false);
            selectedPortrait.preserveAspect = true;
            selectedName = CreateText("Selected Hero Name", heroesPage.transform, new Vector2(0.745f, 0.46f), new Vector2(0.925f, 0.56f),
                "HERO", 20, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);
            selectedStatus = CreateText("Selected Hero Status", heroesPage.transform, new Vector2(0.745f, 0.33f), new Vector2(0.925f, 0.46f),
                "", 13, TextMuted, TextAnchor.MiddleCenter, FontStyle.Normal);
            demoButton = CreateButton("Start Selected Mode", heroesPage.transform, new Vector2(0.745f, 0.15f), new Vector2(0.925f, 0.25f),
                "HERO DEMO", Ember, TextWhite, 15);
            demoButton.onClick.AddListener(() => BeginDemo(trainingMode));
            CreateText("Roster Note", heroesPage.transform, new Vector2(0.06f, 0.065f), new Vector2(0.69f, 0.12f),
                "Dimmed heroes have portraits in the APK but no base 3D model in this export.", 12,
                TextMuted, TextAnchor.MiddleLeft, FontStyle.Normal);
            RefreshHeroDetails();
        }

        private void BuildHeroGrid()
        {
            var scrollObject = new GameObject("Hero Grid Scroll", typeof(RectTransform), typeof(ScrollRect), typeof(Image));
            scrollObject.transform.SetParent(heroesPage.transform, false);
            var scrollRect = scrollObject.GetComponent<RectTransform>();
            SetRect(scrollRect, new Vector2(0.055f, 0.15f), new Vector2(0.695f, 0.82f));
            scrollObject.GetComponent<Image>().color = new Color32(10, 9, 11, 235);

            var viewportObject = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask));
            viewportObject.transform.SetParent(scrollObject.transform, false);
            var viewport = viewportObject.GetComponent<RectTransform>();
            SetRect(viewport, Vector2.zero, Vector2.one);
            viewportObject.GetComponent<Image>().color = Color.white;
            viewportObject.GetComponent<Mask>().showMaskGraphic = false;

            var contentObject = new GameObject("Hero Grid Content", typeof(RectTransform), typeof(GridLayoutGroup), typeof(ContentSizeFitter));
            contentObject.transform.SetParent(viewportObject.transform, false);
            var content = contentObject.GetComponent<RectTransform>();
            content.anchorMin = new Vector2(0f, 1f);
            content.anchorMax = new Vector2(1f, 1f);
            content.pivot = new Vector2(0.5f, 1f);
            content.anchoredPosition = Vector2.zero;
            content.sizeDelta = Vector2.zero;

            var grid = contentObject.GetComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(128f, 132f);
            grid.spacing = new Vector2(10f, 10f);
            grid.padding = new RectOffset(10, 10, 10, 10);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 8;
            grid.childAlignment = TextAnchor.UpperLeft;
            var fitter = contentObject.GetComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var scroll = scrollObject.GetComponent<ScrollRect>();
            scroll.viewport = viewport;
            scroll.content = content;
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;
            scroll.inertia = true;

            heroCardButtons = new Button[heroRoster.Length];
            heroCardImages = new Image[heroRoster.Length];
            for (int i = 0; i < heroRoster.Length; i++)
            {
                int index = i;
                GameObject card = new GameObject("Hero Card " + heroRoster[i].key, typeof(RectTransform), typeof(Image), typeof(Button));
                card.transform.SetParent(contentObject.transform, false);
                Image cardImage = card.GetComponent<Image>();
                cardImage.color = heroRoster[i].prefabResourceKey.Length > 0 ? PanelSoft : new Color32(24, 23, 24, 210);
                Button button = card.GetComponent<Button>();
                button.targetGraphic = cardImage;
                button.transition = Selectable.Transition.ColorTint;
                button.colors = ButtonColors(cardImage.color, Gold);
                button.onClick.AddListener(() => SelectHero(index));

                Image portrait = CreateImage("Portrait", card.transform, new Vector2(0.08f, 0.21f), new Vector2(0.92f, 0.94f),
                    heroRoster[i].prefabResourceKey.Length > 0 ? TextWhite : new Color32(110, 108, 105, 255), false,
                    heroRoster[i].portrait);
                portrait.preserveAspect = true;
                CreateText("Hero Name", card.transform, new Vector2(0.03f, 0.02f), new Vector2(0.97f, 0.22f),
                    heroRoster[i].displayName.ToUpperInvariant(), 10, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);

                heroCardButtons[i] = button;
                heroCardImages[i] = cardImage;
            }
        }

        private void BuildHud()
        {
            CreateImage("HUD Top Bar", hudRoot.transform, new Vector2(0.025f, 0.84f), new Vector2(0.975f, 0.975f), Panel, false);
            Image heroPortrait = CreateImage("HUD Hero Portrait", hudRoot.transform, new Vector2(0.045f, 0.86f), new Vector2(0.105f, 0.955f),
                TextWhite, false);
            heroPortrait.preserveAspect = true;
            selectedHudPortrait = heroPortrait;
            heroNameHud = CreateText("HUD Hero Name", hudRoot.transform, new Vector2(0.12f, 0.90f), new Vector2(0.32f, 0.96f),
                "HERO", 17, TextWhite, TextAnchor.MiddleLeft, FontStyle.Bold);
            modeStatusHud = CreateText("HUD Mode Status", hudRoot.transform, new Vector2(0.12f, 0.855f), new Vector2(0.32f, 0.91f),
                "OFFLINE SHOWCASE", 11, Gold, TextAnchor.MiddleLeft, FontStyle.Bold);

            CreateImage("Health Back", hudRoot.transform, new Vector2(0.36f, 0.90f), new Vector2(0.59f, 0.94f), new Color32(61, 25, 21, 255), false);
            heroHealthFill = CreateImage("Health Fill", hudRoot.transform, new Vector2(0.36f, 0.90f), new Vector2(0.59f, 0.94f), new Color32(154, 54, 38, 255), false);
            heroHealthText = CreateText("Health Text", hudRoot.transform, new Vector2(0.36f, 0.90f), new Vector2(0.59f, 0.94f),
                "100 / 100", 12, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);
            CreateImage("Mana Back", hudRoot.transform, new Vector2(0.36f, 0.855f), new Vector2(0.59f, 0.89f), new Color32(22, 37, 75, 255), false);
            heroManaFill = CreateImage("Mana Fill", hudRoot.transform, new Vector2(0.36f, 0.855f), new Vector2(0.59f, 0.89f), new Color32(55, 96, 169, 255), false);
            heroManaFill.fillAmount = 1f;
            CreateText("Mana Text", hudRoot.transform, new Vector2(0.36f, 0.855f), new Vector2(0.59f, 0.89f),
                "600 / 600", 11, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);

            CreateImage("Target Bar", hudRoot.transform, new Vector2(0.64f, 0.89f), new Vector2(0.81f, 0.94f), new Color32(36, 31, 27, 255), false);
            dummyHealthFill = CreateImage("Target Health Fill", hudRoot.transform, new Vector2(0.64f, 0.89f), new Vector2(0.81f, 0.94f), Gold, false);
            dummyHealthText = CreateText("Target Health Text", hudRoot.transform, new Vector2(0.64f, 0.89f), new Vector2(0.81f, 0.94f),
                "TARGET  100", 11, Ink, TextAnchor.MiddleCenter, FontStyle.Bold);

            Button menu = CreateButton("HUD Menu", hudRoot.transform, new Vector2(0.85f, 0.875f), new Vector2(0.95f, 0.95f),
                "MENU", PanelSoft, TextWhite, 14);
            menu.onClick.AddListener(ReturnToHeroes);

            BuildJoystick();
            BuildAbilityButtons();
            toastRoot = CreateImage("Demo Toast", hudRoot.transform, new Vector2(0.34f, 0.39f), new Vector2(0.66f, 0.50f), Panel, false).gameObject;
            toastText = CreateText("Demo Toast Text", toastRoot.transform, new Vector2(0.04f, 0.08f), new Vector2(0.96f, 0.92f),
                "", 18, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);
            toastRoot.SetActive(false);
        }

        private void BuildJoystick()
        {
            joystickRoot = CreateImage("Virtual Joystick Base", hudRoot.transform, new Vector2(0.045f, 0.065f), new Vector2(0.205f, 0.315f),
                new Color32(232, 225, 210, 90), false, GetCircleSprite()).gameObject;
            joystickRoot.GetComponent<Image>().preserveAspect = true;
            joystickRoot.GetComponent<Image>().raycastTarget = true;
            joystickRect = joystickRoot.GetComponent<RectTransform>();
            joystickKnob = CreateImage("Virtual Joystick Knob", joystickRoot.transform, new Vector2(0.27f, 0.27f), new Vector2(0.73f, 0.73f),
                new Color32(235, 221, 196, 215), false, GetCircleSprite()).rectTransform;
            joystickKnob.GetComponent<Image>().preserveAspect = true;

            var trigger = joystickRoot.AddComponent<EventTrigger>();
            AddTrigger(trigger, EventTriggerType.PointerDown, UpdateJoystick);
            AddTrigger(trigger, EventTriggerType.Drag, UpdateJoystick);
            AddTrigger(trigger, EventTriggerType.PointerUp, EndJoystick);
            AddTrigger(trigger, EventTriggerType.PointerExit, EndJoystick);
        }

        private void BuildAbilityButtons()
        {
            for (int i = 0; i < SkillCount; i++)
            {
                int index = i;
                float left = 0.515f + i * 0.087f;
                var buttonObject = new GameObject("Ability " + (i + 1), typeof(RectTransform), typeof(Image), typeof(Button));
                buttonObject.transform.SetParent(hudRoot.transform, false);
                SetRect(buttonObject.GetComponent<RectTransform>(), new Vector2(left, 0.07f), new Vector2(left + 0.078f, 0.225f));
                Image buttonImage = buttonObject.GetComponent<Image>();
                buttonImage.color = PanelSoft;
                Button button = buttonObject.GetComponent<Button>();
                button.targetGraphic = buttonImage;
                button.transition = Selectable.Transition.ColorTint;
                button.colors = ButtonColors(PanelSoft, Gold);
                button.onClick.AddListener(() => CastAbility(index));
                abilityButtons[i] = button;

                Sprite icon = GetSelectedAbilityIcon(i);
                Image skillIcon = CreateImage("Skill Icon", buttonObject.transform, new Vector2(0.12f, 0.24f), new Vector2(0.88f, 0.90f),
                    TextWhite, false, icon);
                skillIcon.preserveAspect = true;
                CreateText("Skill Key", buttonObject.transform, new Vector2(0.05f, 0.69f), new Vector2(0.30f, 0.97f),
                    new[] { "Q", "W", "E", "R" }[i], 12, Gold, TextAnchor.MiddleCenter, FontStyle.Bold);
                CreateText("Skill Name", buttonObject.transform, new Vector2(0.04f, 0.02f), new Vector2(0.96f, 0.24f),
                    GetSelectedAbilityName(i), 8, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);

                var overlay = CreateImage("Cooldown Overlay", buttonObject.transform, Vector2.zero, Vector2.one,
                    new Color(0f, 0f, 0f, 0.70f), false, GetCircleSprite());
                overlay.type = Image.Type.Filled;
                overlay.fillMethod = Image.FillMethod.Radial360;
                overlay.fillOrigin = (int)Image.Origin360.Top;
                overlay.fillAmount = 0f;
                cooldownOverlays[i] = overlay;
                cooldownLabels[i] = CreateText("Cooldown Seconds", buttonObject.transform, new Vector2(0.1f, 0.34f), new Vector2(0.9f, 0.66f),
                    "", 20, TextWhite, TextAnchor.MiddleCenter, FontStyle.Bold);
            }

            Button attack = CreateButton("Basic Attack", hudRoot.transform, new Vector2(0.88f, 0.075f), new Vector2(0.97f, 0.225f),
                "ATTACK", Ember, TextWhite, 13);
            attack.onClick.AddListener(BasicAttack);
            CreateText("HUD Help", hudRoot.transform, new Vector2(0.27f, 0.065f), new Vector2(0.49f, 0.17f),
                "MOVE  /  ATTACK  /  CAST\nTap arena or drag stick. Skill effects are local previews.", 10, TextMuted, TextAnchor.MiddleLeft, FontStyle.Normal);
        }

        private void AddBackButton(Transform parent, Action onClick)
        {
            Button back = CreateButton("Back", parent, new Vector2(0.87f, 0.865f), new Vector2(0.95f, 0.94f),
                "BACK", PanelSoft, TextWhite, 13);
            back.onClick.AddListener(() => onClick());
        }

        private void BuildHeroGridCardsRefresh()
        {
            if (heroCardButtons == null)
                return;
            for (int i = 0; i < heroCardImages.Length; i++)
            {
                Color color = heroRoster[i].prefabResourceKey.Length > 0 ? PanelSoft : new Color32(22, 21, 22, 210);
                if (i == selectedHeroIndex)
                    color = new Color32(87, 53, 35, 255);
                heroCardImages[i].color = color;
                heroCardButtons[i].colors = ButtonColors(color, Gold);
            }
        }

        private void RefreshHeroDetails()
        {
            if (heroRoster == null || heroRoster.Length == 0 || selectedName == null)
                return;
            OfflineHeroEntry entry = heroRoster[selectedHeroIndex];
            selectedName.text = entry.displayName.ToUpperInvariant();
            selectedPortrait.sprite = entry.portrait;
            bool available = !string.IsNullOrWhiteSpace(entry.prefabResourceKey);
            selectedStatus.text = available
                ? (trainingMode ? "3D MODEL READY\nSKILL PRACTICE / OFFLINE" : "3D MODEL READY\nHERO DEMO / OFFLINE")
                : "PORTRAIT RECOVERED\n3D MODEL NOT IN APK EXPORT";
            demoButton.interactable = available;
            if (demoButton)
            {
                demoButton.GetComponent<Image>().color = !available ? new Color32(73, 70, 67, 255) : trainingMode ? Gold : Ember;
                Text label = demoButton.GetComponentInChildren<Text>();
                if (label)
                    label.text = trainingMode ? "START PRACTICE" : "START HERO DEMO";
            }
            BuildHeroGridCardsRefresh();
        }

        private void SelectHero(int index)
        {
            selectedHeroIndex = Mathf.Clamp(index, 0, heroRoster.Length - 1);
            RefreshHeroDetails();
            SpawnSelectedHero();
        }

        private void SpawnSelectedHero()
        {
            if (heroObject)
                Destroy(heroObject);
            hero = null;
            heroObject = null;

            if (heroRoster == null || heroRoster.Length == 0)
                return;
            string resourceKey = heroRoster[selectedHeroIndex].prefabResourceKey;
            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                if (demoCamera)
                    demoCamera.enabled = false;
                return;
            }

            GameObject prefab = Resources.Load<GameObject>(resourceKey);
            if (!prefab)
            {
                Debug.LogError($"[V14Offline] Could not load hero resource {resourceKey}.");
                return;
            }

            heroObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            hero = heroObject.transform;
            heroObject.SetActive(true);
            ApplyNoShadow(heroObject);
            AlignHeroToStage();
            FitCameraToHero();
            heroSpawnPosition = hero.position;
            heroSpawnRotation = hero.rotation;
            destination = hero.position;
            tapMoveActive = false;
            demoCamera.enabled = true;
            SetCameraYaw(-14f);
            FollowHero(true);
            Debug.Log($"[V14Offline] Loaded {heroRoster[selectedHeroIndex].displayName} from {resourceKey}.");
        }

        private void ShowMainMenu()
        {
            if (demoStarted)
                StopDemo();
            mainPage.SetActive(true);
            modesPage.SetActive(false);
            heroesPage.SetActive(false);
            hudRoot.SetActive(false);
            menuBackdrop.SetActive(true);
            if (hero && demoCamera)
            {
                hero.position = heroSpawnPosition;
                hero.rotation = heroSpawnRotation;
                SetCameraYaw(-14f);
                FollowHero(true);
            }
        }

        private void ShowModesPage()
        {
            mainPage.SetActive(false);
            modesPage.SetActive(true);
            heroesPage.SetActive(false);
            hudRoot.SetActive(false);
            menuBackdrop.SetActive(true);
            SetMenuCamera();
        }

        private void ShowHeroesPage()
        {
            mainPage.SetActive(false);
            modesPage.SetActive(false);
            heroesPage.SetActive(true);
            hudRoot.SetActive(false);
            menuBackdrop.SetActive(true);
            RefreshHeroDetails();
            SetMenuCamera();
        }

        private void ShowHeroCollection()
        {
            trainingMode = false;
            heroCollectionFromMenu = true;
            ShowHeroesPage();
        }

        private void BackFromHeroesPage()
        {
            if (heroCollectionFromMenu)
                ShowMainMenu();
            else
                ShowModesPage();
        }

        private void SetMenuCamera()
        {
            if (hero && demoCamera)
            {
                SetCameraYaw(-14f);
                FollowHero(true);
            }
        }

        private void BeginDemo(bool practice)
        {
            if (!hero || !demoCamera)
            {
                ShowToast("Select a recovered 3D hero first.");
                return;
            }

            trainingMode = practice;
            demoStarted = true;
            ignorePointerUntilRelease = true;
            joystickInput = Vector2.zero;
            tapMoveActive = false;
            touchFinger = -1;
            Array.Clear(cooldownUntil, 0, cooldownUntil.Length);
            hero.position = heroSpawnPosition;
            hero.rotation = heroSpawnRotation;
            destination = hero.position;
            SetCameraYaw(0f);
            FollowHero(true);
            mainPage.SetActive(false);
            modesPage.SetActive(false);
            heroesPage.SetActive(false);
            hudRoot.SetActive(true);
            menuBackdrop.SetActive(false);
            if (selectedHudPortrait)
                selectedHudPortrait.sprite = heroRoster[selectedHeroIndex].portrait;
            if (heroNameHud)
                heroNameHud.text = heroRoster[selectedHeroIndex].displayName.ToUpperInvariant();
            if (modeStatusHud)
                modeStatusHud.text = practice ? "LOCAL SKILL PREVIEWS  /  OFFLINE" : "HERO SHOWCASE  /  OFFLINE";
            dummyHealth = DummyMaxHealth;
            CreateTrainingDummy();
            RefreshDummyHealth();
            UpdateCooldownUI();
            ShowToast(practice ? "SKILL PRACTICE READY" : "HERO DEMO READY");
        }

        private void ReturnToHeroes()
        {
            StopDemo();
            heroesPage.SetActive(true);
            mainPage.SetActive(false);
            modesPage.SetActive(false);
            hudRoot.SetActive(false);
            menuBackdrop.SetActive(true);
            SetMenuCamera();
        }

        private void StopDemo()
        {
            demoStarted = false;
            joystickInput = Vector2.zero;
            tapMoveActive = false;
            touchFinger = -1;
            if (trainingDummy)
                Destroy(trainingDummy);
            trainingDummy = null;
            dummyRenderer = null;
            if (hudRoot)
                hudRoot.SetActive(false);
        }

        private void UpdateJoystick(BaseEventData eventData)
        {
            if (!demoStarted || joystickRect == null)
                return;
            PointerEventData pointer = eventData as PointerEventData;
            if (pointer == null)
                return;
            Vector2 local;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickRect, pointer.position,
                pointer.pressEventCamera, out local))
                return;
            float radius = Mathf.Max(joystickRect.rect.width * 0.42f, 1f);
            joystickInput = Vector2.ClampMagnitude(local / radius, 1f);
            joystickKnob.anchoredPosition = joystickInput * radius * 0.55f;
            tapMoveActive = false;
        }

        private void EndJoystick(BaseEventData eventData)
        {
            joystickInput = Vector2.zero;
            if (joystickKnob)
                joystickKnob.anchoredPosition = Vector2.zero;
        }

        private void UpdateTouchMove()
        {
            if (ignorePointerUntilRelease)
            {
#if ENABLE_LEGACY_INPUT_MANAGER
                if (Input.touchCount == 0 && !Input.GetMouseButton(0))
                    ignorePointerUntilRelease = false;
#elif ENABLE_INPUT_SYSTEM
                var pointer = Pointer.current;
                if (pointer == null || !pointer.press.isPressed)
                    ignorePointerUntilRelease = false;
#endif
                if (ignorePointerUntilRelease)
                    return;
            }

#if ENABLE_LEGACY_INPUT_MANAGER
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == UnityEngine.TouchPhase.Began && touchFinger == -1)
                {
                    if (EventSystem.current && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        continue;
                    touchFinger = touch.fingerId;
                }
                if (touch.fingerId != touchFinger)
                    continue;
                if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Moved ||
                    touch.phase == UnityEngine.TouchPhase.Stationary)
                    SetDestination(touch.position);
                if (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled)
                    touchFinger = -1;
            }
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.touchCount == 0 && Input.GetMouseButtonDown(0) &&
                (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject()))
                SetDestination(Input.mousePosition);
#endif
#elif ENABLE_INPUT_SYSTEM
            var pointer = Pointer.current;
            if (pointer != null && pointer.press.wasPressedThisFrame &&
                (!EventSystem.current || !EventSystem.current.IsPointerOverGameObject()))
                SetDestination(pointer.position.ReadValue());
#endif
        }

        private void UpdateHeroMovement()
        {
            if (joystickInput.sqrMagnitude > 0.04f)
            {
                Vector3 direction = new Vector3(joystickInput.x, 0f, joystickInput.y);
                MoveHero(direction, 4.2f * Time.deltaTime);
                destination = hero.position;
                tapMoveActive = false;
            }
            else if (tapMoveActive)
            {
                Vector3 next = Vector3.MoveTowards(hero.position, destination, 4.2f * Time.deltaTime);
                Vector3 heading = next - hero.position;
                heading.y = 0f;
                if (heading.sqrMagnitude > 0.0001f)
                    hero.rotation = Quaternion.LookRotation(heading);
                hero.position = next;
                if ((hero.position - destination).sqrMagnitude < 0.01f)
                    tapMoveActive = false;
            }

            FollowHero(false);
        }

        private void MoveHero(Vector3 direction, float distance)
        {
            if (direction.sqrMagnitude < 0.001f)
                return;
            direction.Normalize();
            hero.rotation = Quaternion.LookRotation(direction);
            Vector3 candidate = hero.position + direction * distance;
            Vector3 offset = candidate - spawnPoint.position;
            offset.y = 0f;
            candidate = spawnPoint.position + Vector3.ClampMagnitude(offset, 18f);
            candidate.y = hero.position.y;
            hero.position = candidate;
        }

        private void SetDestination(Vector2 screenPosition)
        {
            Ray ray = demoCamera.ScreenPointToRay(screenPosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0f, spawnPoint.position.y, 0f));
            if (!plane.Raycast(ray, out float distance))
                return;
            Vector3 point = ray.GetPoint(distance);
            Vector3 offset = point - spawnPoint.position;
            offset.y = 0f;
            destination = spawnPoint.position + Vector3.ClampMagnitude(offset, 18f);
            destination.y = hero.position.y;
            tapMoveActive = true;
        }

        private void CastAbility(int index)
        {
            if (!demoStarted || index < 0 || index >= SkillCount || Time.time < cooldownUntil[index])
                return;

            cooldownUntil[index] = Time.time + cooldownDuration[index];
            Vector3 forward = hero.forward;
            forward.y = 0f;
            if (forward.sqrMagnitude < 0.001f)
                forward = Vector3.forward;
            forward.Normalize();

            string abilityName = GetSelectedAbilityName(index);
            if (index < 3)
            {
                float range = new[] { 4.5f, 6.0f, 7.0f }[index];
                Vector3 center = hero.position + forward * range;
                SpawnEnergyRing(center, 1.25f + index * 0.18f, new Color(1f, 0.24f, 0.08f, 1f), 0.6f);
                DamageDummy(index == 2 ? 28f : 18f, center, 3.5f);
            }
            else
            {
                SpawnEnergyRing(hero.position, 7f, new Color(1f, 0.57f, 0.12f, 1f), 0.95f);
                DamageDummy(42f, hero.position, 8.5f);
            }

            ShowToast(abilityName.ToUpperInvariant() + "  /  DEMO EFFECT");
        }

        private void BasicAttack()
        {
            if (!demoStarted)
                return;
            Vector3 center = trainingDummy ? trainingDummy.transform.position : hero.position + hero.forward * 2f;
            SpawnEnergyRing(center, 0.8f, new Color(1f, 0.78f, 0.29f, 1f), 0.35f);
            DamageDummy(14f, hero.position, 9f);
            ShowToast("BASIC ATTACK");
        }

        private void DamageDummy(float damage, Vector3 castPoint, float radius)
        {
            if (!trainingDummy)
                return;
            if (Vector3.Distance(trainingDummy.transform.position, castPoint) > radius)
            {
                ShowToast("MOVE CLOSER TO THE TARGET");
                return;
            }

            dummyHealth = Mathf.Max(0f, dummyHealth - damage);
            RefreshDummyHealth();
            if (dummyRenderer)
                StartCoroutine(FlashDummy());
            if (dummyHealth <= 0f)
                StartCoroutine(ResetDummyAfterDelay());
        }

        private IEnumerator FlashDummy()
        {
            if (!dummyRenderer)
                yield break;
            Color original = dummyRenderer.material.color;
            dummyRenderer.material.color = new Color(1f, 0.25f, 0.13f, 1f);
            yield return new WaitForSeconds(0.12f);
            if (dummyRenderer)
                dummyRenderer.material.color = original;
        }

        private IEnumerator ResetDummyAfterDelay()
        {
            ShowToast("TARGET DEFEATED");
            yield return new WaitForSeconds(1.1f);
            dummyHealth = DummyMaxHealth;
            RefreshDummyHealth();
        }

        private void CreateTrainingDummy()
        {
            if (trainingDummy)
                Destroy(trainingDummy);
            Vector3 facing = hero.forward;
            facing.y = 0f;
            if (facing.sqrMagnitude < 0.001f)
                facing = Vector3.forward;
            trainingDummy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            trainingDummy.name = "Offline Training Target";
            trainingDummy.transform.position = hero.position + facing.normalized * 7f;
            trainingDummy.transform.localScale = new Vector3(0.9f, 1.15f, 0.9f);
            var collider = trainingDummy.GetComponent<Collider>();
            if (collider)
                Destroy(collider);
            dummyRenderer = trainingDummy.GetComponent<Renderer>();
            Shader shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            dummyRenderer.material = new Material(shader);
            dummyRenderer.material.color = new Color32(128, 51, 39, 255);
            dummyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            dummyRenderer.receiveShadows = false;
        }

        private void RefreshDummyHealth()
        {
            if (dummyHealthFill)
                dummyHealthFill.fillAmount = dummyHealth / DummyMaxHealth;
            if (dummyHealthText)
                dummyHealthText.text = $"TARGET  {Mathf.CeilToInt(dummyHealth)} / 100";
        }

        private IEnumerator AnimateRing(LineRenderer line, Material material, GameObject ringObject, float maxRadius, float duration)
        {
            float time = 0f;
            const int points = 49;
            while (time < duration && line)
            {
                float ratio = Mathf.Clamp01(time / duration);
                float radius = Mathf.Lerp(maxRadius * 0.25f, maxRadius, ratio);
                for (int i = 0; i < points; i++)
                {
                    float angle = i * Mathf.PI * 2f / (points - 1);
                    line.SetPosition(i, new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius));
                }
                Color color = line.startColor;
                color.a = 1f - ratio;
                line.startColor = color;
                line.endColor = color;
                time += Time.deltaTime;
                yield return null;
            }
            if (material)
                Destroy(material);
            if (ringObject)
                Destroy(ringObject);
        }

        private void SpawnEnergyRing(Vector3 center, float radius, Color color, float duration)
        {
            var ringObject = new GameObject("Offline Ability Effect");
            ringObject.transform.position = new Vector3(center.x, spawnPoint.position.y + 0.03f, center.z);
            var line = ringObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.loop = false;
            line.positionCount = 49;
            line.widthMultiplier = 0.075f;
            line.numCapVertices = 3;
            line.startColor = color;
            line.endColor = color;
            Shader shader = Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Unlit/Color");
            var material = new Material(shader);
            if (material.HasProperty("_BaseColor"))
                material.SetColor("_BaseColor", color);
            if (material.HasProperty("_Color"))
                material.SetColor("_Color", color);
            line.material = material;
            StartCoroutine(AnimateRing(line, material, ringObject, radius, duration));
        }

        private void UpdateCooldownUI()
        {
            for (int i = 0; i < SkillCount; i++)
            {
                if (!cooldownOverlays[i] || !abilityButtons[i])
                    continue;
                float remaining = Mathf.Max(0f, cooldownUntil[i] - Time.time);
                float amount = cooldownDuration[i] <= 0f ? 0f : remaining / cooldownDuration[i];
                cooldownOverlays[i].fillAmount = amount;
                cooldownLabels[i].text = remaining > 0.05f ? Mathf.CeilToInt(remaining).ToString() : string.Empty;
                abilityButtons[i].interactable = remaining <= 0.05f;
            }
        }

        private void ShowToast(string message)
        {
            if (!toastRoot || !toastText)
                return;
            toastText.text = message;
            toastRoot.SetActive(true);
            int version = ++toastVersion;
            StartCoroutine(HideToast(version));
        }

        private IEnumerator HideToast(int version)
        {
            yield return new WaitForSeconds(1.3f);
            if (version == toastVersion && toastRoot)
                toastRoot.SetActive(false);
        }

        private void FollowHero(bool snap)
        {
            if (!hero || !demoCamera)
                return;
            Vector3 target = hero.position + cameraOffset;
            if (snap)
                demoCamera.transform.position = target;
            else
                demoCamera.transform.position = Vector3.Lerp(demoCamera.transform.position, target, 10f * Time.deltaTime);
        }

        private void SetCameraYaw(float yaw)
        {
            demoCamera.transform.rotation = Quaternion.Euler(cameraPitch, yaw, 0f);
        }

        private void AlignHeroToStage()
        {
            Renderer[] renderers = hero.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
                return;
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
                bounds.Encapsulate(renderers[i].bounds);
            hero.position += Vector3.up * (spawnPoint.position.y - bounds.min.y);
        }

        private void FitCameraToHero()
        {
            Renderer[] renderers = hero.GetComponentsInChildren<Renderer>(true);
            float height = 2f;
            if (renderers.Length > 0)
            {
                Bounds bounds = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++)
                    bounds.Encapsulate(renderers[i].bounds);
                height = Mathf.Max(bounds.size.y, 1f);
            }
            cameraOffset = new Vector3(0f, height * 1.15f, -height * 3.2f);
            cameraPitch = Mathf.Atan2(height * 0.62f, height * 3.2f) * Mathf.Rad2Deg;
            demoCamera.fieldOfView = 36f;
        }

        private static void ApplyNoShadow(GameObject actor)
        {
            foreach (Renderer renderer in actor.GetComponentsInChildren<Renderer>(true))
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
            }
        }

        private Sprite GetSelectedAbilityIcon(int index)
        {
            if (heroRoster == null || selectedHeroIndex < 0 || selectedHeroIndex >= heroRoster.Length)
                return null;
            Sprite[] icons = heroRoster[selectedHeroIndex].abilityIcons;
            return icons != null && index < icons.Length ? icons[index] : null;
        }

        private string GetSelectedAbilityName(int index)
        {
            if (heroRoster != null && selectedHeroIndex >= 0 && selectedHeroIndex < heroRoster.Length)
            {
                string[] names = heroRoster[selectedHeroIndex].abilityNames;
                if (names != null && index < names.Length && !string.IsNullOrWhiteSpace(names[index]))
                    return names[index];
            }
            return new[] { "Ability 1", "Ability 2", "Ability 3", "Ultimate" }[index];
        }

        private Sprite GetCircleSprite()
        {
            if (circleSprite)
                return circleSprite;
            const int size = 96;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.name = "Offline UI Circle";
            texture.filterMode = FilterMode.Bilinear;
            for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2((size - 1) * 0.5f, (size - 1) * 0.5f));
                float alpha = Mathf.Clamp01((size * 0.48f - distance) * 0.7f);
                texture.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
            }
            texture.Apply(false, false);
            circleSprite = Sprite.Create(texture, new Rect(0f, 0f, size, size), new Vector2(0.5f, 0.5f), 100f);
            return circleSprite;
        }

        private static void EnsureEventSystem()
        {
            EventSystem eventSystem = EventSystem.current;
            if (!eventSystem)
                eventSystem = new GameObject("Dota Offline EventSystem", typeof(EventSystem)).GetComponent<EventSystem>();
#if ENABLE_INPUT_SYSTEM
            InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (!inputModule)
            {
                foreach (BaseInputModule oldModule in eventSystem.GetComponents<BaseInputModule>())
                    oldModule.enabled = false;
                inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }
            inputModule.AssignDefaultActions();
#else
            if (!eventSystem.GetComponent<BaseInputModule>())
                eventSystem.gameObject.AddComponent<StandaloneInputModule>();
#endif
        }

        private static void AddTrigger(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }

        private static GameObject CreateRoot(string name, Transform parent)
        {
            var root = new GameObject(name, typeof(RectTransform));
            root.transform.SetParent(parent, false);
            SetRect(root.GetComponent<RectTransform>(), Vector2.zero, Vector2.one);
            return root;
        }

        private static void SetRect(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax)
        {
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        private static Image CreateImage(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax,
            Color color, bool raycast, Sprite sprite = null)
        {
            var imageObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            imageObject.transform.SetParent(parent, false);
            SetRect(imageObject.GetComponent<RectTransform>(), anchorMin, anchorMax);
            Image image = imageObject.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = raycast;
            image.sprite = sprite;
            return image;
        }

        private static Text CreateText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax,
            string value, int fontSize, Color color, TextAnchor alignment, FontStyle style)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);
            SetRect(textObject.GetComponent<RectTransform>(), anchorMin, anchorMax);
            Text text = textObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = value;
            text.fontSize = fontSize;
            text.color = color;
            text.alignment = alignment;
            text.fontStyle = style;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.raycastTarget = false;
            return text;
        }

        private static Button CreateButton(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax,
            string label, Color background, Color foreground, int fontSize, Sprite sprite = null)
        {
            var buttonObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);
            SetRect(buttonObject.GetComponent<RectTransform>(), anchorMin, anchorMax);
            Image image = buttonObject.GetComponent<Image>();
            image.color = background;
            image.sprite = sprite;
            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            button.colors = ButtonColors(background, foreground);
            Text labelText = CreateText("Button Label", buttonObject.transform, new Vector2(0.04f, 0.04f),
                new Vector2(0.96f, 0.96f), label, fontSize, foreground, TextAnchor.MiddleCenter, FontStyle.Bold);
            labelText.raycastTarget = false;
            return button;
        }

        private static ColorBlock ButtonColors(Color normal, Color accent)
        {
            return new ColorBlock
            {
                normalColor = normal,
                highlightedColor = accent,
                pressedColor = new Color(0.75f, 0.5f, 0.25f, 1f),
                selectedColor = accent,
                disabledColor = new Color32(70, 67, 64, 210),
                colorMultiplier = 1f,
                fadeDuration = 0.08f
            };
        }
    }
}
