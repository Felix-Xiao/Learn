using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CashRegester
{
	class Program
	{
		static void Main(string[] args)
		{
            Console.WriteLine("可口可乐, 羽毛球 有“买二赠一”优惠条件，苹果有”95折”优惠条件");
            CashRegester cashRegester1 = new CashRegester();
            GoodInfo[] goods = new GoodInfo[] {
                new GoodInfo("可口可乐", 3m, DiscountType.BuyTwoGetOne, "ITEM000001", "瓶"),
                new GoodInfo("羽毛球", 1m, DiscountType.BuyTwoGetOne, "ITEM000005", "个"),
                new GoodInfo("苹果", 5.5m, DiscountType.Percent95, "ITEM000003", "斤")};

            foreach (GoodInfo good in goods)
            {
                cashRegester1.AddAndChangeGoodInfor(good);
            }
            cashRegester1.Calculator(GetInput());

            // Change the information in cash regester
            Console.WriteLine("苹果有”95折”优惠条件");
            CashRegester cashRegester2 = new CashRegester();
            goods = new GoodInfo[] {
                new GoodInfo("可口可乐", 3m, DiscountType.None, "ITEM000001", "瓶"),
                new GoodInfo("羽毛球", 1m, DiscountType.None, "ITEM000005", "个"),
                new GoodInfo("苹果", 5.5m, DiscountType.Percent95, "ITEM000003", "斤")};
            foreach (GoodInfo good in goods)
            {
                cashRegester2.AddAndChangeGoodInfor(good);
            }
            cashRegester2.Calculator(GetInput());

            // Change the information in cash regester
            Console.WriteLine("可口可乐, 羽毛球 有“买二赠一”优惠条件");
            CashRegester cashRegester3 = new CashRegester();
            goods = new GoodInfo[] {
                new GoodInfo("可口可乐", 3m, DiscountType.BuyTwoGetOne, "ITEM000001", "瓶"),
                new GoodInfo("羽毛球", 1m, DiscountType.BuyTwoGetOne, "ITEM000005", "个"),
                new GoodInfo("苹果", 5.5m, DiscountType.None, "ITEM000003", "斤")};
            foreach (GoodInfo good in goods)
            {
                cashRegester3.AddAndChangeGoodInfor(good);
            }
            cashRegester3.Calculator(GetInput());

            // Change the information in cash regester
            Console.WriteLine("没有优惠条件");
            CashRegester cashRegester4 = new CashRegester();

            goods = new GoodInfo[] {
                new GoodInfo("可口可乐", 3m, DiscountType.None, "ITEM000001", "瓶"),
                new GoodInfo("羽毛球", 1m, DiscountType.None, "ITEM000005", "个"),
                new GoodInfo("苹果", 5.5m, DiscountType.None, "ITEM000003", "斤")};
            foreach (GoodInfo good in goods)
            {
                cashRegester4.AddAndChangeGoodInfor(good);
            }
            cashRegester4.Calculator(GetInput());
            Console.ReadLine();
        }
        static private List<string> GetInput()
        {
            // Get the input data
            FileInfo f = new FileInfo(@"E:\Project\CashRegester\CashRegester\input.txt");
            string intput;
            using (StreamReader sr = f.OpenText())
            {
                intput = sr.ReadToEnd();
            }
            List<string> intputList = Regex.Matches(intput, @"'(.*?)'").Cast<Match>().Select(x => x.Groups[1].Value).ToList<string>();

            return intputList;
        }
	}
}
