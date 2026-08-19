# Help Screens

An in-game help system that other modlets add their own sections to.

This modlet owns the shell — an ESC-menu button, the window, and its two levels of
navigation. Every other modlet contributes content; none of them ship UI of their own.

## For players

ESC → **Help**. The left column lists each installed modlet that has contributed help;
picking one shows its pages as tabs along the top.

## For modders — adding a section

Your section becomes a **button in the left column**, and each page inside it becomes a
**tab along the top**. You never declare a button at either level — both strips fill
themselves in from what you add.

**1. Append your section** in `Config/XUi_InGame/windows.xml`. Copy this whole block and
change the marked lines:

```xml
<configs>
    <append xpath="/windows/window[@name='helpScreens']//rect[@name='nav']/rect[@name='tabsContents']">

        <rect name="sectionYourMod" controller="TabSelectorTab" tab_key="yourModHelpSection">
            <rect name="tabs" width="964" height="655" controller="TabSelector"
                  select_tab_contents_on_open="false" select_tab_contents_on_change="false">

                <rect name="tabsHeader" height="38" depth="0">
                    <grid name="tabButtons" pos="3,-2" depth="2" rows="1" cols="4"
                          cell_width="240" cell_height="36" repeat_content="true"
                          arrangement="horizontal">
                        <helpscreen_pagebutton />
                    </grid>
                </rect>

                <rect name="tabsContents" depth="2" pos="0,-40" width="964" height="615">
                    <sprite depth="0" name="pageBackground" sprite="menu_empty3px"
                            color="[mediumGrey]" type="sliced" fillcenter="true" />

                    <!-- your pages, up to cols= above -->
                    <helpscreen_page tab_key="yourModOverview" text_key="yourModOverviewBody" />
                    <helpscreen_page tab_key="yourModTips"     text_key="yourModTipsBody" />
                </rect>
            </rect>
        </rect>

    </append>
</configs>
```

Only four things change per modlet: `name="sectionYourMod"`, the section's `tab_key`, and
the `helpscreen_page` lines. Everything else is fixed scaffolding.

**2. Add your localization** to `Config/Localization.csv`:

```
yourModHelpSection,"Your Mod"
yourModOverview,"Overview"
yourModOverviewBody,"First paragraph.\n\nSecond paragraph."
yourModTips,"Tips"
yourModTipsBody,"..."
```

`tab_key` doubles as the caption at both levels, so there is no separate label to declare.
Literal `\n` becomes a real newline.

### Why the block, rather than one template call

Two engine rules meet here. A template instantiation may not have child nodes —
`XUiFromXml.createFromTemplate` returns an empty view if it does, warning only under
`-debugxui` — so a section template could not wrap page templates. And XPath patching runs
before templates expand, so nothing can append pages into a template's internals either.
The section shell therefore has to be literal XML. The two pieces that *can* be templates,
`helpscreen_page` and `helpscreen_pagebutton`, are.

### Constraints

- **Your folder must sort after `0-Help Screens`.** Mods load in alphabetical order of
  folder name, and a mod patching a window that does not exist yet is skipped in full,
  silently. This modlet is named to sort near the front for exactly that reason.
- **Twenty sections, four pages each.** The left column scrolls, so its limit is the
  `rows` attribute rather than the window height. The page strip does not scroll, so its
  `cols` must keep fitting across the 964px section — four 240px tabs. Raise either from
  your own file:

  ```xml
  <set xpath="/windows/window[@name='helpScreens']//rect[@name='nav']/rect[@name='tabsHeader']//grid[@name='tabButtons']/@rows">30</set>
  ```

  Never let sections or pages outnumber their buttons — the surplus gets a null
  `TabButton`, which `XUiC_TabSelectorTab.TabSelected` dereferences unguarded. Surplus
  *buttons* are free; they hide themselves.
- **Anchor your xpath on `rect[@name='nav']`.** `tabsContents` on its own also matches the
  one inside every section, including yours.

