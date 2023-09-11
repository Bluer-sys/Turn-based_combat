namespace DefaultNamespace.UI.Unit
{
	using System.Collections.Generic;
	using DefaultNamespace.Buffs;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IUiUnitView
	{
		void SetHealth(int value);
		void SetArmor(int value);
		void SetVampire(int value);
		void SetDamage(int value);
		void SetBuffs(List<BuffData> buffs);
	}

	public class UiUnitView : MonoBehaviour, IUiUnitView
	{
		[SerializeField] Image _hpBar;
		[SerializeField] Image _armorBar;
		[SerializeField] Image _vampireBar;
		
		[SerializeField] TextMeshProUGUI _hpValue;
		[SerializeField] TextMeshProUGUI _armorValue;
		[SerializeField] TextMeshProUGUI _vampireValue;
		[SerializeField] TextMeshProUGUI _damageValue;

		[SerializeField] List<TextMeshProUGUI> _buffsValues;


#region IUiUnitView

		public void SetHealth(int value)
		{
			_hpBar.fillAmount	= (float) value / 100;
			_hpValue.text		= $"{value}";
		}
		
		public void SetArmor(int value)
		{
			_armorBar.fillAmount	= (float) value / 100;
			_armorValue.text		= $"{value}";
		}
		
		public void SetVampire(int value)
		{
			_vampireBar.fillAmount	= (float) value / 100;
			_vampireValue.text		= $"{value}";
		}
		
		public void SetDamage(int value)
		{
			_damageValue.text		= $"Damage: {value}";
		}

		public void SetBuffs(List<BuffData> buffs)
		{
			_buffsValues.ForEach( b => b.gameObject.SetActive( false ) );
			
			for (int i = 0; i < buffs.Count; i++)
			{
				BuffData buff = buffs[i];

				_buffsValues[i].text = $"{buff.Buff} {buff.StepsRemained}";
				_buffsValues[i].gameObject.SetActive( true );
			}
		}

#endregion
	}
}