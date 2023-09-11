namespace DefaultNamespace.UI
{
	using System;
	using TMPro;
	using UniRx;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IUiView
	{
		IObservable<UniRx.Unit> OnRestart { get; }
		void SetStep(int value);
	}

	public class UiView : MonoBehaviour, IUiView
	{
		[SerializeField] TextMeshProUGUI	_stepValue;
		[SerializeField] Button				_restartButton;

		
#region IUiView

		public IObservable<UniRx.Unit> OnRestart	=> _restartButton.OnClickAsObservable();

		public void SetStep(int value)		=> _stepValue.text = $"Step: {value}";

#endregion
		
	}
}