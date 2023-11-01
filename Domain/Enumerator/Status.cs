
using System.ComponentModel;

namespace Domain.Enumerator
{
    public enum Status
    {
        [Description("P")]   
        Pendente,

        [Description("EA")]
        EmAndamento,

        [Description("C")]
        Completo
    }

}
