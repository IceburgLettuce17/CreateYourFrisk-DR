using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Attached to the main GUI object. Triggered if GlobalControls.debug is true and F8 is pressed.
/// </summary>
public class DebugMenu : MonoBehaviour
{
	private Rect tutRect;

	private GUIStyle tutText;

	public bool hideGUI = true;
	
	private Action activeMenu;
	
	public string tutString;
	
	public GUIStyle debugTextStyle;
	
	private Rect[] buttonLocations;

	private GUIStyle[] buttonStyles;
	
	public int LastIndex;
	
	private void Awake()
	{
		base.gameObject.SetActive(GlobalControls.debug);
	}
	
	private IEnumerator takeScreenshot()
	{
		hideGUI = true;
		yield return 0;
		ScreenCapture.CaptureScreenshot("cyfdef-screenshot" + DateTime.UtcNow.ToFileTimeUtc() + ".png");
		yield return 0;
		hideGUI = false;
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(takeScreenshot());
		}
	}
	
	private void OnGUI()
	{
		GUI.depth = 4;
		if (Input.GetKeyDown(KeyCode.F8) && GlobalControls.debug);
		{
			hideGUI = false;
			return;
		}
		if (!hideGUI)
		{
			activeMenu(); 
			GUI.Label(tutRect, tutString, tutText);
		}
	}

	public void Start()
	{
		activeMenu = MainMenu;
		CalcGUILocations();
		debugTextStyle = new GUIStyle();
		debugTextStyle.font = Resources.Load<Font>("Fonts/PixelOperator/PixelOperator-Bold");
		debugTextStyle.fontSize = (int)(Screen.width / 640f * 34f);
		debugTextStyle.normal.textColor = Color.white;
		debugTextStyle.alignment = TextAnchor.MiddleLeft;
		debugTextStyle.clipping = TextClipping.Clip;
	}
	
	private void CalcGUILocations()
	{
		Rect rect = new Rect(0f, 0f, Screen.width, Screen.height);
		tutRect = new Rect(rect.width / 2f + ((float)Screen.width - rect.width) / 2f, (float)Screen.height * 0.95f, 1f, 1f);
		tutText = new GUIStyle(debugTextStyle);
		tutText.fontSize = 30;
		tutText.alignment = TextAnchor.MiddleCenter;
		tutText.clipping = TextClipping.Overflow;
		tutString = ("Press F9 to open Debug Menu");
		float num = (float)Screen.width * 0.23f;
		float num2 = (float)Screen.height * 0.055f;
		float num3 = (float)Screen.height * 0.01f;
		float num4 = num3;
		float num5 = (float)Screen.height * 0.12f;
		buttonLocations = new Rect[37];
		buttonStyles = new GUIStyle[37];
		for (int i = 0; i < 4; i++)
		{
		for (int j = 0; j < 9; j++)
		{
			buttonLocations[i * 9 + j] = new Rect(num4, num5, num, num2);
			num5 += num2 + num3;
		}
		num5 = (float)Screen.height * 0.12f;
		num4 += num + num3;
		}
		LastIndex = buttonLocations.Length - 1;
		buttonLocations[LastIndex] = new Rect((float)Screen.width / 2f - num / 2f, (float)Screen.height * 0.8f - num2 - num3, num, num2);
	}
	
	private IEnumerator ResetButtonCalcAfterFrame()
	{
		yield return new WaitForEndOfFrame();
		buttonStyles = new GUIStyle[buttonLocations.Length];
	}
	
	private void DebugButton(int index, string text, Action callback, bool isMenu = false)
	{
		if (buttonStyles[index] == null)
		{
		buttonStyles[index] = new GUIStyle();
		buttonStyles[index].alignment = TextAnchor.MiddleCenter;
		buttonStyles[index].normal.textColor = Color.white;
		buttonStyles[index].fontSize = 30;
		}
		if (isMenu)
		{
		GUI.color = Color.blue;
		}
		if (GUI.Button(buttonLocations[index], string.Empty))
		{
		callback();
		}
		if (isMenu)
		{
		GUI.color = Color.blue;
		}
		else
		{
		GUI.color = Color.white;
		}
		if (buttonStyles[index] != null)
		{
		GUI.Label(buttonLocations[index], text, buttonStyles[index]);
		}
		GUI.color = Color.white;
	}
	
	private void SwitchMenu(Action newMenu)
	{
		buttonStyles = new GUIStyle[buttonLocations.Length];
		StartCoroutine(ResetButtonCalcAfterFrame());
		activeMenu = newMenu;
	}
	
	private void MainMenu()
	{
		int num = 0;
		DebugButton(num++, "Change values", delegate
		{
		SwitchMenu(ValueMenu);
		}, isMenu: true);
		DebugButton(LastIndex, "Hide menu", delegate
		{
			hideGUI = true;
		});
	}
	private void ValueMenu()
	{
		int num = 0;
		DebugButton(num++, "Player's Name", PlayerName);
		DebugButton(num++, "Toggle Fight", ToggleFight);
		DebugButton(num++, "Toggle Shop", ToggleShop);
		DebugButton(num++, "Toggle Error Bypass", ToggleBypass);
		DebugButton(LastIndex, "Back", delegate
		{
			SwitchMenu(MainMenu);
		});
	}
		
	private void PlayerName()
	{
		PlayerCharacter.instance.Name = "CYFDEV";
	}
	
	private void ToggleFight()
	{
		GlobalControls.isInFight = !GlobalControls.isInFight;
	}

	private void ToggleShop()
	{
		GlobalControls.isInShop = !GlobalControls.isInShop;
	}
	
	private void ToggleBypass()
	{
		GlobalControls.errorBypass = !GlobalControls.errorBypass;
	}
}
