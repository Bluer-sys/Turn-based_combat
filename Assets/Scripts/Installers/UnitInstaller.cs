namespace DefaultNamespace.Installers
{
	using DefaultNamespace.Buffs;
	using UnityEngine;
	using Zenject;

	public class UnitInstaller :MonoInstaller
	{
		[SerializeField] EUnit _unitType;
		
		public override void InstallBindings()
		{
			// UnitParams
			Container
				.Bind<UnitParams>()
				.AsSingle();
			
			// BuffsController
			Container
				.BindInterfacesTo<BuffsController>()
				.AsSingle();
			
			// Unit Type
			Container.BindInstance( _unitType );
		}
	}
}