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

        public List<NpgsqlParameter> Converter(GetFacturaByStatusRequest request, int minutosCaduca)
        {
            return _facturaConverterL04.Converter(request, minutosCaduca);
        }

        public List<NpgsqlParameter> Converter(SetFacturaDbRequest request, int minutosCaduca)
        {
            return _facturaConverterL04.Converter(request, minutosCaduca);
        }

        public List<NpgsqlParameter> Converter(SetFacturaStatusRequest request, int minutosCaduca)
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

        public List<SetFacturaDbResponse> ConverterSetFactura(DataTable table)
        {
            List<SetFacturaDbResponse> list = new List<SetFacturaDbResponse>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(_facturaConverterL04.ConverterSetFactura(row));
            }
            return list;
        }

        public List<SetFacturaStatusResponse> ConverterSetFacturaStatus(DataTable table)
        {
            List<SetFacturaStatusResponse> list = new List<SetFacturaStatusResponse>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(_facturaConverterL04.ConverterSetFacturaStatus(row));
            }
            return list;
        }

        public List<FacturaItemResponse> ConverterGetFacturasByStatusActual(DataTable table)
        {
            List<FacturaItemResponse> list = new List<FacturaItemResponse>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(_facturaConverterL04.ConverterFacturaItem(row));
            }
            return list;
        }
    }
}
