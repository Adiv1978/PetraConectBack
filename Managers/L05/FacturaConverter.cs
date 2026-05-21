using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L05
{
    public class FacturaConverter
    {
        private readonly L04.FacturaConverter _facturaConverterL04;

        public FacturaConverter()
        {
            _facturaConverterL04 = new L04.FacturaConverter();
        }

        public List<NpgsqlParameter> Converter(GetFacturaRequest request, int minutosCaduca)
        {
            return _facturaConverterL04.Converter(request, minutosCaduca);
        }

        public List<FacturaItemResponse> ConverterGetFactura(DataTable table)
        {
            List<FacturaItemResponse> list = new List<FacturaItemResponse>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(_facturaConverterL04.ConverterGetFactura(row));
            }
            return list;
        }
    }
}
