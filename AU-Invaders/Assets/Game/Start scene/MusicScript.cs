using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private static MusicScript musisScriptInstance;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (musisScriptInstance == null)
        {
            musisScriptInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
