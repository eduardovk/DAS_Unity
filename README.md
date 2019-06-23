# Dialogue and Action System
#### by Eduardo Vicenzi Kuhn

### Introduction
Dialogue and Action System for Unity (or simply DAS_Unity) is an asset for Unity3D Game Engine which provides an easy way for beginners to create complete dialogues in their games with the most frequent features seen on RPGs such as dialogue ramifications, multiple options, associated actions and so on.

### How to install it
Download this project and copy the contents of "Assets" folder.

Inside your project, create a folder and name it "DAS_Unity" (or whatever you want). Now simply paste the contents inside this folder.

### How to use it
DAS is composed of six classes:
- `DAS_DialogueController` (attached to an empty object on the scene)
- `DAS_DialogueSystem` (attached to the npc, it has an array of type DAS_Dialogue)
- `DAS_Dialogue` (child of dialogue system, it has an array of strings and/or an array of DAS_Options)
- `DAS_Option` (child of dialogue, it has a DAS_Dialogue target or a DAS_Action)
- `DAS_Action` (abstract class which any script can inherit from, so it can be used as an action of an option)
- `DAS_DialogueBox` (prefab child of canvas)

The image below shows the basic structure described above

//TODO
