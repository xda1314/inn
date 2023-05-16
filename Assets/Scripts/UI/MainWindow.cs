/****************************************************************************
 * 3/15/2019 5:55:02 PM IVY-PC By IVY CodeAutoGeneration
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace IvyCore
{
	public partial class MainWindow : UI_WindowBase
	{
		public override void Init()
		{
			//please add init code here
		}

		public override void OnShow()
		{
			base.OnShow();
		}

		public override void OnHide()
		{
			base.OnHide();
		}

		public override void OnClose()
		{
			base.OnClose();
		}

		void ShowLog(string content)
		{
			Debug.Log("[ MainWindow:]" + content);
		}
	}
}