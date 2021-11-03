using System.Collections.Generic;
using System.Collections;
using System;

namespace Proyect
{
    /// <summary>
    /// Interfaz observer para que el emprendedor se entere de un cambio (Patron observer).
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Mertodo pque hace que el usuario se entere del cambio
        /// </summary>
        void Actualizacion();
    }
}