For a page that needs more than scrolling text — grids, live bindings, buttons — skip
`helpscreen_page` and write your own `<rect controller="TabSelectorTab" tab_key="...">`.

## The main menu panel — Mod Summary

There is a second, smaller surface: a tabbed box on the **main menu**, titled *Mod Summary*,
for the things worth knowing *before* starting a game. Same contract, one level instead of
two — every page is a top-level tab, so a modlet contributing here gets tabs, not a section.

It is aimed squarely at **overhauls**: this is where you say what your overhaul is, how it
expects to be played, and anything a player should decide on before they start. A summary,
not a manual — the in-game screen is where the detail belongs.

**The panel ships hidden.** An empty box on the menu helps nobody, so a pack opts in:

```xml
<set xpath="/windows/window[@name='mainMenuHelpScreens']/@visible">true</set>
```

Enabling it is a statement of ownership — you're taking responsibility for what's in it.
**Replace the About body while you're there**, since tab 0 is what opens and it should be
your description, not a placeholder about the panel:

```xml
<set xpath="/windows/window[@name='mainMenuHelpScreens']//helpmenu_page[@tab_key='helpMenuAbout']/@text_key">yourPackOverviewBody</set>
```

That patch reaches a template *instantiation* — an ordinary node with ordinary attributes
until templates expand, and patching runs before that. (The same timing is why nothing can
append *into* a template.) Then spend the remaining cells on detail pages. A modlet can still
contribute a tab without enabling anything; it stays unseen until some pack turns the panel on.

**Append your pages** in `Config/XUi_Menu/windows.xml`:

```xml
<configs>
    <conditional>
        <if cond="mod_loaded('sphereii_help_screens')">
            <append xpath="/windows/window[@name='mainMenuHelpScreens']//rect[@name='tabsContents']">
                <helpmenu_page tab_key="yourModMenuOverview" text_key="yourModMenuOverviewBody" />
            </append>
        </if>
    </conditional>
</configs>
```

Then two localization rows per page, exactly as in-game: `tab_key` is the caption, `text_key`
is the body. Pages scroll, so length is not a constraint.

### Constraints, which differ from the in-game screen

- **Four tabs, and the About page holds one of them.** The strip is a grid of fixed 107px
  cells — it does not scroll. Raising it is a one-liner from your own file, `cols` and
  `cell_width` together, but past five or so the cells stop being wide enough to read.
- **Keep captions short — your mod's name is the intent.** A caption too long for its cell
  shrinks to fit rather than overrunning its neighbour, so a long name costs legibility, not
  layout.
- **Never add more pages than there are cells.** Surplus buttons hide themselves; surplus
  *pages* get a null `TabButton` that `XUiC_TabSelectorTab.TabSelected` dereferences
  unguarded.
- The same alphabetical load-order rule applies — your folder must sort after `0-Help Screens`.

Use this panel for what someone needs to decide before starting, and the in-game screen for
the full reference. `SphereII Learn By Doing` does exactly that: three menu tabs, four in-game
pages, sharing no localization keys.

## Implementation notes

Both navigation levels are vanilla `XUiC_TabSelector`. Nesting one inside another is safe
because `GetChildControllers` adds a match and then stops descending, so the outer selector
collects section rects and never reaches an inner selector's own tabs. The two levels use
different gamepad bindings (`use_page_buttons="true"` on the nav) because `Update` is gated
only on visibility, so sharing one binding would step both at once.

The only C# is the ESC-menu button and the Close button:

- `Harmony/XUiC_InGameMenuWindow_HelpButton.cs` — `XUiC_InGameMenuWindow.Init` wires exactly
  ten buttons by name, so a button appended from XML is inert. This postfix supplies the
  handler.
- `Scripts/XUiC_HelpScreens.cs` — the window's own controller, reached from XML by
  `controller="HelpScreens, SphereIIHelpScreens"`. Wires Close.

Because the modlet ships an assembly, it requires EAC to be disabled.

See `Mods/Ideas/XUI_SYSTEM_REFERENCE.md` for the wider XUi reference this was built from.

## Version

Version: TBD
