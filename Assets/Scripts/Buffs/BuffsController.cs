namespace DefaultNamespace.Buffs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UniRx;
	using Zenject;

	public interface IBuffsController
	{
		ReactiveCommand<List<BuffData>> OnBuffsApplied { get; }
	}

	public class BuffsController : IInitializable, IDisposable, IBuffsController
	{
		[Inject] EUnit			_unitType;
		[Inject] CombatEvents	_combatEvents;
		[Inject] UnitParams		_unitParams;
		[Inject] List<IUnit>	_units;
		
		readonly CompositeDisposable	_lifetimeDisposables	= new CompositeDisposable();
		readonly List<BuffData>			_buffs					= new List<BuffData>();
		
		IUnit _opponent;

		
		public void Initialize()
		{
			SetOpponent();

			// On Buff
			_combatEvents.OnBuffClicked
				.Where( u => u == _unitType )
				.Subscribe( _ => ApplyRandomBuff() )
				.AddTo( _lifetimeDisposables );

			// On Step Completed
			_combatEvents.Step
				.Subscribe( _ => DecreaseBuffValue() )
				.AddTo( _lifetimeDisposables );
		}

		public void Dispose()		=> _lifetimeDisposables.Dispose();


#region IBuffsController

		public ReactiveCommand<List<BuffData>> OnBuffsApplied { get; } = new ReactiveCommand<List<BuffData>>();
		
#endregion


		void ApplyRandomBuff()
		{
			if( _buffs.Count > 1 )
				return;
			
			EBuff buff			= GetRandomBuff();
			int stepCount		= UnityEngine.Random.Range( 1, 4 );
			
			BuffData data		= new BuffData
			{
				Buff			= buff,
				StepsRemained	= stepCount
			};
			
			_buffs.Add( data );

			ApplyBuff( data.Buff );
			OnBuffsApplied.Execute( _buffs );
		}

		void ApplyBuff(EBuff buff)
		{
			switch (buff)
			{
				case EBuff.DoubleDamage:
					_unitParams.SetDamageMpl( 2 );
					break;
				case EBuff.ArmorSelf:
					_unitParams.AddArmorAddon( 50 );
					break;
				case EBuff.ArmorDestruction:
					_opponent.AddArmorAddon( -10 );
					break;
				case EBuff.VampirismSelf:
					_unitParams.AddArmorAddon( -25 );
					_unitParams.AddVampireAddon( 50 );
					break;
				case EBuff.VampirismDecrease:
					_opponent.AddVampireAddon( -25 );
					break;
			}
		}

		void DecreaseBuffValue()
		{
			_unitParams.RemoveAllAddons();

			for (int i = 0; i < _buffs.Count; i++)
			{
				BuffData newData = _buffs[i];
				newData.StepsRemained--;
				
				_buffs[i] = newData;
			}

			_buffs.RemoveAll( b => b.StepsRemained <= 0 );
			
			_buffs.ForEach( b => ApplyBuff( b.Buff ) );
			OnBuffsApplied.Execute( _buffs );
		}
		
		EBuff GetRandomBuff()
		{
			int buffsCount				= Enum.GetValues( typeof(EBuff) ).Length;
			EBuff buff					= (EBuff) UnityEngine.Random.Range( 1, buffsCount );
			List<EBuff> availableBuffs	= _buffs.Select( b => b.Buff ).ToList();

			while ( availableBuffs.Contains( buff ) || buff == EBuff.None )
			{
				buff = (EBuff) ( ( (int) buff + 1 ) % buffsCount );
			}
			
			return buff;
		}

		void SetOpponent()	=> _opponent = _units.First( u => u.UnitType != _unitType );
	}
}