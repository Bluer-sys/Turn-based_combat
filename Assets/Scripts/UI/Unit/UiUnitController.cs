namespace DefaultNamespace.UI.Unit
{
	using System;
	using DefaultNamespace.Buffs;
	using UniRx;
	using Zenject;

	public class UiUnitController : IInitializable, IDisposable
	{
		[Inject] IUiUnitView		_view;
		[Inject] UnitParams			_unitParams;
		[Inject] IBuffsController	_buffsController;
		
		readonly CompositeDisposable _lifetimeDisposables = new CompositeDisposable();

		
		public void Initialize()
		{
			// On Any Unit Param Changed
			_unitParams.OnAnyParamChanged
				.Subscribe( _ => Refresh() )
				.AddTo( _lifetimeDisposables );
			
			// Set Buffs
			_buffsController.OnBuffsApplied
				.Subscribe( _view.SetBuffs )
				.AddTo( _lifetimeDisposables );
		}

		public void Dispose() => _lifetimeDisposables.Dispose();

		
		void Refresh()
		{
			_view.SetDamage	( _unitParams.GetDamage	() );
			_view.SetHealth	( _unitParams.GetHealth	() );
			_view.SetArmor	( _unitParams.GetArmor	() );
			_view.SetVampire( _unitParams.GetVampire() );
		}
	}
}