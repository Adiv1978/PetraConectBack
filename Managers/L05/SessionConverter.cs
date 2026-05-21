using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L05
{
    public class SessionConverter
    {
        private readonly L04.SessionConverter _sessionConverterL04;

        public SessionConverter()
        {
            _sessionConverterL04 = new L04.SessionConverter();
        }

        public List<NpgsqlParameter> Converter(ValidateSessionRequest request, int minutosCaduca)
        {
            return _sessionConverterL04.Converter(request, minutosCaduca);
        }

        public List<ValidateSessionResponse> Converter(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<ValidateSessionResponse> list = new List<ValidateSessionResponse>();
            foreach (DataRow row in table.Rows)
                list.Add(_sessionConverterL04.Converter(row));
            return list;
        }
    }
}
