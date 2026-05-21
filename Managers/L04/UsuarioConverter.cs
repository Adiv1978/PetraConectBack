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

        public List<NpgsqlParameter> Converter(LoginUsuarioRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_nick", request.Nick ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_pass", request.Pass ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_minutos_caduca", minutosCaduca));
            return parameters;
        }

        public LoginUsuarioResponse ConverterLoginUsuario(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            LoginUsuarioResponse response = new LoginUsuarioResponse();
            if (row.Table.Columns.Contains("isok") && row["isok"] != DBNull.Value)
                response.IsOk = Convert.ToBoolean(row["isok"]);
            if (row.Table.Columns.Contains("mensaje") && row["mensaje"] != DBNull.Value)
                response.Mensaje = Convert.ToString(row["mensaje"]);
            if (row.Table.Columns.Contains("idusuario") && row["idusuario"] != DBNull.Value)
                response.IdUsuario = Convert.ToInt64(row["idusuario"]);
            if (row.Table.Columns.Contains("idsesion") && row["idsesion"] != DBNull.Value)
                response.IdSesion = Convert.ToInt64(row["idsesion"]);
            if (row.Table.Columns.Contains("sessiontoken") && row["sessiontoken"] != DBNull.Value)
                response.SessionToken = Convert.ToString(row["sessiontoken"]);
            if (row.Table.Columns.Contains("feccaduca") && row["feccaduca"] != DBNull.Value)
                response.FecCaduca = Convert.ToDateTime(row["feccaduca"]);
            return response;
        }
    }
}
