using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    
    public partial class UserDB 
    {
        /// <summary>
        /// 根据用户名称获取用户信息
        /// </summary>
        /// <param name="user_name">用户名称</param>
        /// <returns>返回用户信息</returns>
        public User GetSingleByName(string user_name)
        {
            var userModel = this.datacontext.User.Where(p => p.user_login_name == user_name).FirstOrDefault();
            return userModel;
        }

        /// <summary>
        /// 根据用户邮箱获取用户信息
        /// </summary>
        /// <param name="user_email">用户邮箱</param>
        /// <returns>用户信息</returns>
        public User GetSingleByEmail(string user_email)
        {
            var userModel = this.datacontext.User.Where(p => p.user_email == user_email).FirstOrDefault();
            return userModel;
        }
    }
}
