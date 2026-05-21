using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L05
{
    public class UsuarioConverter
    {
        private readonly L04.UsuarioConverter _usuarioConverterL04;
        public UsuarioConverter()
        {
            _usuarioConverterL04 = new L04.UsuarioConverter();
        }

        public List<NpgsqlParameter> Converter(SetUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _usuarioConverterL04.Converter(request);
        }

        public List<SetUsuarioResponse> Converter(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<SetUsuarioResponse> list = new List<SetUsuarioResponse>();
            foreach (DataRow row in table.Rows)
            {
                SetUsuarioResponse item = _usuarioConverterL04.Converter(row);
                list.Add(item);
            }
            return list;
        }

        public List<NpgsqlParameter> Converter(UpdateUsuarioRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _usuarioConverterL04.Converter(request, minutosCaduca);
        }

        public List<UpdateUsuarioResponse> ConverterUpdateUsuario(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<UpdateUsuarioResponse> list = new List<UpdateUsuarioResponse>();
            foreach (DataRow row in table.Rows)
            {
                UpdateUsuarioResponse item = _usuarioConverterL04.ConverterUpdateUsuario(row);
                list.Add(item);
            }
            return list;
        }

        public List<NpgsqlParameter> Converter(LoginUsuarioRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _usuarioConverterL04.Converter(request, minutosCaduca);
        }

        public List<LoginUsuarioResponse> ConverterLoginUsuario(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<LoginUsuarioResponse> list = new List<LoginUsuarioResponse>();
            foreach (DataRow row in table.Rows)
            {
                LoginUsuarioResponse item = _usuarioConverterL04.ConverterLoginUsuario(row);
                list.Add(item);
            }
            return list;
        }
    }
}
