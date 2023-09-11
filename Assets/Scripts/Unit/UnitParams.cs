namespace DefaultNamespace
{
	using System;
	using UniRx;
	using UnityEngine;

	public class UnitParams
	{
		readonly IntReactiveProperty _damage		= new IntReactiveProperty( 15 );
		readonly IntReactiveProperty _health		= new IntReactiveProperty( 100 );
		readonly IntReactiveProperty _armor			= new IntReactiveProperty();
		readonly IntReactiveProperty _vampire		= new IntReactiveProperty();

		readonly IntReactiveProperty _damageAddon	= new IntReactiveProperty();
		readonly IntReactiveProperty _healthAddon	= new IntReactiveProperty();
		readonly IntReactiveProperty _armorAddon	= new IntReactiveProperty();
		readonly IntReactiveProperty _vampireAddon	= new IntReactiveProperty();

		readonly IntReactiveProperty _damageMpl		= new IntReactiveProperty( 1 );

		
		public readonly IObservable<UniRx.Unit> OnAnyParamChanged;

		
		UnitParams()
		{
			OnAnyParamChanged = Observable.Merge(
				_damage			.AsUnitObservable(),
				_health			.AsUnitObservable(), 
				_armor			.AsUnitObservable(), 
				_vampire		.AsUnitObservable(), 
				_damageAddon	.AsUnitObservable(), 
				_healthAddon	.AsUnitObservable(), 
				_armorAddon		.AsUnitObservable(), 
				_vampireAddon	.AsUnitObservable(), 
				_damageMpl		.AsUnitObservable()
			);
		}
		
		
#region Getters

		public int GetDamage()		=> ( _damage.Value + _damageAddon.Value ) * _damageMpl.Value;
		public int GetHealth()		=> _health.Value + _healthAddon.Value;
		public int GetArmor()		=> Mathf.Clamp( _armor.Value + _armorAddon.Value, 0, 100 );
		public int GetVampire()		=> Mathf.Clamp( _vampire.Value + _vampireAddon.Value, 0, 100 );

#endregion

#region Add Value

		public void AddHealth(int value)	=> _health.Value += value;

#endregion

#region Remove Value

		public void RemoveHealth(int value)		=> _health.Value -= value;

#endregion

#region Param Addons

		public void AddArmorAddon(int addon)	=> _armorAddon.Value += addon;

		public void AddVampireAddon(int addon)	=> _vampireAddon.Value += addon;

#endregion

#region Param Multipliers

		public void SetDamageMpl(int value)		=> _damageMpl.Value = value;

#endregion

		public void RemoveAllAddons()
		{
			_damageAddon.Value	= 0;
			_healthAddon.Value	= 0;
			_armorAddon.Value	= 0;
			_vampireAddon.Value	= 0;
		
			_damageMpl.Value	= 1;
		}
	}
}