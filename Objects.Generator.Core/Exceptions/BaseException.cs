namespace Objects.Generator.Core.Exceptions
{
    using System;

    [Serializable]
    public class BaseException : Exception
    {

        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        {
        }

    }

}
