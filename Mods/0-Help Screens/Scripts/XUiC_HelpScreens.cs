using UnityEngine.Scripting;

/// <summary>
/// Controller for the help screen window. Its only job is the Close button.
///
/// Deliberately in the GLOBAL namespace, and named with the XUiC_ prefix, because of how
/// XML resolves a controller. XUiFromXml calls
/// ReflectionHelpers.GetTypeWithPrefix("XUiC_", value), which inserts the prefix ahead of
/// the class name and hands the result to Type.GetType. Type.GetType only searches
/// Assembly-CSharp and mscorlib unless the name is assembly qualified - hence the XML
/// says:
///
///     controller="HelpScreens, SphereIIHelpScreens"
///
/// which becomes the lookup "XUiC_HelpScreens, SphereIIHelpScreens". A namespace here
/// would have to appear in that XML string too. SCore uses the same convention
/// (controller="SCoreCompanion, SCore").
///
/// [Preserve] keeps the type through IL stripping. Without it the class can vanish from a
/// stripped build and the controller silently falls back to the base XUiController.
/// </summary>
[Preserve]
public class XUiC_HelpScreens : XUiController
{
    /// <summary>Matches the button name in Config/XUi_InGame/windows.xml.</summary>
    private const string CloseButtonName = "btnClose";

    /// <summary>Fallback if the ESC menu has not stamped its static ID yet.</summary>
    private const string InGameMenuGroupFallback = "ingameMenu";

    public override void Init()
    {
        base.Init();

        var holder = GetChildById(CloseButtonName);
        var button = holder?.GetChildByType<XUiC_SimpleButton>();
        if (button == null)
        {
            Log.Warning($"[HelpScreens] '{CloseButtonName}' not found in the help window; " +
                        "it will only be closable with ESC.");
            return;
        }

        button.OnPressed += (_sender, _mouseButton) =>
        {
            // Close ourselves, then put the pause menu back - the reverse of how
            // XUiC_InGameMenuWindow opened us.
            var windowManager = xui.playerUI.windowManager;
            windowManager.Close(windowGroup);

            // XUiC_InGameMenuWindow.ID is a static stamped from its own window group during
            // Init, so it is populated by the time any window can be clicked. The literal is
            // only a guard against that assumption changing.
            var menuId = string.IsNullOrEmpty(XUiC_InGameMenuWindow.ID)
                ? InGameMenuGroupFallback
                : XUiC_InGameMenuWindow.ID;

            windowManager.Open(menuId, _bModal: true);
        };
    }
}
