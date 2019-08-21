﻿/*----------------------------------------------------------------
// Copyright © 2019 Chinairap.All rights reserved. 
// CLR版本：	4.0.30319.42000
// 类 名 称：    StringExtension
// 文 件 名：    StringExtension
// 创建者：      DUWENINK
// 创建日期：	2019/8/21 14:52:13
// 版本	日期					修改人	
// v0.1	2019/8/21 14:52:13	DUWENINK
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DUWENINK.Base.Extensions
{
    /// <summary>
    /// 命名空间： DUWENINK.Base.Extensions
    /// 创建者：   DUWENINK
    /// 创建日期： 2019/8/21 14:52:13
    /// 类名：     StringExtension
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 用于判断是否为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBlank(this string s)
        {
            return s == null || s.Trim().Length == 0;
        }

        /// <summary>
        /// 用于判断是否为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotBlank(this string s)
        {
            return !s.IsBlank();
        }

        /// <summary>
        /// 将字符串转换成MD5加密字符串
        /// </summary>
        /// <param name="orgStr"></param>
        /// <returns></returns>
        public static string ToMd5(this string orgStr)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var encoding = Encoding.UTF8;
                var encryptedBytes = md5.ComputeHash(encoding.GetBytes(orgStr));
                var sb = new StringBuilder(32);
                foreach (var bt in encryptedBytes)
                {
                    sb.Append(bt.ToString("x").PadLeft(2, '0'));
                }
                return sb.ToString();
            }
        }




        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetExt(this string s)
        {
            var ret = string.Empty;
            if (!s.Contains('.')) return ret;
            var temp = s.Split('.');
            ret = temp[temp.Length - 1];

            return ret;
        }
        /// <summary>
        /// 验证QQ格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsQq(this string s)
        {
            return s.IsBlank() || Regex.IsMatch(s, @"^[1-9]\d{4,15}$");
        }

        /// <summary>
        /// 判断是否为有效的Email地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            if (s.IsBlank()) return false;
            const string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 验证是否是合法的电话号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhone(this string s)
        {
            return s.IsBlank() || Regex.IsMatch(s, @"^\+?((\d{2,4}(-)?)|(\(\d{2,4}\)))*(\d{0,16})*$");
        }

        /// <summary>
        /// 验证是否是合法的手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsMobile(this string s)
        {
            return !s.IsBlank() && Regex.IsMatch(s, @"^\+?\d{0,4}?[1][3-8]\d{9}$");
        }

        /// <summary>
        /// 验证是否是合法的邮编
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string s)
        {
            return s.IsBlank() || Regex.IsMatch(s, @"[1-9]\d{5}(?!\d)");
        }

        /// <summary>
        /// 验证是否是合法的传真
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsFax(this string s)
        {
            return s.IsBlank() || Regex.IsMatch(s, @"(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
        }

        /// <summary>
        /// 检查字符串是否为有效的int数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt(this string val)
        {
            return !IsBlank(val) && int.TryParse(val, out _);
        }

        /// <summary>
        /// 字符串转数字，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this string val)
        {
            if (IsBlank(val))
                return 0;
            return int.TryParse(val, out var k) ? k : 0;
        }

        /// <summary>
        /// 检查字符串是否为有效的INT64数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt64(this string val)
        {
            return !IsBlank(val) && long.TryParse(val, out _);
        }

        /// <summary>
        /// 检查字符串是否为有效的Decimal
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string val)
        {
            return !IsBlank(val) && decimal.TryParse(val, out _);
        }

        /// <summary>
        /// 获取中文字符串首字母
        /// </summary>
        /// <param name="source"></param>
        ///  <param name="toUpper">是否大写</param>
        /// <returns></returns>
        public static string GetChineseSpell(this string source, bool toUpper = true)
        {
            var len = source.Length;
            var myStr = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                myStr.Append(GetSpell(source.Substring(i, 1)));
            }
            return toUpper ? myStr.ToString().ToUpper() : myStr.ToString();
        }

        /// <summary>
        /// 比较两个字符串值是否相等
        /// </summary>
        public static bool IsEqual(this string source, string comapreValue, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (source.IsNotBlank() && comapreValue.IsNotBlank())
            {
                return source.Equals(comapreValue, comparison);
            }
            return source.IsBlank() && comapreValue.IsBlank();
        }

        /// <summary>  
        /// 获取单个中文的首字母  
        /// </summary>  
        /// <param name="cnChar"></param>  
        /// <returns></returns>  
        private static string GetSpell(string cnChar)
        {
            var arrCn = Encoding.Default.GetBytes(cnChar);
            if (arrCn.Length <= 1) return cnChar;
            var area = arrCn[0];
            var pos = arrCn[1];
            var code = (area << 8) + pos;
            var areaCode = new[] { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };

            for (var i = 0; i < 26; i++)
            {
                var max = 55290;
                if (i != 25)
                {
                    max = areaCode[i + 1];
                }
                if (areaCode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new[] { (byte)(97 + i) });
                }
            }
            return "*";
        }

        /// <summary>
        /// 用于获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T GetEnum<T>(this string s)
        {

            try
            {
                var enumType = typeof(T);
                var result = (T)Enum.Parse(enumType, s);
                //var result = Convert.ToInt32(enumModel);
                return result;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 字符串中用横线替换逗号 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceDotByHorizontalBar(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && (input.Contains(",") || input.Contains("，")))
            {
                return input.Replace(',', '-').Replace('，', '-');
            }

            return input;
        }
    }
}
