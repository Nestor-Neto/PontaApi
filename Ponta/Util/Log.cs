
namespace Application.Util
{
    public static class Log
    {
        public static List<string> Logs(string campos)
        {
            List<string> Listcampos = new();
            var logs = campos.Split("|");

            foreach (var campo in logs)
                Listcampos.Add(campo);

            return Listcampos;
        }
        public static string LabelFormat(string labellog, List<string> Listcampos)
        {
            string label = labellog;
            if (Listcampos != null && !string.IsNullOrEmpty(labellog))
            {
                for (int i = 0; i < Listcampos.Count; i++)
                    label = label.Replace(string.Concat("{", i.ToString(), "}"), Listcampos[i]);
            }
            return label;
        }
    }
}
