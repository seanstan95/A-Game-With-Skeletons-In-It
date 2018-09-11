using UnityEngine;

/*
    * EventManager exists purely to simulate MonoBehaviour's Update functions for UIManager.
    * UIManager is a static class (for simplicity of access from other scripts), and as such can not inherit from MonoBehaviour.
*/

public class EventManager : MonoBehaviour {
    
	private void Start () {
		//UIManager.Initialize ();
	}

	private void Update () {
		//UIManager.Update ();
	}
}
