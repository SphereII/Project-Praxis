using HarmonyLib;

namespace SphereII.HelpScreens
{
    /// <summary>
    /// Wires the Help button we append to the ESC menu.
    ///
    /// This is the one part of the help system XML cannot do. XUiC_InGameMenuWindow.Init
    /// looks up exactly ten buttons by name - btnOptions, btnSandboxSettings, btnExit and
    /// so on - and attaches a C# handler to each. A button appended from XML is never in
    /// that list, so it renders and highlights but does nothing on press.
    ///
    /// Nothing else here needs code: the window, its tab strip and the pages other modlets
    /// contribute are all XML on top of the vanilla XUiC_TabSelector.
    /// </summary>
    [HarmonyPatch(typeof(XUiC_InGameMenuWindow))]
    [HarmonyPatch(nameof(XUiC_InGameMenuWindow.Init))]
    public class XUiCInGameMenuWindowInit
    {
        /// <summary>Matches the button name appended in Config/XUi_InGame/windows.xml.</summary>
        private const string ButtonName = "btnSphereIIHelpScreens";

        /// <summary>The window_group registered in Config/XUi_InGame/xui.xml.</summary>
        public const string HelpWindowGroup = "helpScreens";

        public static void Postfix(XUiC_InGameMenuWindow __instance)
        {
            // Unlike vanilla's own lookups, this one is guarded. If our windows.xml patch
            // did not apply - a game update moving the ESC menu, or a load-order accident -
            // the button simply is not there, and the ESC menu must still work.
            var holder = __instance.GetChildById(ButtonName);
            if (holder == null)
            {
                Log.Warning($"[HelpScreens] '{ButtonName}' not found in the ESC menu; " +
                            "the Help button will not be available. Check that " +
                            "Config/XUi_InGame/windows.xml applied.");
                return;
            }

            var button = holder.GetChildByType<XUiC_SimpleButton>();
            if (button == null)
            {
                Log.Warning($"[HelpScreens] '{ButtonName}' has no XUiC_SimpleButton child.");
                return;
            }

            button.OnPressed += (_sender, _mouseButton) =>
            {
                // Same two steps vanilla uses for Sandbox Settings: drop the ESC menu, then
                // open ours modally. Opening without closing leaves the menu underneath and
                // its buttons still clickable.
                var windowManager = __instance.xui.playerUI.windowManager;
                windowManager.Close(__instance.windowGroup);
                windowManager.Open(HelpWindowGroup, _bModal: true);
            };
        }
    }
}
