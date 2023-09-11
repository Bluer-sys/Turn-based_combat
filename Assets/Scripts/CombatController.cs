namespace DefaultNamespace
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UniRx;
	using UnityEngine.SceneManagement;
	using Zenject;

	public class CombatController : IInitializable, IDisposable
	{
		[Inject] List<IUnit>	_units;
		[Inject] CombatEvents	_combatEvents;

		readonly CompositeDisposable _lifetimeDisposables = new CompositeDisposable();
		
		
		public void Initialize()
		{
			// Increase Step On Right Unit Complete Attack
			_combatEvents.OnAttackClicked
				.Subscribe( OnAttackClicked )
				.AddTo( _lifetimeDisposables );
			
			// Restart Combat On Any Unit Dead
			_units
				.Select( u => u.OnDead )
				.Merge()
				.Subscribe( _ => RestartCombat() )
				.AddTo( _lifetimeDisposables );
		}

		public void Dispose()	=> _lifetimeDisposables.Dispose();

		
		void OnAttackClicked(EUnit unit)
		{
			if( unit == EUnit.RightSide )
				_combatEvents.Step.Value++;
			
			_combatEvents.CurrentUnit.Value = unit == EUnit.RightSide 
				? EUnit.LeftSide 
				: EUnit.RightSide;
		}

		void RestartCombat()	=> SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}
}