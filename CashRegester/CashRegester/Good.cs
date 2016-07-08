using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegester
{
	[Serializable]
	class GoodInfo
	{
		#region private properties

		private string name;
		private int amount;
		private decimal price;
		private DiscountType discountType;
		private string barCode;
        private string unit;
        private decimal subTotal;
        private decimal disContMoney;

        #endregion

        #region public properties

        public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public int Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		public DiscountType DiscountType
		{
			get { return discountType; }
			set { discountType = value; }
		}

		public string BarCode
		{
			get { return barCode; }
			set { barCode = value; }
		}
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        public decimal SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }
        public decimal DisCountMoney
        {
            get { return disContMoney; }
            set { disContMoney = value; }
        }
        #endregion

        public GoodInfo(string name, decimal price, DiscountType discountType, string barCode, string unit)
		{
			this.Name = name;
			this.Price = price;
			this.DiscountType = discountType;
			this.BarCode = barCode;
            this.Unit = unit;
		}
	}
}
