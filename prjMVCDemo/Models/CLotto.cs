using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMauiDemo.Model
{
    public class CLotto
    {
        public string lotto()
        {
            //    int[] n = new int[6];
            //    Random rm = new Random();
            //    List<int> list = new List<int>();
            //    n[0] = rm.Next(1, 50);
            //    list.Add(n[0]);
            //    for (int i = 1; i < n.Length; i++)
            //    {
            //        n[i] = rm.Next(1, 50);
            //        for (int j = 0; j < i; j++)
            //        {
            //            if (n[i] == n[j])
            //            {
            //                n[i] = rm.Next(1, 50);
            //                j = 0;
            //            }
            //        }
            //        list.Add(n[i]);
            //    }
            //    list.Sort();
            //    lbl.Text = String.Join(" ", list);


            Random rm = new Random();
            List<int> list = new List<int>();
            List<int> result = new List<int>();
            for (int i = 1; i < 50; i++)
            {
                list.Add(i);
            }
            int num = rm.Next(0, list.Count);
            for (int i = 1; i <= 6; i++)
            {
                result.Add(list[num]);
                list.RemoveAt(num);
                num = rm.Next(0, list.Count);
            }
            result.Sort();
            string s = String.Join(" ", result);
            return s;
        }
    }
}
