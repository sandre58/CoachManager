using Newtonsoft.Json;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class ObjectExtensions
    {
        #region Public Methods and Operators

        public static T Clone<T>(this T source)
        {
            string output = JsonConvert.SerializeObject(source);

            var clone = JsonConvert.DeserializeObject<T>(output);

            return clone;
        }

        #endregion Public Methods and Operators
    }
}