using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameLokal.Utility
{
    [AddComponentMenu("Game Lokal/State Machine")]
    public class StateMachine : MonoBehaviour
    {
        #region CONSTANT
        
        private const string DEBUG_TYPE = "StateMachine";
        
        #endregion
        
        #region PRIVATE FIELD
        
        [ValueDropdown("AvailableStates"), HorizontalGroup("TestState"), ShowInInspector, DisableInEditorMode, PropertyOrder(10)]
        private string _testState = "";
        
        #endregion
        
        #region PUBLIC FIELD
        
        [ValueDropdown("AvailableStates"), Required]
        public string defaultState;
        public State[] states = new State[0];
        
        #endregion

        #region PROPERTIES

        public State CurrentState { get; private set; }

        #endregion
        
        #region PRIVATE METHODS

        [Button(ButtonSizes.Large), HideInPlayMode]
        private void AddNewState()
        {
            var newState = new GameObject($"State {states.Length}");
            var state = newState.AddComponent<State>();
            newState.transform.parent = transform;
            newState.transform.localPosition = Vector3.zero;
            newState.transform.localRotation = Quaternion.identity;
            newState.transform.localScale = Vector3.one;
            states = states.Concat(new[] { state }).ToArray();

            if (CurrentState == null)
                CurrentState = state;
        }

        [Button(ButtonSizes.Small), HorizontalGroup("TestState"), DisableInEditorMode, PropertyOrder(11)]
        private void ChangeState()
        {
            SetState(_testState);
        }

        private void Start()
        {
            foreach (var state in states)
            {
                if(state.gameObject.activeSelf)
                    state.gameObject.SetActive(false);
            }
            
            SetState(defaultState);
        }

        private List<string> AvailableStates
        {
            get
            {
                return states.Select(state => state.StateName).ToList();
            }
        }
        
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Change state. this will call OnStateExit on the current state before calling OnStateEnter on the target state.
        /// </summary>
        /// <param name="stateName">Name of the state</param>
        public void SetState(string stateName)
        {
            State newState = states.FirstOrDefault(o => o.StateName == stateName);

            if(newState != null)
            {
                if (CurrentState != null)
                {
                    // Call Exit Actions
                    CurrentState.onStateExit?.Invoke();
                    // Then finally disable old state
                    CurrentState.gameObject.SetActive(false);
                }

                // Switch Active new state
                newState.gameObject.SetActive(true);

                // Then Set new current state
                CurrentState = newState;

                // Finally, call State enter
                CurrentState.onStateEnter?.Invoke();
            }
            else
                Log.Show($"{gameObject.name} : Trying to set unknown state {stateName}", DEBUG_TYPE);
        }

        #endregion

    }
}

