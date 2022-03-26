using BepInEx;

namespace DebuggingCommands
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private bool forceEnableChat;

        private void Awake()
        {
            On.uiChatBox.OnInputFinished += UiChatBoxOnOnInputFinished;
            On.uiStartGame.StartGame += OnGameStart;
            this.forceEnableChat = Config
                .Bind("Debugging Commands", "Force chat", true, "Enable chat even in single-player")
                .Value;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnGameStart(On.uiStartGame.orig_StartGame orig, uiStartGame self)
        {
            orig(self);
            if (!this.forceEnableChat)
            {
                return;
            }

            if (!uiChatBox.Instance)
            {
                uiStartGame.Instance.CreateChatBox();
                Logger.LogInfo("Force-enable chat box");
            }
        }

        private void UiChatBoxOnOnInputFinished(On.uiChatBox.orig_OnInputFinished orig, uiChatBox self, string _s)
        {
            if (Prefix(_s))
            {
                orig(self, _s);
            }
        }
        
        // stolen from https://github.com/CarbonNikm/FTK-Debugging-Commands/blob/main/FTKDebugCommands/FTKDebugCommands.cs
        static bool Prefix(string _s)
        {
            if (_s == "/partyheal")
            {
                EncounterSession.Instance.OverworldFullPartyHeal();
                EncounterSession.Instance.CombatFullPartyHeal();
                foreach (CharacterOverworld characterOverworld in FTKHub.Instance.m_CharacterOverworlds)
                {
                    if (!characterOverworld.m_WaitForRespawn)
                    {
                        characterOverworld.m_CharacterStats.PlayerFullHealCheat();
                    }
                }
                uiChatBox.Instance.m_InputBox.text = string.Empty;
                return false;
            }
            else if(_s.StartsWith("/give"))
            {
                string itemname = _s.Substring(6).ToLower();
                CharacterOverworld cow = GameLogic.Instance.GetCurrentCOW();
                int i = 0;
                foreach (Google2u.TextItemsRow row in Google2u.TextItems.Instance.Rows)
                {
                    if (row._en.ToString().ToLower().Equals(itemname))
                    {
                        string baseitemname = Google2u.TextItems.Instance.rowNames[i].Substring(4);
                        cow.AddItemToBackpack(GridEditor.FTK_itembase.GetEnum(baseitemname), cow);
                    }
                    i += 1;
                }
                uiChatBox.Instance.m_InputBox.text = string.Empty;
                return false;
            }
            else if(_s == "/reveal")
            {
                GameLogic.Instance.RevealMap();
                uiChatBox.Instance.m_InputBox.text = string.Empty;
                return false;
            }
            else if (_s == "/unreveal")
            {
                GameLogic.Instance.UnrevealMap();
                uiChatBox.Instance.m_InputBox.text = string.Empty;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
