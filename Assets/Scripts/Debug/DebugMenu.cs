using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Attached to the main GUI object. Triggered if GlobalControls.debug is true and F8 is pressed.
/// </summary>
public class DebugMenu : MonoBehaviour
{
	
	public static void takeScreenshot()
	{
		ScreenCapture.CaptureScreenshot("cyfdef-screenshot" + DateTime.UtcNow.ToFileTimeUtc() + ".png");
	}
		
	public static void PlayerName()
	{
		PlayerCharacter.instance.Name = "CYFDEV";
	}
	
	public static void ToggleFight()
	{
		GlobalControls.isInFight = !GlobalControls.isInFight;
	}

	public static void ToggleShop()
	{
		GlobalControls.isInShop = !GlobalControls.isInShop;
	}
	
	public static void ToggleBypass()
	{
		GlobalControls.errorBypass = !GlobalControls.errorBypass;
	}
}
