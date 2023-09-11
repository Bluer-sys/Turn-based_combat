namespace DefaultNamespace.Installers
{
	using DefaultNamespace.UI;
	using Zenject;

	public class CombatSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// Unit(s)
			Container
				.BindInterfacesTo<Unit>()
				.FromComponentsInHierarchy()
				.AsSingle();
			
			// CombatEvents
			Container
				.Bind<CombatEvents>()
				.AsSingle();
			
			// CombatController
			Container
				.BindInterfacesTo<CombatController>()
				.AsSingle();
			
			// UiView
			Container
				.BindInterfacesTo<UiView>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			// UiController
			Container
				.BindInterfacesTo<UiController>()
				.AsSingle();
		}
	}
}