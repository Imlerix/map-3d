using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GameObject MainMenuGroup;
	public GameObject CustomizeGroup;
	public GameObject SettingsGroup;

	private Vector3 MainMenuGroupPos;
	private Vector3 CustomizeGroupPos;
	private Vector3 SettingGroupPos;

	private float ScreenWidth;
	private bool BurgerMenu = false;
	
	void Start()
	{
		//Output the current screen window width in the console
		ScreenWidth = Screen.width;
		print(ScreenWidth);
		
		SettingGroupPos = MainMenuGroup.transform.position;
		SettingGroupPos.x -= ScreenWidth;
		SettingsGroup.transform.position = SettingGroupPos;
		
		CustomizeGroupPos = MainMenuGroup.transform.position;
		CustomizeGroupPos.x += ScreenWidth;
		CustomizeGroup.transform.position = CustomizeGroupPos;
		
		MainMenuGroupPos = MainMenuGroup.transform.position;
	}

	public void ChangeScene (string sceneName){
		Application.LoadLevel(sceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	
	
	
	// Start Menu
	public void CustomizeOnClick ()
	{
		SettingGroupPos.x -= ScreenWidth;
		SettingsGroup.transform.position = SettingGroupPos;
		
		CustomizeGroupPos.x -= ScreenWidth ;
		CustomizeGroup.transform.position = CustomizeGroupPos;
		
		MainMenuGroupPos.x -= ScreenWidth ;
		MainMenuGroup.transform.position = MainMenuGroupPos;
			
	}
	
	public void CustomizeBACKOnClick ()
	{
		SettingGroupPos.x += ScreenWidth;
		SettingsGroup.transform.position = SettingGroupPos;
		
		CustomizeGroupPos.x += ScreenWidth ;
		CustomizeGroup.transform.position = CustomizeGroupPos;
		
		MainMenuGroupPos.x += ScreenWidth ;
		MainMenuGroup.transform.position = MainMenuGroupPos;
			
	}

	public void SettingsOnClick ()
	{
		SettingGroupPos.x += ScreenWidth;
		SettingsGroup.transform.position = SettingGroupPos;
		
		CustomizeGroupPos.x += ScreenWidth ;
		CustomizeGroup.transform.position = CustomizeGroupPos;
		
		MainMenuGroupPos.x += ScreenWidth ;
		MainMenuGroup.transform.position = MainMenuGroupPos;
			
	}
	
	public void SettingsBACKOnClick ()
	{
		SettingGroupPos.x -= ScreenWidth;
		SettingsGroup.transform.position = SettingGroupPos;
		
		CustomizeGroupPos.x -= ScreenWidth ;
		CustomizeGroup.transform.position = CustomizeGroupPos;
		
		MainMenuGroupPos.x -= ScreenWidth ;
		MainMenuGroup.transform.position = MainMenuGroupPos;
			
	}
	
	
	// inGame menu
	public void BurgerOnClick ()
	{
		BurgerMenu = !BurgerMenu;
		if (BurgerMenu == true)
		{
			MainMenuGroup.SetActive(true);
		}
		else
		{
			MainMenuGroup.SetActive(false);
		}
	}
}
