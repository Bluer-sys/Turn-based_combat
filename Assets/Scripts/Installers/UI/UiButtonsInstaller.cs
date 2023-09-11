namespace DefaultNamespace.Installers
{
	using DefaultNamespace.UI;
	using UnityEngine;
	using Zenject;

	public class UiButtonsInstaller : MonoInstaller
	{
		[SerializeField] EUnit _unitType;
		
		
		public override void InstallBindings()
		{
			// UiButtonsView
			Container
				.BindInterfacesTo<UiButtonsView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			// UiButtonsController
			Container
				.BindInterfacesTo<UiButtonsController>()
				.AsSingle();
			
			// Unit Type
			Container.BindInstance( _unitType );
		}
	}
}