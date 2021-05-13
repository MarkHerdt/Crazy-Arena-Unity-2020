using System;
using UnityEngine;

public static class EventController
{
    //public static EventController eventController;

    //private void Awake()
    //{
    //    eventController = this;
    //}

    public static event Action OnPlayerJoined = delegate { };
    public static void PlayerJoined()
    {
        OnPlayerJoined();
    }

    /// <summary>
    /// Is called when the CharacterMenu is activated
    /// </summary>
    public static event Action OnCharacterMenuActive = delegate { };
    /// <summary>
    /// Is called when the CharacterMenu is activated
    /// </summary>
    public static void CharacterMenuActivate()
    {
        OnCharacterMenuActive();
    }

    /// <summary>
    /// Is called when the Player changes the Character Mesh
    /// </summary>
    public static event Action OnCharacterChange = delegate { };
    /// <summary>
    /// Is called when the Player changes the Character Mesh
    /// </summary>
    public static void CharacterChange()
    {
        OnCharacterChange();
    }

    /// <summary>
    /// Is called when the Player presses the left D-Pad Button
    /// </summary>
    public static event Action<int> OnCharacterSelectionButtonLeftPressed = delegate { };
    /// <summary>
    /// Is called when the Player presses the left D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    public static void CharacterSelectionButtonLeftPressed(int index)
    {
        OnCharacterSelectionButtonLeftPressed(index);
    }

    /// <summary>
    /// Is called when the Player releases the Left D-Pad Button
    /// </summary>
    public static event Action<int> OnCharacterSelectionButtonLeftReleased = delegate { };
    /// <summary>
    /// Is called when the Player releases the Left D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    public static void CharacterSelectionButtonLeftReleased(int index)
    {
        OnCharacterSelectionButtonLeftReleased(index);
    }

    /// <summary>
    /// Is called when the Player presses the right D-Pad Button
    /// </summary>
    public static event Action<int> OnCharacterSelectionButtonRightPressed = delegate { };
    /// <summary>
    /// Is called when the Player presses the right D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    public static void CharacterSelectionButtonRightPressed(int index)
    {
        OnCharacterSelectionButtonRightPressed(index);
    }

    /// <summary>
    /// Is called when the Player realeses the right D-Pad Button
    /// </summary>
    public static event Action<int> OnCharacterSelectionButtonRightReleased = delegate { };
    /// <summary>
    /// Is called when the Player realeses the right D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    public static void CharacterSelectionButtonRightReleased(int index)
    {
        OnCharacterSelectionButtonRightReleased(index);
    }

    /// <summary>
    /// Is fired when the Player selects their Character choice
    /// </summary>
    public static event Action<int, int> OnSelectCharacter = delegate { };
    /// <summary>
    /// Is called when the Player selects their Character choice
    /// </summary>
    /// <param name="index"></param>
    public static void SelectCharacter(int index, int character)
    {
        OnSelectCharacter(index, character);
    }

    /// <summary>
    /// Is fired when the Player deselects their Character choice
    /// </summary>
    public static event Action<int, int> OnDeselectCharacter = delegate { };
    /// <summary>
    /// Is called when the Player deselects their Character choice
    /// </summary>
    /// <param name="index"></param>
    public static void DeSelectCharacter(int index, int character)
    {
        OnDeselectCharacter(index, character);
    }

    public static event Action<int, int> OnDPadUp = delegate { };
    public static void DPadUp(int index, int direction)
    {
        OnDPadUp(index, direction);
    }

    public static event Action<int, int> OnDPadDown = delegate { };
    public static void DPadDown(int index, int direction)
    {
        OnDPadDown(index, direction);
    }

    /// <summary>
    /// Is fired when the Game starts
    /// </summary>
    public static event Action OnGameStart = delegate { };
    /// <summary>
    /// Is called when the Game starts
    /// </summary>
    public static void GameStart()
    {
        OnGameStart();
    }

    /// <summary>
    /// Is called when an Object is broken by an Alien
    /// </summary>
    public static event Action<int> OnObjectBreak = delegate { };
    /// <summary>
    /// Is called when an Object is broken by an Alien
    /// </summary>
    /// <param name="amount"></param>
    public static void ObjectBreak(int amount)
    {
        OnObjectBreak(amount);
    }

    /// <summary>
    /// Is called when an Object is repaired by an Astronaut
    /// </summary>
    public static event Action<int> OnObjectUnBreak = delegate { };
    /// <summary>
    /// Is called when an Object is repaired by an Astronaut
    /// </summary>
    /// <param name="amount"></param>
    public static void ObjectUnBreak(int amount)
    {
        OnObjectUnBreak(amount);
    }

    /// <summary>
    /// Is called when one of the Teams wins the game
    /// </summary>
    public static event Action OnEndMatch = delegate { };
    /// <summary>
    /// Is called when one of the Teams wins the game
    /// </summary>
    public static void EndMatch()
    {
        OnEndMatch();
    }
}