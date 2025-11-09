using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(2);
	}

	public void QuitMenu2()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}
}
