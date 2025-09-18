# PlayerTrackerMod for Car Runners V2

**Educational, private/offline-use VR mod for displaying real-time player positions in Car Runners V2 (PC, Spring Cleaning Update).**

---

## ✨ Features

- **Real-time display** of all player positions (X, Y, Z) in a clean top-left overlay
- **Toggle overlay** with `F1` key
- **Configurable UI** via `config.json`
- **Minimal anti-cheat risk** (passive read-only)
- **Performance optimized** (60fps update)
- **Photon Networking** support (like Gorilla Tag mods)
- **Works with Car Runners V2 PC, Spring Cleaning update**
- **Unity 2021.3.x compatible**

---

## 🚨 Disclaimers

- **For educational and private/offline use only!**
- Do **NOT** use in online public matches. Respect developers, TOS, and other players.
- The author is **not responsible** for misuse or bans. You use this mod at your own risk.
- Intended for learning how to safely interact with Unity/Photon games in a non-invasive way.

---

## 🛠️ Installation

1. **Download the latest `PlayerTrackerMod.dll` release.**
2. Place `PlayerTrackerMod.dll` into your Car Runners V2 mods folder (typically `Car Runners V2/Mods/`).
3. (First run only) A `config.json` will be generated in `UserData/PlayerTrackerMod/`—edit as desired.
4. Launch the game. Overlay should appear in the top left. Press `F1` to toggle.

---

## 🧩 Compatibility

| Game Version          | Unity Version | Supported | Notes                 |
|---------------------- |--------------|-----------|-----------------------|
| Car Runners V2 (PC)   | 2021.3.x     | ✅        | Spring Cleaning OK    |
| Non-PC VR versions    | N/A          | ❌        | Not tested            |

---

## ⚙️ Configuration

Edit `UserData/PlayerTrackerMod/config.json` for UI tweaks:

```json
{
  "PanelX": 10.0,
  "PanelY": 10.0,
  "PanelWidth": 340.0,
  "PanelHeight": 220.0,
  "BackgroundAlpha": 0.35,
  "FontSize": 16
}
```

---

## 🧑‍💻 Building from Source

1. **Requirements:**
   - Visual Studio 2019+ (.NET 4.6.2)
   - Unity 2021.3.x DLLs (`UnityEngine.dll`, `UnityEngine.UI.dll`)
   - `PhotonUnityNetworking.dll`
   - [`Newtonsoft.Json`](https://www.newtonsoft.com/json)
2. **Clone repo and open `PlayerTrackerMod.sln`**
3. Reference the correct DLLs in project properties (see `.csproj`).
4. Build in `Release|x64` mode.
5. Copy the resulting `PlayerTrackerMod.dll` to your mods folder.

---

## 🐞 Troubleshooting

- **Overlay not showing?**
  - Ensure mod is in correct folder.
  - Check game version and mod loader compatibility.
- **Game crashes or errors?**
  - Check logs for missing dependencies (`PhotonUnityNetworking.dll`, `Newtonsoft.Json.dll`).
- **Player positions not updating?**
  - Only works in rooms using Photon networking.
  - Some custom game modes may not be supported.
- **Anti-cheat warnings?**
  - This mod is passive/read-only, but always use in private/offline settings to avoid bans.

---

## 🤝 Support & Community

- [GitHub Issues](https://github.com/loverman4825/PlayerTrackerMod/issues) for bug reports/requests
- General VR modding: [r/VRmodding](https://www.reddit.com/r/VRmodding/)
- Learn more: [Photon Unity Networking Docs](https://doc.photonengine.com/en-us/pun/v2)

---

## 📝 License

This project is licensed under the [MIT License](LICENSE).

---

**Not affiliated with Car Runners V2 or its developers. Use responsibly and ethically.**
