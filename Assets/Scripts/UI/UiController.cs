namespace DefaultNamespace.UI
{
	using System;
	using UniRx;
	using UnityEngine.SceneManagement;
	using Zenject;

	public class UiController : IInitializable, IDisposable
	{
		[Inject] IUiView		_view;
		[Inject] CombatEvents	_combatEvents;
		
		readonly CompositeDisposable _lifetimeDisposables = new CompositeDisposable();

		
		public void Initialize()
		{
			// On Step Changed
			_combatEvents.Step
				.Subscribe( s => _view.SetStep( s ) )
				.AddTo( _lifetimeDisposables );
			
			// On Restart
			_view.OnRestart
				.Subscribe( _ => RestartCombat() )
				.AddTo( _lifetimeDisposables );
		}

		public void Dispose()	=> _lifetimeDisposables.Dispose();
		
		
		void RestartCombat()	=> SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}
}