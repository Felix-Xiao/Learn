
using System.Collections.Generic;
using System.Configuration;

namespace SimpleCABApp
{
	class ShellItemsSection : ConfigurationSection
	{
		[ConfigurationProperty("menuitems", IsDefaultCollection = true)]
		public MenuItemElementCollection MenuItems
		{
			get
			{
				return (MenuItemElementCollection)(this["menuitems"]);
			}
		}
	}
}
