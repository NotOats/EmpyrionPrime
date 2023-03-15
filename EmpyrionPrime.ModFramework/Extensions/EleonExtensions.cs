using Eleon.Modding;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmpyrionPrime.ModFramework.Extensions
{
    public static class EleonExtensions
    {
        public static Id ToId(this int id)
        {
            return new Id(id);
        }

        public static PString ToPString(this string text)
        {
            return new PString(text);
        }
    }
}
