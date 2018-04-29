using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavenGenerator.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string s, int n)
             => new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
    }
}
