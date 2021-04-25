using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServerTests.Common.DBManager.SQLite
{
    public class CommonTestUtilities
    {
        /// <summary>
        /// 二つのオブジェクトの一致不一致を判定する
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="test">テスト用オブジェクト</param>
        /// <param name="check">チェック用オブジェクト</param>
        /// <param name="equal">ture:2つのオブジェクトがイコールの時にOK  false:2つのオブジェクトが不一致の時にOK</param>
        public static void PropCheck<T>(T test, T check, bool equal)
        {
            Type t = typeof(T);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propInfos)
            {
                var property = typeof(T).GetProperty(prop.Name);
                var val = property?.GetValue(test);     // プロパティ名から値の取り出し
                var val2 = property?.GetValue(check);   // プロパティ名から値の取り出し

                // コピー後同じ値が入ってたら失敗
                if ((equal && val.ToString().CompareTo(val2 == null ? string.Empty : val2.ToString()) != 0)
                    || (!equal && val.ToString().CompareTo(val2 == null ? string.Empty : val2.ToString()) == 0))
                {
                    Assert.Fail(string.Format("property name:{0} test{1}, check{2}", prop.Name, val.ToString(), val2.ToString()));
                }
            }
        }

        /// <summary>
        /// テーブルデータのプロパティ値とgRPCのプロパティ値をチェックする
        /// </summary>
        /// <typeparam name="T">テーブル側のプロパティ値型</typeparam>
        /// <typeparam name="T2">gRPC側プロパティ値型</typeparam>
        /// <param name="test">テスト用オブジェクト(テーブル側)</param>
        /// <param name="check">チェック用オブジェクト(gRPC側)</param>
        /// <param name="equal">ture:2つのオブジェクトがイコールの時にOK  false:2つのオブジェクトが不一致の時にOK</param>
        public static void gRPCPropCheck<T,T2>(T test, T2 check, bool equal)
        {
            Type t = typeof(T);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propInfos)
            {
                var property = typeof(T).GetProperty(prop.Name);
                var val = property?.GetValue(test);     // プロパティ名から値の取り出し

                var property2 = typeof(T2).GetProperty(prop.Name);
                var val2 = property2?.GetValue(check);   // gRPC側のプロパティ名から値の取り出し


                if (prop.PropertyType.Equals(typeof(DateTime)))
                {
                    // コピー後同じ値が入ってたら失敗
                    if ((equal && ((DateTime)val).ToString("yyyy/MM/dd").CompareTo(val2 == null ? string.Empty : val2.ToString()) != 0)
                        || (!equal && ((DateTime)val).ToString("yyyy/MM/dd").CompareTo(val2 == null ? string.Empty : val2.ToString()) == 0))
                    {
                        Assert.Fail(string.Format("property name:{0} test{1}, check{2}", prop.Name, val.ToString(), val2.ToString()));
                    }
                }
                else
                {
                    // コピー後同じ値が入ってたら失敗
                    if ((equal && val.ToString().CompareTo(val2 == null ? string.Empty : val2.ToString()) != 0)
                        || (!equal && val.ToString().CompareTo(val2 == null ? string.Empty : val2.ToString()) == 0))
                    {
                        Assert.Fail(string.Format("property name:{0} test{1}, check{2}", prop.Name, val.ToString(), val2.ToString()));
                    }
                }
            }
        }

    }
}
