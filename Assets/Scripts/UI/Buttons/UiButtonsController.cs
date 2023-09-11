namespace DefaultNamespace.UI
{
	using System;
	using UniRx;
	using Zenject;

	public class UiButtonsController : IInitializable, IDisposable
	{
		[Inject] EUnit			_unitType;
		[Inject] IUiButtonsView _view;
		[Inject] CombatEvents	_combatEvents;

		readonly CompositeDisposable _lifetimeDisposables = new CompositeDisposable();
		
		
		public void Initialize()
		{
			// On Attack
			_view.OnAttack
				.Subscribe( _ =>
				{
					_combatEvents.OnAttackClicked.Execute( _unitType );
					_view.SetAttackInteractable( false );
					_view.SetBuffInteractable( false );
				} )
				.AddTo( _lifetimeDisposables );
			
			// On Buff
			_view.OnBuff
				.Subscribe( u =>
				{
					_combatEvents.OnBuffClicked.Execute( _unitType );
					_view.SetBuffInteractable( false );
				} )
				.AddTo( _lifetimeDisposables );
			
			// Current Unit Changed
			_combatEvents.CurrentUnit
				.Subscribe( u =>
				{
					_view.SetAttackInteractable( u == _unitType );
					_view.SetBuffInteractable( u == _unitType );
				} )
				.AddTo( _lifetimeDisposables );
		}

		public void Dispose() => _lifetimeDisposables.Dispose();
	}
}