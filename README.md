# Project Praxis

**A survival overhaul for 7 Days to Die where skills come from use, not from spending points — and fade if you stop.**

*Praxis* means knowledge put into practice: what you can do, rather than what you know on paper.

You no longer spend points on a build. You arrive at one by using the skills you want.

---

## ⚠️ This is a proof of concept

It is playable, and it is being played, but it is **not finished and not balanced**. Progression
rates especially are still being tuned — they have already moved a long way once — and nothing
here promises that a world you start today will survive the next update.

**Treat a save in this pack as one you are willing to lose.**

That cuts both ways, and it is the useful part. If a skill climbs absurdly fast, or sits still for
a whole day while you work at it, that is worth [saying out loud](../../issues) rather than quietly
playing around. Finding those is what this build is for.

None of this makes the game easier in the ordinary sense. Zombies hit as hard as they always did,
the blood moon still comes, and the world is still trying to kill you. What changes is how you get
good at surviving it.

---

## What it does

### Progression — the part that will feel unfamiliar

Swing a club and you get better with clubs. Mine ore and you get better at mining. Every perk levels
itself from the actions it governs, and the attribute above it rises from anything in its tree — so
committing to a few perks pulls their parent up with them.

- **You cannot buy any of it.** Perks and attributes are no longer for sale, and levelling up awards
  no skill points to spend. The skill window becomes somewhere you look at what you have earned
  rather than somewhere you shop.
- **Skills fade if you abandon them.** Every skill carries a hidden inactivity timer. Stop using
  something long enough and it slips back down. A wide, shallow character is harder to hold together
  than a narrow, deep one, because everything you are not currently doing is quietly cooling off.
- **Your first few days decide a lot.** Pick the weapon you actually enjoy and use it, rather than
  saving it for later.

Levelling still matters even without points to spend. Level requirements, attribute caps and perk
prerequisites are all unchanged, so your level still governs what you are allowed to reach.

### Comfort and quality of life

Meant to remove chores rather than difficulty.

- Crafting reads from containers near you, so materials no longer have to be hauled into your own
  inventory first. Block upgrades and repairs read from them too — though not while enemies are
  close, because shoring up a base is something you do between fights rather than during one.
- The **Drop Box** sorts for you. Anything put inside is moved out into nearby containers that
  already hold that item. Comes in wood, iron and steel.
- Books are shared across a party, so a group does not need duplicates of the same volume.
- Camera and weapon sway are switched off. Flickering and pulsing POI lights hold steady. Whatever
  you are aiming at shows a health bar.
- Locked containers can be picked with a minigame instead of simply refusing you.
- Chuck and the Rancher have lost their ranged attacks and have to reach you to hurt you.

### Less to flinch at

Scenes implying that a person killed another person, or took their own life, are removed — the
bodies hung from posts, and the noose. Ordinary death is left alone: corpses, blood and bones are
all still there, because a body that fell to the outbreak says nothing about anyone having done it
to them. Language is softened, and one trader has been persuaded to be civil.

### Challenges

Around eighty extra challenges sit alongside the ones the game ships with, in three new categories.

| Category | Roughly | What it asks for |
| --- | --- | --- |
| **Ability** | 50 | Ten kills with one particular weapon, down every attribute tree. A bonus group asks for stealth instead — sneak kills with melee and ranged, then unbroken streaks. |
| **SCore** | 20 | Decapitation with blade, gun and bow. Crafting with particular ingredients. Starting, sustaining and extinguishing fires. Harvesting, gathering, wearing armour, clearing every sleeper from a POI. |
| **NPC** | 8 | Hiring companions. **These cannot be completed in this pack** — see below. |

Most pay 1000 experience and redeem themselves the moment you finish them; there is no trip back to
the challenge window to claim anything.

> **The eight NPC challenges cannot be completed here.** They need an NPC mod to supply someone to
> hire, and none is installed. The category is listed and its challenges are visible, but nothing in
> it will ever tick over. It is left in place so the challenges are there if you add an NPC pack
> yourself.

