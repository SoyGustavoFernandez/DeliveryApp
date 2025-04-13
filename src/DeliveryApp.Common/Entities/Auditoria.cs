using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Common.Entities
{
    public class Auditoria
    {
        /// <summary>
        /// Id del usuario que realizó el registro.
        /// </summary>
        [Required]
        public Guid IdUsuReg { get; set; }

        /// <summary>
        /// Fecha y hora en que se realizó el registro.
        /// </summary>
        public DateTime FecReg { get; set; }

        /// <summary>
        /// Id del usuario que modificó el registro por última vez.
        /// </summary>
        public Guid? IdUsuMod { get; set; }

        /// <summary>
        /// Fecha y hora de la última modificación del registro.
        /// </summary>
        public DateTime? FecMod { get; set; }

        /// <summary>
        /// Indica si el permiso está vigente o no. Por defecto es verdadero.
        /// </summary>
        [Required]
        public bool Vigente { get; set; } = true;
    }
}