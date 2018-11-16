using UnityEngine;

public class ContinueMenu : MonoBehaviour {

	public static Transform Create()
	{
		return Instantiate(Resources.Load<Transform>("Menus/ContinueMenu"));
	}
	
	public void OnRestartClicked()
	{
		Debug.LogWarning("Restart button clicked! Not implemented yet!");
	}
}
