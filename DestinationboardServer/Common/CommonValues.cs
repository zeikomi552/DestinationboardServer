
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationboardServer.Common
{
	public class CommonValues 
	{
		static CommonValues _Instance = new CommonValues();

		private CommonValues()
		{

		}

		public static CommonValues GetInstance()
		{
			return _Instance;

		}
		public static DateTime ConvertDateTime(string date_text, string format)
		{
			// 作成日時
			DateTime ret = DateTime.MinValue;
			DateTime.TryParseExact(date_text, format, null, System.Globalization.DateTimeStyles.None, out ret);
			return ret;
		}
	}
}
