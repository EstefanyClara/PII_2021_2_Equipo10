using System;
using System.Runtime.Serialization;

namespace Proyect
{
    /// <summary>
    /// Excepcion para cuando el usurio ingresa un dato vacio cuando se registra
    /// </summary>
    [System.Serializable]
    public class EmptyUserBuilderException : Exception
    {
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public EmptyUserBuilderException () { }

        /// <summary>
        /// Constructor con mensaje.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public EmptyUserBuilderException (string message) : base(message) { }
        
        /// <summary>
        /// Constructor con mensaje y excepcion encadenada.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public EmptyUserBuilderException (string message, System.Exception inner) : base(message, inner) { }
        
        /// <summary>
        /// Constructor con atributo serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected EmptyUserBuilderException (SerializationInfo info,StreamingContext context) : base(info, context) { }
    }
}