using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace QuickRMS.Site.WebUI.Common
{
    public class Utilities
    {
        public static TEnum TryConvertToEnum<TEnum>(string value) where TEnum : struct
        {
            var def = default(TEnum);
            var ret = Enum.TryParse<TEnum>(value, out def);
            return def;
        }

        /// <summary>
        /// 距离当前月份几个月差值的月份的某一天
        /// </summary>
        /// <param name="dif">月份差距</param>
        /// <returns>结果月份的第一天</returns>
        public static String GetMonthSomeDay(DateTime now, int dif)
        {
            return now.AddMonths(dif).ToShortDateString().Replace('/', '-');
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="myObject">支持序列化的对象</param>
        public static string Serialize<T>(T myObject)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            StringBuilder sb = new StringBuilder();

            using (TextWriter tr = new StringWriter(sb))
            {
                ser.Serialize(tr, myObject);
                tr.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// xml反串行化
        /// </summary>
        /// <param name="xml">xml字符串</param>
        public static T Deserialize<T>(string xml)
            where T : class
        {

            if (String.IsNullOrEmpty(xml))
            {
                throw new Exception(String.Format("XmlData is Null or Empty!"));
            }
            T result;

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (TextReader tReader = new StringReader(xml))
            {
                result = ser.Deserialize(tReader) as T;
                tReader.Close();
            }

            return result;
        }

       


        /// <summary>
        /// 将字符串类型数据转化为整型，并提供转化失败的默认值
        /// </summary>
        /// <param name="value">将被转化的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>被转化后的整型值</returns>
        public static int ConvertToIntValue(string value, int defValue)
        {
            int result = 0;
            bool isParsed = Int32.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        /// <summary>
        /// 将字符串类型数据转化为布尔类型，并提供转化失败的默认值
        /// </summary>
        /// <param name="value">将被转化的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>被转化后的整型值</returns>
        public static bool ConvertToBoolValue(string value, bool defValue)
        {
            bool result = false;
            bool isParsed = Boolean.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        public static double ConvertToDoubleValue(string value, double defValue)
        {
            double result = 0;
            bool isParsed = double.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        /// <summary>
        /// 将字符串类型数据转化为浮点型，并提供转化失败的默认值
        /// </summary>
        /// <param name="value">将被转化的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>被转化后的浮点值</returns>
        public static float ConvertToFloatValue(string value, float defValue)
        {
            float result = 0;
            bool isParsed = float.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        public static decimal ConvertToDecimalValue(string value, decimal defValue)
        {
            decimal result = 0;
            bool isParsed = decimal.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        /// <summary>
        /// 将字符串类型数据转化为日期，并提供转化失败的默认值
        /// </summary>
        /// <param name="value">将被转化的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>被转化后的日期值</returns>
        public static DateTime ConvertToDateValue(string value, DateTime defValue)
        {
            DateTime result = defValue;
            bool isParsed = DateTime.TryParse(value, out result);

            if (isParsed)
                return result;

            return defValue;
        }

        /// <summary>
        /// 将字符串类型数据转化为日期，并提供转化失败的默认值
        /// </summary>
        /// <param name="value">将被转化的字符串</param>
        /// <param name="defValue">默认值</param>
        /// <returns>被转化后的日期值</returns>
        public static DateTime? TryConvertToDateValue(string value)
        {

            DateTime result = DateTime.Now;
            bool isParsed = DateTime.TryParse(value, out result);

            if (isParsed)
                return result;

            return null;
        }

        public static decimal? TryConvertToDecimalValue(string value)
        {
            decimal def = 0;
            bool isParsed = decimal.TryParse(value,out def);

            if (isParsed)
                return def;

            return null;
        }

        /// <summary>
        /// 生成随机文件名
        /// </summary>
        /// <returns></returns>
        public static String GenerateRandomFileName()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            var numberlist = GetRandomNumList();
            for (int i = 0; i < 20; i++)
            {
                var index = random.Next(0, 61);
                sb.Append((char)numberlist[index]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取随机数字
        /// </summary>
        /// <returns></returns>
        private static int[] GetRandomNumList()
        {
            int[] numlist = new int[62];

            for (int i = 0; i < 10; i++)
            {
                numlist[i] = i + 48;
            }

            for (int i = 10; i < 36; i++)
            {
                numlist[i] = i + 55;
            }

            for (int i = 36; i < 62; i++)
            {
                numlist[i] = i + 61;
            }

            return numlist;
        }

        /// <summary>
        /// 计算年差
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string GetYearDuration(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                return "0年";
            }
            else if (from < to)
            {
                TimeSpan diff = to - from;
                int year = (int)diff.TotalDays / 365;

                return year + "年";
            }

            return string.Empty;
        }

        public static string ConvertToDataStringValue(DateTime? data)
        {
            if (data == null) return "";
            return data.Value.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// 将日期转化到年月日时分的格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String ToDateTimeEndWithMinute(String date)
        {
            DateTime result = Utilities.ConvertToDateValue(date, DateTime.MinValue);
            if (result != DateTime.MinValue)
            {
                return String.Format("{0:yyyy年M月d日HH时mm分}", result);
            }

            return String.Empty;
        }

        public static string GetConfigSettingValue(string key)
        {
            string authenticationKey = "";
            if (String.IsNullOrEmpty(key))
                authenticationKey = ConfigurationManager.AppSettings[key].Trim();

            return authenticationKey;


        }


        public static string encrypt_string_using_MD5(string s)
        {

            byte[] byte_array = System.Text.Encoding.Default.GetBytes(s);

            System.Security.Cryptography.HashAlgorithm alg =
                System.Security.Cryptography.HashAlgorithm.Create("MD5");

            byte[] byte_array2 = alg.ComputeHash(byte_array);

            var sb
                = new System.Text.StringBuilder(byte_array2.Length);

            foreach (byte b in byte_array2)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString();
        }

        public static decimal? GetDecimalValue(byte[] datas, int pos1, int pos2)
        {
            byte[] values = new byte[2];
            values[0] = datas[pos1];
            values[1] = datas[pos2];
            Int16 value = BitConverter.ToInt16(values, 0);
            decimal result = (decimal)value / (decimal)100;
            if (result == 200)
                return null;
            return result;
        }

        public static DateTime GetDateTimeValue(byte[] datas, int startPos)
        {
            if (datas[startPos + 1] == 0)
                return DateTime.Now;
            int year = datas[startPos] + 2000;
            int month = datas[startPos + 1];
            int day = datas[startPos + 2];
            int week = datas[startPos + 3];
            int hour = datas[startPos + 4];
            int minutes = datas[startPos + 5];
            int second = datas[startPos + 6];
            return new DateTime(year, month, day, hour, minutes, second);
        }


        public static int GetIntValue(byte[] datas, int pos1, int pos2)
        {
            byte[] values = new byte[2];
            values[0] = datas[pos1];
            values[1] = datas[pos2];
            return BitConverter.ToUInt16(values, 0);
        }

    }
}