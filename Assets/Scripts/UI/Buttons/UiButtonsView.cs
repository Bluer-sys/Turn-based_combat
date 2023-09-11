namespace DefaultNamespace.UI
{
	using System;
	using UniRx;
	using UnityEngine;
	using UnityEngine.UI;
	
	public interface IUiButtonsView
	{
		IObservable<UniRx.Unit> OnAttack	{ get; }
		IObservable<UniRx.Unit> OnBuff		{ get; }
		void SetAttackInteractable(bool isOn);
		void SetBuffInteractable(bool isOn);
	}

	public class UiButtonsView : MonoBehaviour, IUiButtonsView
	{
		[SerializeField] Button		_attackButton;
		[SerializeField] Button		_buffButton;
		

#region IUiButtonsView

		public IObservable<UniRx.Unit> OnAttack		=> _attackButton.OnClickAsObservable();
		public IObservable<UniRx.Unit> OnBuff		=> _buffButton.OnClickAsObservable();

		public void SetAttackInteractable(bool isOn)	=> _attackButton.interactable	= isOn;
		public void SetBuffInteractable(bool isOn)		=> _buffButton.interactable		= isOn;

#endregion

	}
}