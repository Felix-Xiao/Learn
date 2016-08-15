
using System.Configuration;

namespace SimpleCABApp
{
	class MenuItemElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new MenuItemElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			MenuItemElement e = (MenuItemElement)element;
			
			return e.ID;
		}
	}
}
