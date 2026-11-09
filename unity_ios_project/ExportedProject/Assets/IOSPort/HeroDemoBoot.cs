using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

namespace Dota.Port
{
    public sealed class HeroDemoBoot : MonoBehaviour
    {
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Camera demoCamera;

        private const float MenuCameraYaw = -14f;
        private static readonly Color32 Card = new Color32(28, 28, 30, 248);
        private static readonly Color32 Inset = new Color32(18, 18, 20, 255);
        private static readonly Color32 Hairline = new Color32(56, 56, 58, 255);
        private static readonly Color32 Blue = new Color32(10, 132, 255, 255);
        private static readonly Color32 Pill = new Color32(17, 36, 59, 255);
        private static readonly Color32 Selected = new Color32(99, 99, 102, 255);
        private static readonly Color32 White = new Color32(255, 255, 255, 255);
        private static readonly Color32 Muted = new Color32(174, 174, 178, 255);

        private Transform hero;
        private Vector3 destination;
        private Vector3 heroSpawnPosition;
        private Quaternion heroSpawnRotation;
        private Vector3 cameraOffset;
        private float cameraPitch;
        private GameObject launcherCard;
        private GameObject playPage;
        private GameObject infoPage;
        private Button playTab;
        private Button infoTab;
        private Image playTabImage;
        private Image infoTabImage;
        private Text playTabLabel;
        private Text infoTabLabel;
        private Button startButton;
        private Button backButton;
        private bool demoStarted;
        private bool ignorePointerUntilRelease;
        private int finger = -1;

        private void Start()
        {
            if (!heroPrefab || !spawnPoint || !demoCamera)
            {
                Debug.LogError("[V14Demo] Missing hero prefab, spawn point, or camera.");
                enabled = false;
                return;
            }

            hero = Instantiate(heroPrefab, spawnPoint.position, spawnPoint.rotation).transform;
            hero.gameObject.SetActive(true);
            AlignHeroToStage();
            FitCameraToHero();
            heroSpawnPosition = hero.position;
            heroSpawnRotation = hero.rotation;
            destination = hero.position;
            demoCamera.enabled = true;
            SetCameraYaw(MenuCameraYaw);
            FollowHero();
            BuildLauncher();
            Debug.Log($"[V14Demo] Spawned {heroPrefab.name} with the v1.4 launcher.");
            StartCoroutine(ReportFirstFrame());
        }

        private System.Collections.IEnumerator ReportFirstFrame()
        {
            yield return new WaitForEndOfFrame();
            Debug.Log($"[V14Demo] First frame completed ({SystemInfo.graphicsDeviceType})");
        }

        private void Update()
        {
            if (!demoStarted || !hero)
                return;

#if ENABLE_LEGACY_INPUT_MANAGER
            if (ignorePointerUntilRelease)
            {
                if (Input.touchCount == 0 && !Input.GetMouseButton(0))
                    ignorePointerUntilRelease = false;
            }
            else
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (finger == -1 && touch.phase == UnityEngine.TouchPhase.Began)
                        finger = touch.fingerId;
                    if (touch.fingerId != finger)
                        continue;
                    if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Moved)
                        SetDestination(touch.position);
                    if (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled)
                        finger = -1;
                }

#if UNITY_EDITOR || UNITY_STANDALONE
                if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
                    SetDestination(Input.mousePosition);
#endif
            }
#elif ENABLE_INPUT_SYSTEM
            var pointer = Pointer.current;
            if (ignorePointerUntilRelease)
            {
                if (pointer == null || !pointer.press.isPressed)
                    ignorePointerUntilRelease = false;
            }
            else if (pointer != null && pointer.press.isPressed)
            {
                SetDestination(pointer.position.ReadValue());
            }
