using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Screens
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;
        [SerializeField] private List<BaseScreen> ScreenList;
        private List<BaseScreen> _screenStack = new List<BaseScreen>();

        public void Awake()
        {
            if(Instance)
                Destroy(gameObject);
            
            Instance = this;
        }

        public T AddScreen<T>() where T : BaseScreen
        {
            var screen = ScreenList.Find(s => s is T);
            if (screen == null)
            {
                Debug.LogError($"Screen of type {typeof(T)} not found in ScreenList");
                return null;
            }

            // Hide stack, sad i can't use update display here
            if (_screenStack.Count > 0 && screen.HideStack)
            {
                foreach(BaseScreen stack in _screenStack)
                {
                    if(stack.Showing) stack.Hide();
                }
            }

            if(screen.SingleInstance)
            {
                var match = _screenStack.Find(x=>x.GetType() == typeof(T));
                if(match != null)
                {
                    match.transform.SetAsLastSibling();
                    return match as T;
                }
            }
            
            var newScreen = Instantiate(screen.gameObject, transform).GetComponent<T>();
            _screenStack.Insert(0, newScreen);
            return newScreen;
        }

        public void CloseScreen<T>() where T : BaseScreen
        {
            if (_screenStack.Count == 0) return;

            var screen = _screenStack.Find(x => x.GetType() == typeof(T));
            if (screen == null)
            {
                Debug.LogError($"Screen of type {typeof(T)} not found in the stack");
                return;
            }

            _screenStack.Remove(screen);
            UpdateStackDisplay();

            Destroy(screen.gameObject);
        }

        public void CloseScreen(BaseScreen screen)
        {
            if (_screenStack.Count == 0) return;

            if (!_screenStack.Contains(screen))
            {
                Debug.LogError($"Screen {screen} is not in the stack");
                return;
            }

            _screenStack.Remove(screen);
            UpdateStackDisplay();

            Destroy(screen.gameObject);
        }

        private void UpdateStackDisplay()
        {
            bool hideStack = false;
            for(int i = 0; i < _screenStack.Count; i++)
            {
                var screen = _screenStack[i];
                if(hideStack)
                {
                    if(screen.Showing == true) screen.Hide();
                }
                else
                {
                    if(screen.Showing == false) screen.Show();

                    hideStack = screen.HideStack;
                }
            }
        }
    }
}