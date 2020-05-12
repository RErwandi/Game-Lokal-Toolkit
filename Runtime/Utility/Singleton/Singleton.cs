namespace GameLokal.Utility
{
	using UnityEngine;

	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private const string MSG_MULTI_INSTANCE = "Multiple instances of a singleton '{0}' exist";
		private const string MSG_CREATE_INSTANCE = "Creating a singleton instance of '{0}'";
		private const string MSG_EXIST_INSTANCE = "Using '{0}' instance";
		private const string DEBUG_TYPE = "Singleton";

		private static T _instance;

		// CONSTRUCTOR: ---------------------------------------------------------------------------

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType(typeof(T));

					if (FindObjectsOfType(typeof(T)).Length > 1)
					{
						Log.Show(MSG_MULTI_INSTANCE, DEBUG_TYPE, _instance.gameObject.name);

						return _instance;
					}

					if (_instance == null)
					{
						CreateInstance();
					} 
					else 
					{
						Log.Show(MSG_EXIST_INSTANCE, DEBUG_TYPE, _instance.gameObject.name);
					}
				}

				return _instance;
			}
		}

		// VIRTUAL METHODS: -----------------------------------------------------------------------

		protected virtual void OnCreate() { }

		protected virtual bool ShouldNotDestroyOnLoad() { return true; }

		// PRIVATE METHODS: -----------------------------------------------------------------------

		private static void CreateInstance()
		{
			var singleton = new GameObject();
			_instance = singleton.AddComponent<T>();
			singleton.name = $"{typeof(T)}(singleton)";

			var component = _instance.GetComponent<Singleton<T>>();
			component.OnCreate();

			if (component.ShouldNotDestroyOnLoad()) DontDestroyOnLoad(singleton);
			Log.Show(MSG_CREATE_INSTANCE, DEBUG_TYPE, typeof(T));
		}

		private void OnDestroy () 
		{
            _instance = null;
		}
	}
}