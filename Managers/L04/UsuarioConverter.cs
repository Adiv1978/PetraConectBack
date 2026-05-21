using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L04
{
    public class UsuarioConverter
    {
        public List<NpgsqlParameter> Converter(SetUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_nick", request.Nick ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_pass", request.Pass ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_emailalegra", request.EmailAlegra ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_keyalegra", request.KeyAlegra ?? (object)DBNull.Value));
            return parameters;
        }

        public SetUsuarioResponse Converter(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            SetUsuarioResponse response = new SetUsuarioResponse();
            if (row.Table.Columns.Contains("isok") && row["isok"] != DBNull.Value)
                response.IsOk = Convert.ToBoolean(row["isok"]);
            if (row.Table.Columns.Contains("mensaje") && row["mensaje"] != DBNull.Value)
                response.Mensaje = Convert.ToString(row["mensaje"]);
            if (row.Table.Columns.Contains("idusuario") && row["idusuario"] != DBNull.Value)
                response.IdUsuario = Convert.ToInt64(row["idusuario"]);
            return response;
        }

        public List<NpgsqlParameter> Converter(UpdateUsuarioRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_minutos_caduca", minutosCaduca));
            parameters.Add(new NpgsqlParameter("@p_pass", request.Pass ?? (object)DBNull.Value));
            return parameters;
        }
        public UpdateUsuarioResponse ConverterUpdateUsuario(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            UpdateUsuarioResponse response = new UpdateUsuarioResponse();
            if (row.Table.Columns.Contains("isok") && row["isok"] != DBNull.Value)
                response.IsOk = Convert.ToBoolean(row["isok"]);
            if (row.Table.Columns.Contains("mensaje") && row["mensaje"] != DBNull.Value)
                response.Mensaje = Convert.ToString(row["mensaje"]);
            if (row.Table.Columns.Contains("idusuario") && row["idusuario"] != DBNull.Value)
                response.IdUsuario = Convert.ToInt64(row["idusuario"]);
            return response;
        }
    }
}