#endif

            Vector3 next = Vector3.MoveTowards(hero.position, destination, 5f * Time.deltaTime);
            Vector3 heading = next - hero.position;
            heading.y = 0f;
            if (heading.sqrMagnitude > 0.000001f)
                hero.rotation = Quaternion.LookRotation(heading);
            hero.position = next;
            FollowHero();
        }

        private void BuildLauncher()
        {
            EnsureEventSystem();

            var canvasObject = new GameObject("Dota 2 Mobile Launcher", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 500;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            launcherCard = CreateImage("Launcher Card", canvasObject.transform, new Vector2(0.055f, 0.07f), new Vector2(0.50f, 0.93f), Card, true).gameObject;

            CreateText("Game Title", launcherCard.transform, new Vector2(0.07f, 0.84f), new Vector2(0.60f, 0.96f),
                "DOTA 2 MOBILE", 34, White, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText("Build Label", launcherCard.transform, new Vector2(0.07f, 0.77f), new Vector2(0.90f, 0.84f),
                "VERSION 1.4  •  iOS DEMO", 18, Blue, TextAnchor.MiddleLeft, FontStyle.Bold);

            var reset = CreateButton("Reset", launcherCard.transform, new Vector2(0.62f, 0.865f), new Vector2(0.78f, 0.94f),
                "RESET", Pill, White, 18);
            reset.onClick.AddListener(ResetDemo);
            var play = CreateButton("Play", launcherCard.transform, new Vector2(0.80f, 0.865f), new Vector2(0.94f, 0.94f),
                "PLAY", Blue, White, 18);
            play.onClick.AddListener(StartDemo);

            CreateImage("Header Separator", launcherCard.transform, new Vector2(0.07f, 0.735f), new Vector2(0.93f, 0.738f), Hairline, false);

            playTab = CreateButton("Play Tab", launcherCard.transform, new Vector2(0.07f, 0.655f), new Vector2(0.48f, 0.72f),
                "PLAY", Selected, White, 18);
            infoTab = CreateButton("Info Tab", launcherCard.transform, new Vector2(0.52f, 0.655f), new Vector2(0.93f, 0.72f),
                "INFO", Pill, Muted, 18);
            playTabImage = playTab.GetComponent<Image>();
            infoTabImage = infoTab.GetComponent<Image>();
            playTabLabel = playTab.GetComponentInChildren<Text>();
            infoTabLabel = infoTab.GetComponentInChildren<Text>();
            playTab.onClick.AddListener(() => ShowInfo(false));
            infoTab.onClick.AddListener(() => ShowInfo(true));

            playPage = CreateImage("Play Page", launcherCard.transform, new Vector2(0.07f, 0.255f), new Vector2(0.93f, 0.625f), Inset, true).gameObject;
            CreateText("Hero Demo Title", playPage.transform, new Vector2(0.08f, 0.58f), new Vector2(0.92f, 0.91f),
                "V1.4 HERO DEMO", 29, White, TextAnchor.MiddleLeft, FontStyle.Bold);
            CreateText("Hero Demo Description", playPage.transform, new Vector2(0.08f, 0.20f), new Vector2(0.92f, 0.58f),
                $"Showcase {heroPrefab.name} from the new APK assets. Tap or drag to move.", 19, Muted, TextAnchor.MiddleLeft, FontStyle.Normal);
            startButton = CreateButton("Enter Demo", launcherCard.transform, new Vector2(0.07f, 0.135f), new Vector2(0.93f, 0.235f),
                "ENTER HERO DEMO", Blue, White, 22);
            startButton.onClick.AddListener(StartDemo);

            infoPage = CreateImage("Info Page", launcherCard.transform, new Vector2(0.07f, 0.255f), new Vector2(0.93f, 0.625f), Inset, true).gameObject;
            CreateText("Build Details", infoPage.transform, new Vector2(0.08f, 0.08f), new Vector2(0.92f, 0.92f),
                "SOURCE     APK v1.4\nMODE        HERO SHOWCASE\nINPUT        TOUCH MOVE\nGAMEPLAY  MATCH LOGIC NOT INCLUDED",
                18, White, TextAnchor.MiddleLeft, FontStyle.Normal);
            infoPage.SetActive(false);

            CreateImage("Footer Separator", launcherCard.transform, new Vector2(0.07f, 0.115f), new Vector2(0.93f, 0.118f), Hairline, false);
            CreateText("Footer", launcherCard.transform, new Vector2(0.07f, 0.04f), new Vector2(0.93f, 0.105f),
                "APPLE METAL  •  LANDSCAPE", 15, Muted, TextAnchor.MiddleLeft, FontStyle.Normal);

            backButton = CreateButton("Back To Menu", canvasObject.transform, new Vector2(0.82f, 0.88f), new Vector2(0.96f, 0.95f),
                "MENU", Blue, White, 18);
            backButton.onClick.AddListener(ReturnToMenu);
            backButton.gameObject.SetActive(false);
        }

        private static void EnsureEventSystem()
        {
            var eventSystem = EventSystem.current;
            if (!eventSystem)
                eventSystem = new GameObject("Dota 2 Mobile EventSystem", typeof(EventSystem)).GetComponent<EventSystem>();

#if ENABLE_INPUT_SYSTEM
            var inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (!inputModule)
            {
                foreach (var oldModule in eventSystem.GetComponents<BaseInputModule>())
                    oldModule.enabled = false;
                inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }
            inputModule.AssignDefaultActions();
#else
            if (!eventSystem.GetComponent<BaseInputModule>())
                eventSystem.gameObject.AddComponent<StandaloneInputModule>();
#endif
        }

        private void ShowInfo(bool showInfo)
        {
            playPage.SetActive(!showInfo);
            infoPage.SetActive(showInfo);
            playTabImage.color = showInfo ? Pill : Selected;
            infoTabImage.color = showInfo ? Selected : Pill;
            playTabLabel.color = showInfo ? Muted : White;
            infoTabLabel.color = showInfo ? White : Muted;
        }

        private void StartDemo()
        {
            demoStarted = true;
            ignorePointerUntilRelease = true;
            SetCameraYaw(0f);
            FollowHero();
            launcherCard.SetActive(false);
            backButton.gameObject.SetActive(true);
            destination = hero.position;
        }

        private void ReturnToMenu()
        {
            hero.position = heroSpawnPosition;
            hero.rotation = heroSpawnRotation;
            destination = hero.position;
            SetCameraYaw(MenuCameraYaw);
            FollowHero();
            demoStarted = false;
            launcherCard.SetActive(true);
            backButton.gameObject.SetActive(false);
            ShowInfo(false);
        }

        private void ResetDemo()
        {
            if (demoStarted)
                ReturnToMenu();
            else
            {
                hero.position = heroSpawnPosition;
                hero.rotation = heroSpawnRotation;
                destination = hero.position;
                FollowHero();
                ShowInfo(false);
            }
        }

        private void SetDestination(Vector2 screenPosition)
        {
            var plane = new Plane(Vector3.up, spawnPoint.position);
            Ray ray = demoCamera.ScreenPointToRay(screenPosition);
            if (!plane.Raycast(ray, out float distance))
                return;

            Vector3 offset = ray.GetPoint(distance) - spawnPoint.position;
            destination = spawnPoint.position + Vector3.ClampMagnitude(offset, 25f);
        }

        private void FollowHero()
        {
            demoCamera.transform.position = hero.position + cameraOffset;
        }

        private void SetCameraYaw(float yaw)
        {
            demoCamera.transform.rotation = Quaternion.Euler(cameraPitch, yaw, 0f);
        }

        private void AlignHeroToStage()
        {
            var renderers = hero.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
                return;

            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
                bounds.Encapsulate(renderers[i].bounds);

            hero.position += Vector3.up * (spawnPoint.position.y - bounds.min.y);
        }

        private void FitCameraToHero()
        {
            var renderers = hero.GetComponentsInChildren<Renderer>(true);
            float height = 2f;
            if (renderers.Length > 0)
            {
                Bounds bounds = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++)
                    bounds.Encapsulate(renderers[i].bounds);
                height = Mathf.Max(bounds.size.y, 1f);
            }

            cameraOffset = new Vector3(0f, height * 1.15f, -height * 2.85f);
            cameraPitch = Mathf.Atan2(height * 0.65f, height * 2.85f) * Mathf.Rad2Deg;
            demoCamera.fieldOfView = 36f;
        }

        private static Image CreateImage(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Color color, bool raycast)
        {
            var objectWithImage = new GameObject(name, typeof(RectTransform), typeof(Image));
            objectWithImage.transform.SetParent(parent, false);
            var rect = objectWithImage.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            var image = objectWithImage.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = raycast;
            return image;
        }

        private static Text CreateText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax,
            string value, int fontSize, Color color, TextAnchor alignment, FontStyle style)
        {
            var textObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);
            var rect = textObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            var text = textObject.GetComponent<Text>();
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
            string label, Color background, Color foreground, int fontSize)
        {
            var buttonObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);
            var rect = buttonObject.GetComponent<RectTransform>();
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = buttonObject.GetComponent<Image>();
            image.color = background;
            image.raycastTarget = true;
            var button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            button.colors = new ColorBlock
            {
                normalColor = background,
                highlightedColor = new Color32(65, 145, 220, 255),
                pressedColor = new Color32(5, 94, 184, 255),
                selectedColor = background,
                disabledColor = new Color32(75, 75, 78, 255),
                colorMultiplier = 1f,
                fadeDuration = 0.08f
            };

            var labelText = CreateText("Label", buttonObject.transform, Vector2.zero, Vector2.one,
                label, fontSize, foreground, TextAnchor.MiddleCenter, FontStyle.Bold);
            labelText.raycastTarget = false;
            return button;
        }
    }
}
