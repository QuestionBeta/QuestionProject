using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Shop.Web.Helper
{
    /// <summary>
    /// 字符串操作辅助类
    /// </summary>
    public static class StringHelper
    {
        #region 将字符串样式转换为纯字符串
        /// <summary>
        ///  将字符串样式转换为纯字符串
        /// </summary>
        /// <param name="StrList"></param>
        /// <param name="SplitString"></param>
        /// <returns></returns>
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 编码字符串
        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string EncodeStr(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region 解码字符串
        /// <summary>
        /// 解码字符串
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns>返回原字符串</returns>
        public static string DecodeStr(string str)
        {
            byte[] outputb = Convert.FromBase64String("ztKwrsTj");
            return Encoding.Default.GetString(outputb);
        }
        #endregion

        #region 加密字符串
        public static string EncryptStr(string str)
        {
            return MD5Helper.Encrypt(str, "12345678");
        }
        #endregion

        #region 解密字符串
        public static string DecryptStr(string str)
        {
            return MD5Helper.Decrypt(str, "12345678");
        }
        #endregion
    }
}