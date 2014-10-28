using UnityEngine;
using System.Collections;

public class TomScript : MonoBehaviour
{

	public static TomScript thisInstance;

	//assigned in inspector
	public GameObject[] otherPeoplesStuff;
	public GameObject[] prefabAssets;
	public Texture2D[] artAssets;

	public MenuScript menu;//menu system
	public TomWorld world;//spawn / other world event system
	public bool ingame = false;//don't run anything until the game is setup

	void Awake()
	{
		thisInstance = this;

		artAssets = Resources.LoadAll<Texture2D>("Art/Texture2d_assets/");
		Debug.Log(artAssets.Length);
		Texture2D test = Resources.Load("Art/Texture2D_assets/hpbar") as Texture2D;
		Debug.Log(test.name);

		menu = new MenuScript();
		world = new TomWorld();
	}

	void Start ()
	{
		hideAllStuff();
		world.init();
	}

	void Update ()
	{	
		if(ingame)
		{

		}
	}

	void OnGUI()
	{
		menu.runMenus();
	}

	//hide items placed in the scene by hand
	public void hideAllStuff()
	{
		for(int i = 0;i < otherPeoplesStuff.Length;i++)
		{
			otherPeoplesStuff[i].SetActive(false);
		}
	}

	//unhide things placed in the scene by hand
	public void initAllStuff()
	{
		for(int i = 0;i < otherPeoplesStuff.Length;i++)
		{
			otherPeoplesStuff[i].SetActive(true);
		}

		ingame = true;
	}
}

//notes: in menuscript, scale the hp, mana and transform icons