A few entries in the SCore category are development tests rather than real challenges, and show
placeholder text where their description should be.

### A wider world

[ZZTong-Prefabs](https://www.nexusmods.com/7daystodie/mods/1434/) is bundled in, adding **233 points
of interest, 128 roadside decorations and 58 RWG tiles**. World generation mixes them with the stock
ones on its own — nothing needs configuring, generate a world and they are in it.

Two things worth knowing. The decorations are scenery rather than quest locations and take the place
of nothing, so having them costs you no POIs. And some of the hunters' traps scattered among them
are genuinely lethal, so treat an unfamiliar contraption in the woods with more respect than it
appears to deserve.

---

## What's in the pack

| Modlet | Role |
| --- | --- |
| `0-SCore` | Framework. Almost everything else depends on it. |
| `0_TFP_Harmony` | Harmony runtime. |
| `0-Help Screens` | The in-game help screen and the main menu Mod Summary panel. |
| `SphereII Learn By Doing` | Use-based progression — the core of the pack. |
| `SphereII A Round World` | Quality of life, the Drop Box. |
| `SphereII Challenges` | ~80 extra challenges. |
| `SphereII Peace of Mind` | Softened imagery and language. |
| `SphereII Disable Sway` | No camera or weapon sway. |
| `SphereII Disable Special Attack` | Chuck and the Rancher, melee only. |
| `Locks` | Lock picking minigame. |
| `ZZTong-Prefabs` | 233 POIs, 128 decorations, 58 RWG tiles. Third party — see Credits. |
| `ZZ-ModInfo` | The pack's name and description. No gameplay of its own. |

---

## Installation

**Game version: 3.1.** Other versions are not supported.

### Easy Anti-Cheat must be off

The pack ships assemblies (`0-SCore`, `0_TFP_Harmony`, `0-Help Screens`, `SphereII Disable Sway`),
and those will not load with EAC enabled. Launch the game with EAC disabled.

### With the 7D2D Mod Launcher (recommended)

The pack ships a launcher configuration at [`ModLauncher.xml`](ModLauncher.xml). Point the launcher
at it, pick **Project Praxis**, and install. The launcher handles the EAC setting, keeps the pack in
its own folder, and updates it in place.

### Manually

1. Download the latest [release](../../releases).
2. Go to `%APPDATA%/7DaysToDie` and locate or create a `Mods` folder.
3. Copy every folder from the release's `Mods` directory into it, so you end up with
   `%APPDATA%/7DaysToDie/Mods/0-SCore`, `.../SphereII Learn By Doing`, and so on.
4. Launch with EAC disabled.

> Do not use `C:\Program Files (x86)\Steam\steamapps\common\7 Days To Die\Mods`. That location is
> deprecated by The Fun Pimps and will stop working.

**Install all of it.** The modlets are not independent — most depend on `0-SCore`, and the pack is
tuned as a whole. Load order matters and is alphabetical by folder name, so do not rename anything.

### On a server

Install on the **server and on every client**. Server-side alone mostly works, but location names
and some trigger wiring will not reach players.

---

## Help, in game

Press **ESC** and choose **Mod Help** for the full detail on progression, decay, and everything each
modlet changes. The main menu also carries a Mod Summary panel describing the pack before you start.

---

## Feedback

Progression rates are the thing most worth hearing about. If a skill runs away from you or refuses
to move, [open an issue](../../issues) and say which skill, roughly how long you spent, and what you
were doing. Logs help but are not required.

---

## Credits and licence

**ZZTong-Prefabs** is the work of **ZZTong** (Bruce Tong) and is licensed **GPLv3**. It is bundled
here with credit as its author asks. It is the only third-party content in this pack.

- Upstream: <https://github.com/zztong/7d2d-prefabs>
- Nexus: <https://www.nexusmods.com/7daystodie/mods/1434/>

Everything else is by **sphereii**, and the individual modlets live in
[SphereII.Mods](https://github.com/SphereII/SphereII.Mods).
