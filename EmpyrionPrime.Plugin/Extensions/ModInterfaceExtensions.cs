using Eleon.Modding;

namespace EmpyrionPrime.Plugin.Extensions
{
    public static class ModInterfaceExtensions
    {
        /// <summary>
        /// Converts an int into an Empyrion <see cref="Id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Id ToId(this int id)
        {
            return new Id(id);
        }

        /// <summary>
        /// Converts a string into an Empyrion <see cref="PString"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static PString ToPString(this string str)
        {
            return new PString(str);
        }
    }
}
