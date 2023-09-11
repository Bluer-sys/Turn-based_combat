namespace DefaultNamespace
{
	using System.Collections.Generic;
	using System.Linq;
	using DG.Tweening;
	using UniRx;
	using UnityEngine;
	using Zenject;

	public interface IUnit
	{
		EUnit UnitType			{ get; }
		ReactiveCommand OnDead	{ get; }

		void TakeDamage(int damage);
		
		// Addons
		void AddArmorAddon(int addon);
		void AddVampireAddon(int addon);
	}

	public class Unit : MonoBehaviour, IUnit
	{
		[SerializeField] MeshRenderer _mesh;
		
		[Inject] EUnit			_unitType;
		[Inject] UnitParams		_unitParams;
		[Inject] CombatEvents	_combatEvents;
		[Inject] List<IUnit>	_units;

		IUnit	_opponent;
		Tween	_hitTween;

		public void Start()
		{
			SetOpponent();
			
			// On Attack
			_combatEvents.OnAttackClicked
				.Where( u => u == _unitType )
				.Subscribe( _ => Attack() )
				.AddTo( this );
		}

#region IUnit

		public EUnit UnitType			=> _unitType;

		public ReactiveCommand OnDead	{ get; } = new ReactiveCommand();

		public void TakeDamage(int damage)
		{
			damage	= (int) ( damage * (100 - _unitParams.GetArmor() ) / 100f );
			
			_unitParams.RemoveHealth( damage );

			if( damage > 0 )
				HitTween();

			if( _unitParams.GetHealth() <= 0 )
				OnDead.Execute();
		}

		public void AddArmorAddon(int addon)	=> _unitParams.AddArmorAddon( addon );

		public void AddVampireAddon(int addon)	=> _unitParams.AddVampireAddon( addon );

#endregion

		void Attack()
		{
			int damage		= _unitParams.GetDamage();
			int vampire		= _unitParams.GetVampire();
			
			_opponent.TakeDamage( damage );

			int returnedHealth	= (int) (vampire / 100f * damage);
			
			_unitParams.AddHealth( returnedHealth );
		}

		void HitTween()
		{
			_hitTween?.Kill( true );

			_hitTween = _mesh.material
				.DOColor( Color.red, 0.5f )
				.SetLoops( 2, LoopType.Yoyo )
				.OnComplete( () => _hitTween = null );
		}

		void SetOpponent()	=> _opponent = _units.First( u => u.UnitType != _unitType );
	}
}