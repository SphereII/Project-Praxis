ZZ-ModInfo — Project Praxis
===========================

The front page for **Project Praxis**, a proof-of-concept survival overhaul in which skills
come from use rather than from spending points, and fade if you stop using them.

The modlet folder keeps the `ZZ-` name for load-order reasons (see below); *Project Praxis*
is what the pack is called, and what `ModInfo.xml` presents to players.

It describes the pack on the main menu and does nothing else — no blocks, no items, no
recipes, no code. Removing it changes how the pack is *described*, never how it *plays*.

## What it does

1. Switches on the **Mod Summary** panel that `0-Help Screens` provides on the main menu.
   That panel ships hidden, because an empty box helps nobody.
2. Fills all four of its tabs: **About**, **Progression**, **Comfort** and **Challenges**.

Both are done from `Config/XUi_Menu/windows.xml` with XPath patches. There is no C# and no
window of its own — the panel belongs to `0-Help Screens`; this modlet only enables and
populates it.

## Why it is separate

The text used to live in `SphereII A Round World`. That was wrong. A Round World is a
standalone quality-of-life modlet that people install on its own, and it had no business
speaking for a ten-modlet overhaul it merely ships inside. Pack-level text belongs to the
pack.

Keeping it apart also means the wording can change without touching — or re-releasing — any
modlet that actually does something.

## The name

`ZZ-` so it sorts last. Mods apply in alphabetical order of folder name, and a modlet
patching a window that does not exist yet is skipped in full and *silently*, so anything
contributing to the panel has to load after `0-Help Screens` declares it. Sorting last also
gives this modlet the final word if anything else touches the same tabs.

## Editing the text

All of it is in `Config/Localization.csv`, keyed `zzPack*`. Literal `\n` becomes a real
newline. Pages scroll, so length is not a constraint — the Comfort page is the longest at
about 2,400 characters.

**The panel is full at four tabs.** A fifth would hand `XUiC_TabSelectorTab.TabSelected` a
null `TabButton` — it indexes `Tabs[i]` against `tabButtons[i]` with no guard. To go
further, raise both of these first, and note captions shrink to fit their cell so five is
about the limit at 450px wide:

```xml
<set xpath="/windows/window[@name='mainMenuHelpScreens']//grid[@name='tabButtons']/@cols">5</set>
<set xpath="/windows/window[@name='mainMenuHelpScreens']//grid[@name='tabButtons']/@cell_width">86</set>
```

## Two things not to lose

- **The ZZTong attribution is not decoration.** `ZZTong-Prefabs` is GPLv3 and its README
  asks that anyone bundling the POIs credit the author, naming the handle "ZZTong" or
  "zztong" specifically. The Mod Summary names both the modlet and the author — in the
  opening line of About, and again on the Comfort page. It is the only third-party content
  in this pack.

  Upstream: <https://github.com/zztong/7d2d-prefabs>

  The link lives here and **not** in the Mod Summary panel, deliberately. Making text in
  that panel clickable would mean giving SCore a generic open-a-URL action, and that would
  drop "can launch a browser" from DLL-tier to XML-tier for every modlet depending on it —
  XML mods run with EAC enabled, so they reach a far wider and less cautious audience than
  anything shipping an assembly. Not a trade worth making so a credit line can be clicked,
  especially as ZZTong asks for credit rather than a link.
- **The NPC challenges warning.** `SphereII Challenges` ships eight NPC-hiring challenges
  and this pack installs no NPC modlet — there is no `entity_class` starting with `npc`
  anywhere in it. The Challenges page says so, rather than leaving players hunting for
  something that is not there.

## Keeping it accurate

`Config/XUi_Menu/windows.xml` carries the pack manifest in a comment. When the pack gains or
loses a modlet, update that list and the text together. The ZZTong counts (233 POIs, 128
decorations, 58 RWG tiles) were taken from the `.tts` files under its `Prefabs` folder — its
README gives no totals — and the page rounds them, so a version bump does not immediately
make it wrong.