using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CashRegester
{
	class CashRegester
	{
		private string type;
		Dictionary<string, GoodInfo> dic = new Dictionary<string, GoodInfo>();
        decimal totalMoney = 0;
        decimal totalDiscantMoney = 0;

        public string Type
		{
			get { return type; }
			set { type = value; }
		}
        public void AddAndChangeGoodInfor(GoodInfo good)
        {
            if(dic.ContainsKey(good.BarCode))
            {
                dic.Add(good.BarCode, good);
            }
            else
            {
                dic[good.BarCode] = good;
            }
        }
        public void Calculator(List<string> input)
		{
            foreach(string barCode in input)
            {
				if (barCode.Contains("-"))
				{
					string[] sArray= barCode.Split('-');
					dic[sArray[0]].Amount = dic[sArray[0]].Amount + Convert.ToInt16(sArray[1]);
				}
				else
				{
					dic[barCode].Amount++;
				}
            }
            List<GoodInfo> goods = (from temp in dic select temp.Value).ToList();
            foreach (GoodInfo good in goods)
            {
                 if (good.DiscountType == DiscountType.BuyTwoGetOne)
                {
                    good.SubTotal = good.Price * (good.Amount - good.Amount / 3);
                    good.DisCountMoney = good.Price * (good.Amount / 3);
                }
                else if (good.DiscountType == DiscountType.Percent95)
                {
                    good.SubTotal = good.Price * good.Amount * 0.95m;
                    good.DisCountMoney = good.Price * good.Amount * 0.05m;
                }
                else
                {
                    good.SubTotal = good.Price * good.Amount;
                }
                totalMoney = totalMoney + good.SubTotal;
                totalDiscantMoney = totalDiscantMoney + good.DisCountMoney;
            }
            Print(goods, totalMoney, totalDiscantMoney);
        }

        private void Print(List<GoodInfo> goods, decimal totalMoney, decimal totalDiscantMoney)
        {
            List<GoodInfo> discountGood1 = new List<GoodInfo>();
            Console.WriteLine("***<没钱赚商店>购物清单***");
            foreach(GoodInfo good in goods)
            {
                string disCount2 = null;
                if (good.DiscountType == DiscountType.BuyTwoGetOne && good.DisCountMoney != 0)
                {
                    discountGood1.Add(good);
                }
                else if (good.DiscountType == DiscountType.Percent95 && good.DisCountMoney != 0)
                {
                    disCount2 = string.Format("节省{0:c}(元)", good.DisCountMoney);
                }

                Console.WriteLine("名称：{0}，数量：{1}{2}，单价：{3:c}(元)，小计：{4:c}(元){5}", good.Name, good.Amount, good.Unit, good.Price, good.SubTotal, disCount2);

            }
            Console.WriteLine("----------------------");
            if (discountGood1.Count != 0)
            {
                Console.WriteLine("买二赠一商品：");
            }
            foreach (GoodInfo good in discountGood1)
            {
                if (good.DiscountType == DiscountType.BuyTwoGetOne)
                {
                    Console.WriteLine("名称：{0}，数量：{1}", good.Name, good.DisCountMoney / good.Price);
                }
            }
            Console.WriteLine("----------------------");

            Console.WriteLine("总计：{0:c}(元)", totalMoney.ToString("#0.00"));
            if (totalDiscantMoney != 0)
            {
                Console.WriteLine("节省：{0:c}(元)", totalDiscantMoney.ToString("#0.00"));
            }
            Console.WriteLine("**********************\n\n");
        }
	}
}
