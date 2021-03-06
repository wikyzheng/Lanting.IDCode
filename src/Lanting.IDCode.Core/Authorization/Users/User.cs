﻿using System;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace Lanting.IDCode.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }

        /// <summary>
        /// 可生成码数量
        /// </summary>
        public virtual int AllowCodeCount { get; set; }

        /// <summary>
        /// 可设置页面数量
        /// </summary>
        public virtual int AllowProductCount { get; set; }

        /// <summary>
        /// 已生成的码数
        /// </summary>
        public virtual int TotalCountCount { get; set; }
    }
}
