using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L04
{
    public class SessionConverter
    {
        public List<NpgsqlParameter> Converter(ValidateSessionRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_minutos_caduca", minutosCaduca));
            return parameters;
        }

        public ValidateSessionResponse Converter(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            ValidateSessionResponse response = new ValidateSessionResponse();
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
