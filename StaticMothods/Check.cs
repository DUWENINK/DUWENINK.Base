/*----------------------------------------------------------------
// Copyright © 2019 Chinairap.All rights reserved. 
// CLR版本：	4.0.30319.42000
// 类 名 称：    Check
// 文 件 名：    Check
// 创建者：      DUWENINK
// 创建日期：	2019/8/21 14:43:33
// 版本	日期					修改人	
// v0.1	2019/8/21 14:43:33	DUWENINK
//----------------------------------------------------------------*/
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DUWENINK.Base.StaticMothods
{
    /// <summary>
    /// 命名空间： DUWENINK.Base.StaticMothods
    /// 创建者：   DUWENINK
    /// 说明:     本类来自 EntityFrameworkCore https://github.com/DUWENINK/EntityFrameworkCore 用法参照 https://github.com/DUWENINK/EntityFrameworkCore/blob/release/3.0-preview9/src/EFCore/DbContext.cs
    /// 创建日期： 2019/8/21 14:43:33
    /// 类名：     Check
    /// </summary>
    [DebuggerStepThrough]
    internal static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration] T value, [InvokerParameterName] [NotNull] string parameterName)
        {
#pragma warning disable IDE0041 // Use 'is null' check
            if (ReferenceEquals(value, null))
#pragma warning restore IDE0041 // Use 'is null' check
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count != 0) return value;
            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(nameof(parameterName));

        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            Exception e = null;
            if (value is null)
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(nameof(parameterName));
            }

            if (e == null) return value;
            NotEmpty(parameterName, nameof(parameterName));

            throw e;

        }

        public static string NullButNotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value is null || value.Length != 0) return value;
            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(nameof(parameterName));

        }

        public static IReadOnlyList<T> HasNoNulls<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.All(e => e != null)) return value;
            NotEmpty(parameterName, nameof(parameterName));

            throw new ArgumentException(parameterName);

        }


    }
}
