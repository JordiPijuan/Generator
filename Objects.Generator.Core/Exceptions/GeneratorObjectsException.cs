namespace Objects.Generator.Core.Exceptions
{
    using System;
    using Objects.Generator.Core.Enumerations;
    
    public class GeneratorObjectsException : BaseException
    {

        public GeneratorObjectsError Code { get; set; }
        public int HttpCode { get; set; }
        public int HttpSubCode { get; set; }

        public GeneratorObjectsException(
            GeneratorObjectsError code, 
            string message
            )
            : this(code, message, null)
        {
        }

        public GeneratorObjectsException(GeneratorObjectsError code)
            : this(code, GeneratorObjectsErrorDescription.GetInstance().ErrorCollection[code], null)
        {
        }

        public GeneratorObjectsException(
            GeneratorObjectsError code, 
            string message, 
            Exception innerException
            )
            : base(message, innerException)
        {
            Code = code;
            SetHttpCodes();
        }

        public GeneratorObjectsException(
            GeneratorObjectsError code, 
            params object[] args
            )
            : this(code, string.Format(GeneratorObjectsErrorDescription.GetInstance().ErrorCollection[code], args), null)
        {
            Code = code;
            SetHttpCodes();
        }

        protected virtual void SetHttpCodes()
        {
            HttpCode = (int)Code;
            HttpSubCode = 0;
        }

        public virtual decimal GetHttpCode()
        {
            if (HttpSubCode == 0)
                return decimal.Parse(string.Format("{0}", HttpCode));

            return decimal.Parse(string.Format("{0}.{1}", HttpCode, HttpSubCode));
        }


    }

}
