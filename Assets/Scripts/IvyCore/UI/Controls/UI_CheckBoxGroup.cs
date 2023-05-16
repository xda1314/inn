using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IvyCore.UI
{
    public class UI_CheckBoxGroup : UIBehaviour
    {

        [SerializeField] private bool m_AllowSwitchOff = false;

        /// <summary>
        /// Is it allowed that no toggle is switched on?
        /// </summary>
        /// <remarks>
        /// If this setting is enabled, pressing the toggle that is currently switched on will switch it off, so that no toggle is switched on. If this setting is disabled, pressing the toggle that is currently switched on will not change its state.
        /// Note that even if allowSwitchOff is false, the Toggle Group will not enforce its constraint right away if no toggles in the group are switched on when the scene is loaded or when the group is instantiated. It will only prevent the user from switching a toggle off.
        /// </remarks>
        public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }

        //public List<UI_CheckBox> m_CheckBoxes = new List<UI_CheckBox>();
        public HashSet<UI_CheckBox> m_CheckBoxes = new HashSet<UI_CheckBox>();

        protected UI_CheckBoxGroup()
        { }

        private bool ValidateToggleIsInGroup(UI_CheckBox cb)
        {
            if (cb == null || !m_CheckBoxes.Contains(cb))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Notify the group that the given checkBox is enabled.
        /// </summary>
        /// <param name="toggle">The checkBox that got triggered on</param>
        public void NotifyToggleOn(UI_CheckBox cb)
        {
            if (!ValidateToggleIsInGroup(cb))
            {
                if(cb!=null)
                {
                    m_CheckBoxes.Add(cb);
                }
            }

            // disable all toggles in the group
            var itr = m_CheckBoxes.GetEnumerator();
            while(itr.MoveNext())
            {
                if(itr.Current!=cb)
                {
                    itr.Current.isChecked = false;
                }
            }
        }

        /// <summary>
        /// Unregister a checkBox from the group.
        /// </summary>
        /// <param name="toggle">The toggle to remove.</param>
        public void UnregisterToggle(UI_CheckBox cb)
        {
            if (m_CheckBoxes.Contains(cb))
                m_CheckBoxes.Remove(cb);
        }

        /// <summary>
        /// Register a checkBox with the checkBox group so it is watched for changes and notified if another checkBox in the group changes.
        /// </summary>
        /// <param name="toggle">The toggle to register with the group.</param>
        public void RegisterToggle(UI_CheckBox cb)
        {
            if (!m_CheckBoxes.Contains(cb))
                m_CheckBoxes.Add(cb);
        }

        public bool IsCheckBoxRegisted(UI_CheckBox cb)
        {
            return m_CheckBoxes.Contains(cb);
        }

        /// <summary>
        /// Are any of the checkBoxes on?
        /// </summary>
        /// <returns>Are and of the toggles on?</returns>
        public bool AnyTogglesOn()
        {
            
            return m_CheckBoxes.FirstOrDefault(x => x.isChecked) != null;
        }

        /// <summary>
        /// Returns the checkBoxes in this group that are active.
        /// </summary>
        /// <returns>The active checkBoxes in the group.</returns>
        /// <remarks>
        /// CheckBoxe belonging to this group but are not active either because their GameObject is inactive or because the CheckBoxes component is disabled, are not returned as part of the list.
        /// </remarks>
        public IEnumerable<UI_CheckBox> ActiveToggles()
        {
            return m_CheckBoxes.Where(x => x.isChecked);
        }

        /// <summary>
        /// Switch all checkBoxes off.
        /// </summary>
        /// <remarks>
        /// This method can be used to switch all checkBoxes off, regardless of whether the allowSwitchOff property is enabled or not.
        /// </remarks>
        public void SetAllTogglesOff()
        {
            bool oldAllowSwitchOff = m_AllowSwitchOff;
            m_AllowSwitchOff = true;

            var itr = m_CheckBoxes.GetEnumerator();
            while (itr.MoveNext())
            {
                itr.Current.isChecked = false;
            }

            m_AllowSwitchOff = oldAllowSwitchOff;
        }
    }
}