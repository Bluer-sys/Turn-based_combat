namespace DefaultNamespace
{
	using UniRx;

	public class CombatEvents
	{
		public readonly ReactiveProperty<EUnit> CurrentUnit			= new ReactiveProperty<EUnit>( EUnit.LeftSide );
		public readonly ReactiveProperty<int>	Step				= new ReactiveProperty<int>( 1 );

		public readonly ReactiveCommand<EUnit>	OnAttackClicked		= new ReactiveCommand<EUnit>();
		public readonly ReactiveCommand<EUnit>	OnBuffClicked		= new ReactiveCommand<EUnit>();
	}
}