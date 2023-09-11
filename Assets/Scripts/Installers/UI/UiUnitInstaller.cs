namespace DefaultNamespace.Installers
{
	using DefaultNamespace.UI.Unit;
	using UnityEngine;
	using Zenject;

	public class UiUnitInstaller : MonoInstaller
	{
		[SerializeField] EUnit _unitType;
		
		public override void InstallBindings()
		{
			// UiUnitView
			Container
				.BindInterfacesTo<UiUnitView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			// UiUnitController
			Container
				.BindInterfacesTo<UiUnitController>()
				.AsSingle();
			
			// Unit Type
			Container.BindInstance( _unitType );
		}
	}
}