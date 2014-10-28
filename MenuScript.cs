using UnityEngine;
using System.Collections;

public class MenuScript
{

	public int menuState = 0;

	public void runMenus()
	{
		switch(menuState)
		{
		case 0:
			MainMenu();
			break;
		case 1:
			InGameUI();
			break;
		}
	}

	void MainMenu()
	{
		//vars
		Rect MenuBackgroundRect = new Rect(Screen.width*0.05f, Screen.height*0.025f, Screen.width*0.9f, Screen.height*0.95f);
		Rect StartButtonRect = new Rect(MenuBackgroundRect.width*0.1f, MenuBackgroundRect.height*0.07f,
		                                Screen.width*0.25f, Screen.height*0.12f);
		Rect QuitButtonRect = new Rect(StartButtonRect.x, StartButtonRect.y + StartButtonRect.height+MenuBackgroundRect.height*0.05f,
		                               StartButtonRect.width, StartButtonRect.height);

		//gui
		GUI.Box(MenuBackgroundRect, "Main Menu text");
		if(GUI.Button(StartButtonRect, "Play"))
		{
			TomScript.thisInstance.initAllStuff();
			menuState = 1;
		}
		GUI.Button(QuitButtonRect, "Quit");

	}

	void InGameUI()
	{
		//vars
		Rect hpmpBG = new Rect(0,0,Screen.width*0.5f, Screen.height*0.15f);
		Rect healthRect = new Rect(hpmpBG.x+hpmpBG.width*0.005f, hpmpBG.y+hpmpBG.height/2*0.15f, 16, 26);
		Rect manaRect = new Rect(healthRect.x, healthRect.y+healthRect.height*1.3f, healthRect.width, healthRect.height);
		Rect trBG = new Rect(hpmpBG.width, 0, Screen.width*0.5f, hpmpBG.height);
		Rect tr = new Rect(trBG.x+trBG.width*0.005f, trBG.y+trBG.height*0.08f, 60, 60);
		//hp: 2/320 by 6/72
		//mp: 2/320 by 40/72

		//gui
		GUI.DrawTexture(hpmpBG, TomScript.thisInstance.artAssets[1]);
		GUI.DrawTexture(trBG, TomScript.thisInstance.artAssets[2]);
		GUI.DrawTexture(healthRect, TomScript.thisInstance.artAssets[0]);
		GUI.DrawTexture(manaRect, TomScript.thisInstance.artAssets[4]);
		GUI.DrawTexture(tr, TomScript.thisInstance.artAssets[3]);

	}

}
