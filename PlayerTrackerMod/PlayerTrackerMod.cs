using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Newtonsoft.Json;

namespace PlayerTrackerMod
{
    public class PlayerTracker : MonoBehaviourPunCallbacks
    {
        private GameObject uiCanvas;
        private Text playerText;
        private bool uiVisible = true;
        private float updateInterval = 1.0f / 60.0f; // 60fps
        private float lastUpdateTime = 0f;
        private PlayerTrackerConfig config;

        private static string configPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserData", "PlayerTrackerMod", "config.json");

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            LoadConfig();
            CreateUI();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                uiVisible = !uiVisible;
                uiCanvas.SetActive(uiVisible);
            }

            if (!uiVisible || Time.unscaledTime - lastUpdateTime < updateInterval)
                return;

            lastUpdateTime = Time.unscaledTime;
            UpdatePlayerList();
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    config = JsonConvert.DeserializeObject<PlayerTrackerConfig>(File.ReadAllText(configPath));
                }
                else
                {
                    config = PlayerTrackerConfig.Default();
                    Directory.CreateDirectory(Path.GetDirectoryName(configPath));
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("[PlayerTrackerMod] Failed to load config, using defaults: " + e.Message);
                config = PlayerTrackerConfig.Default();
            }
        }

        private void CreateUI()
        {
            uiCanvas = new GameObject("PlayerTrackerUI");
            Canvas canvas = uiCanvas.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            uiCanvas.AddComponent<CanvasScaler>();
            uiCanvas.AddComponent<GraphicRaycaster>();
            DontDestroyOnLoad(uiCanvas);

            GameObject panel = new GameObject("Panel");
            panel.transform.SetParent(uiCanvas.transform, false);
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0, 0, 0, config.BackgroundAlpha);

            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(0, 1);
            panelRect.pivot = new Vector2(0, 1);
            panelRect.anchoredPosition = new Vector2(config.PanelX, -config.PanelY);
            panelRect.sizeDelta = new Vector2(config.PanelWidth, config.PanelHeight);

            GameObject textGO = new GameObject("PlayerText");
            textGO.transform.SetParent(panel.transform, false);
            playerText = textGO.AddComponent<Text>();
            playerText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            playerText.fontSize = config.FontSize;
            playerText.color = Color.white;
            playerText.alignment = TextAnchor.UpperLeft;
            playerText.horizontalOverflow = HorizontalWrapMode.Overflow;
            playerText.verticalOverflow = VerticalWrapMode.Overflow;

            RectTransform textRect = textGO.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(5, 5);
            textRect.offsetMax = new Vector2(-5, -5);
        }

        private void UpdatePlayerList()
        {
            if (PhotonNetwork.InRoom)
            {
                var playerEntries = new List<string>();
                foreach (var p in PhotonNetwork.PlayerListOthers.Concat(new[] { PhotonNetwork.LocalPlayer }))
                {
                    try
                    {
                        GameObject playerObj = FindPlayerObject(p);
                        if (playerObj != null)
                        {
                            Vector3 pos = playerObj.transform.position;
                            playerEntries.Add($"{p.NickName}: X={pos.x:F2} Y={pos.y:F2} Z={pos.z:F2}");
                        }
                        else
                        {
                            playerEntries.Add($"{p.NickName}: (not found)");
                        }
                    }
                    catch
                    {
                        playerEntries.Add($"{p.NickName}: (error)");
                    }
                }
                playerText.text = string.Join("\n", playerEntries);
            }
            else
            {
                playerText.text = "Not in room.";
            }
        }

        private GameObject FindPlayerObject(Player player)
        {
            // Attempt to find the PhotonView owned by this player
            foreach (var view in FindObjectsOfType<PhotonView>())
            {
                if (view.Owner == player && view.gameObject.CompareTag("Player"))
                    return view.gameObject;
            }
            // Fallback: try all objects with "Player" tag
            var candidates = GameObject.FindGameObjectsWithTag("Player");
            return candidates.FirstOrDefault();
        }
    }

    [Serializable]
    public class PlayerTrackerConfig
    {
        public float PanelX = 10f;
        public float PanelY = 10f;
        public float PanelWidth = 340f;
        public float PanelHeight = 220f;
        public float BackgroundAlpha = 0.35f;
        public int FontSize = 16;

        public static PlayerTrackerConfig Default() => new PlayerTrackerConfig();
    }
}
