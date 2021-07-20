using System;

namespace TestProject1.Helpers
{
    /// <summary>
    /// Class for variables and methods that are using Randomizer 
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// Return random element of any IEnumerate
        /// </summary>
        /// <typeparam name="T">Type of IEnumerate</typeparam>
        /// <returns>Random value of IEnumerate</returns>
        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));

            return (T)v.GetValue(new Random().Next(v.Length));
        }
    }
}
