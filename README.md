# Dialogue and Action System v1.0
#### by Eduardo Vicenzi Kuhn

<a href="https://www.youtube.com/watch?v=ZeKorNSKFrM"><img src="https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/DAS_Print.PNG" width="400"/></a>

### Introduction
`Dialogue and Action System for Unity` (or simply `DAS_Unity`) is an asset for Unity3D Game Engine which provides an easy way for beginners to create complete dialogues in their games with the most frequent features seen on RPGs such as dialogue ramifications, multiple options, associated actions and so on.

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

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/Basic_Structure.png)

Let's take a closer look at the DialogueSystem object, which is a child of the Character(NPC). It contains a DAS_DialogueSystem script attached to it, as you can see below

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/DialogueSystem.png)

The DAS_DialogueSystem is composed of the following variables:
- `Dialogue Controller`: The DialogueController of the scene (only one per scene)
- `Dialogue Box`: The DialogueBox (child of canvas) that will be used by this character
- `Dialogues`: Array of Dialogues
- `Phrase Speed`: The speed of the typing animation of the dialogue
- `Photo`(Optional): If you set a sprite here, all the child dialogues can inherit this sprite (it's the character profile image)
- `Title`(Optional): If you set a title here, all the child dialogues can inherit this title (title of the dialogue box, usualy the character's name)
- `Random`: If checked, dialogues will be shown at random order
- `Action When Start`(Optional): Here you can set a script that inherits DAS_Action to be executed when the dialogue starts
- `Action When Finish`(Optional): Here you can set a script that inherits DAS_Action to be executed when the dialogue ends

Ok, now let's see about the Dialogue objects, which is a child of the DialogueSystem object and contains a DAS_Dialogue script attached to it, as you can see below

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/Dialogue.png)

The DAS_DialogueSystem is composed of the following variables:
- `Last One`: If checked, the dialogue with the character will end when this dialogue is reached
- `Phrases`: An array of strings that will be the dialogue itself (use only one phrase if this dialogue has options)
- `Options`(Optional): An array of Options that will be displayed inside this dialogue
- `Inherit Style`: If checked, this dialogue will use the photo and title of the parent DialogueSystem, if it has those variables assigned
- `Photo` (Hidden/Optional): If InheritStyle is unchecked, you can attribute a specific photo to this dialogue only
- `Title` (Hidden/Optional): If InheritStyle is unchecked, you can attribute a specific title to this dialogue only
- `Play Specific Sound`: If checked, when this dialogue is reached a special sound can be played
- `Audio Source` (Hidden): If PlaySpecificSound is checked, an Audio Source must be provided
- `Specific Sound` (Hidden): If PlaySpecificSound is checked, an Audio Clip must be provided
- `Animate Phrases`: If checked, an animation will simulate the typing of the strings
- `Play Typing Sound`(Hidden): If AnimatePhrases is checked, here you can choose if animation will have sound
- `Jump to Dialogue`: You can check this if you want to redirect to a specific dialogue, instead of the next in the array
- `Target Dialogue`(Hidden): If Jump to Dialogue is checked, a Dialogue must be provided

Example with options:

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/DialogueWithOptions.png)

Since in the last image we saw some DAS_Options variables, let's see how an Option object implements a DAS_Option script:

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/OptionAction.png)

A DAS_Option has the variables:
- `Title`: The text of the option itself
- `End Dialogue`: If checked, the dialogue with the character will close when the option is clicked
- `Open Dialogue`: If checked, a specific dialogue will be called when the option is clicked
- `Dialogue`(Hidden): If Open Dialogue is checked, a Dialogue must be provided
- `Execute Action`: If checked, a specific action will be executed when the option is clicked
- `Action`(Hidden): If Execute Action is checked, a script that inherits from DAS_Action must be provided 

An example of an Option that closes the dialogue:

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/Option.png)

#### Canvas

If you want to use the Prefab DialogueBox without having to make changes, set your Canvas UI Scale Mode to "Scale With Screen Size" and set the Reference Resolution to 1920 x 1280

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/Canvas.PNG)

And don't forget to add the DialogueBox Prefab as a child of the Canvas!

![alt text](https://raw.githubusercontent.com/eduardovk/DAS_Unity/master/git_images/DialogBox.PNG)


#### For a better understanding, please consider running and analysing the examples within the Examples Scene in the project.

## Feel free to contribute to this project!
