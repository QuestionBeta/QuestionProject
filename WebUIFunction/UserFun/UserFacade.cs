using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class UserFun
    {
        /// <summary>
        /// 根据用户名和密码查询用户是否存在
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回true or false</returns>
        public bool IsExist(string user_name, string pwd)
        {
            if (string.IsNullOrEmpty(user_name) || string.IsNullOrEmpty(pwd))
            {
                return false;
            }

            return this.baseDataModel.IsExist(user_name, pwd);
        }

        /// <summary>
        /// 根据用户名称获取用户信息
        /// </summary>
        /// <param name="user_name">用户名称</param>
        /// <returns>返回用户信息</returns>
        public User GetSingleByName(string user_name)
        {
            if (string.IsNullOrEmpty(user_name))
            {
                return null;
            }

            try
            {
                return this.baseDataModel.GetSingleByName(user_name);
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// 根据用户邮箱获取用户信息
        /// </summary>
        /// <param name="user_name">用户邮箱</param>
        /// <returns>返回用户信息</returns>
        public User GetSingleByEmail(string user_email)
        {
            if (string.IsNullOrEmpty(user_email))
            {
                return null;
            }

            try
            {
                this.baseDataModel.GetSingleByEmail(user_email);
            }
            catch
            {

            }

            return null;
        }
    }
}
