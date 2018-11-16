using UnityEngine;

public class MenuController : MonoBehaviour
{
	private static MenuController instance;
	
	public static MenuController Instance
	{
		get { return instance; }
	}

	public void Awake()
	{
		instance = this;
	}

	public void ShowMenu(Transform menu)
	{
		menu.SetParent(transform);
	}
}
