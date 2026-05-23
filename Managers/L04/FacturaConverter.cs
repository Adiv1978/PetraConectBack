using Npgsql;
using NpgsqlTypes;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;
using System.Text.Json;

namespace PetraConectBack.Managers.L04
{
    public class FacturaConverter
    {
        public List<NpgsqlParameter> Converter(GetFacturaRequest request, int minutosCaduca)
        {
            return new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_minutos_caduca", minutosCaduca),
                new NpgsqlParameter("@p_idfactura", request.IdFactura ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_idalegra", request.IdAlegra ?? (object)DBNull.Value)
            };
        }

        public List<NpgsqlParameter> Converter(GetFacturaByStatusRequest request, int minutosCaduca)
        {
            return new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_minutos_caduca", minutosCaduca),
                new NpgsqlParameter("@p_status", NpgsqlDbType.Varchar) { Value = request.Status ?? (object)DBNull.Value },
                new NpgsqlParameter("@p_limit", request.Limit ?? 100)
            };
        }



        public List<NpgsqlParameter> Converter(SetFacturaStatusRequest request, int minutosCaduca)
        {
            return new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_minutos_caduca", minutosCaduca),
                new NpgsqlParameter("@p_idfactura", request.IdFactura ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_nuevo_status", NpgsqlDbType.Varchar) { Value = request.NuevoStatus ?? (object)DBNull.Value },
                new NpgsqlParameter("@p_comentario", request.Comentario ?? (object)DBNull.Value)
            };
        }

        public List<NpgsqlParameter> Converter(SetFacturaDbRequest request, int minutosCaduca)
        {
            return new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_minutos_caduca", minutosCaduca),
                new NpgsqlParameter("@p_idalegra", request.IdAlegra ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_observacion", request.Observacion ?? (object)DBNull.Value),
                request.ReferenciasProductos == null || request.ReferenciasProductos.Count == 0
                    ? new NpgsqlParameter("@p_referenciasproductos", NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = DBNull.Value }
                    : new NpgsqlParameter("@p_referenciasproductos", NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = request.ReferenciasProductos.ToArray() }
            };
        }

        public FacturaItemResponse ConverterGetFactura(DataRow row)
        {
            return ConverterFacturaItem(row);
        }

        public FacturaItemResponse ConverterFacturaItem(DataRow row)
        {
            FacturaItemResponse item = new FacturaItemResponse();
            item.IdFactura = row["idfactura"] == DBNull.Value ? null : Convert.ToInt64(row["idfactura"]);
            item.IdAlegra = row["idalegra"] == DBNull.Value ? null : Convert.ToString(row["idalegra"]);
            item.RegFec = row["regfec"] == DBNull.Value ? null : Convert.ToDateTime(row["regfec"]);
            item.Observacion = row["observacion"] == DBNull.Value ? null : Convert.ToString(row["observacion"]);
            item.IsOpen = row["isopen"] != DBNull.Value && Convert.ToBoolean(row["isopen"]);
            item.StatusActual = row["status_actual"] == DBNull.Value ? null : Convert.ToString(row["status_actual"]);
            item.StatusFecReg = row["status_fecreg"] == DBNull.Value ? null : Convert.ToDateTime(row["status_fecreg"]);
            item.StatusComentario = row["status_comentario"] == DBNull.Value ? null : Convert.ToString(row["status_comentario"]);
            item.IdUsuarioStatus = row["idusuario_status"] == DBNull.Value ? null : Convert.ToInt64(row["idusuario_status"]);

            item.DetalleRawJson = row["detalle_json"] == DBNull.Value ? null : Convert.ToString(row["detalle_json"]);
            item.StatusRawJson = row["status_json"] == DBNull.Value ? null : Convert.ToString(row["status_json"]);

            if (!string.IsNullOrWhiteSpace(item.DetalleRawJson))
            {
                using JsonDocument doc = JsonDocument.Parse(item.DetalleRawJson);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement detalleElement in doc.RootElement.EnumerateArray())
                    {
                        item.Detalle.Add(ConverterFacturaDetalle(detalleElement));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(item.StatusRawJson))
            {
                using JsonDocument doc = JsonDocument.Parse(item.StatusRawJson);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement statusElement in doc.RootElement.EnumerateArray())
                    {
                        item.Status.Add(ConverterFacturaStatus(statusElement));
                    }
                }
            }

            return item;
        }



        public SetFacturaStatusResponse ConverterSetFacturaStatus(DataRow row)
        {
            return new SetFacturaStatusResponse
            {
                IsOk = row["isok"] != DBNull.Value && Convert.ToBoolean(row["isok"]),
                Mensaje = row["mensaje"] == DBNull.Value ? null : Convert.ToString(row["mensaje"]),
                IdFactura = row["idfactura"] == DBNull.Value ? null : Convert.ToInt64(row["idfactura"]),
                Status = row["status"] == DBNull.Value ? null : Convert.ToString(row["status"])
            };
        }

        public SetFacturaDbResponse ConverterSetFactura(DataRow row)
        {
            return new SetFacturaDbResponse
            {
                IsOk = row["isok"] != DBNull.Value && Convert.ToBoolean(row["isok"]),
                Mensaje = row["mensaje"] == DBNull.Value ? null : Convert.ToString(row["mensaje"]),
                IdFactura = row["idfactura"] == DBNull.Value ? null : Convert.ToInt64(row["idfactura"])
            };
        }

        public FacturaDetalleItemResponse ConverterFacturaDetalle(JsonElement detalleElement)
        {
            FacturaDetalleItemResponse item = new FacturaDetalleItemResponse { RawJson = detalleElement.GetRawText() };
            if (detalleElement.ValueKind != JsonValueKind.Object) return item;
            if (detalleElement.TryGetProperty("idFacturaDet", out JsonElement idFacturaDet) && idFacturaDet.ValueKind == JsonValueKind.Number && idFacturaDet.TryGetInt64(out long v1)) item.IdFacturaDet = v1;
            if (detalleElement.TryGetProperty("idProducto", out JsonElement idProducto) && idProducto.ValueKind == JsonValueKind.Number && idProducto.TryGetInt64(out long v2)) item.IdProducto = v2;
            if (detalleElement.TryGetProperty("idAlegra", out JsonElement idAlegra) && idAlegra.ValueKind == JsonValueKind.String) item.IdAlegra = idAlegra.GetString();
            if (detalleElement.TryGetProperty("referencia", out JsonElement referencia) && referencia.ValueKind == JsonValueKind.String) item.Referencia = referencia.GetString();
            if (detalleElement.TryGetProperty("nombre", out JsonElement nombre) && nombre.ValueKind == JsonValueKind.String) item.Nombre = nombre.GetString();
            if (detalleElement.TryGetProperty("descripcion", out JsonElement descripcion) && descripcion.ValueKind == JsonValueKind.String) item.Descripcion = descripcion.GetString();
            if (detalleElement.TryGetProperty("isCocina", out JsonElement isCocina))
            {
                if (isCocina.ValueKind == JsonValueKind.True || isCocina.ValueKind == JsonValueKind.False) item.IsCocina = isCocina.GetBoolean();
                else if (isCocina.ValueKind == JsonValueKind.String && bool.TryParse(isCocina.GetString(), out bool b)) item.IsCocina = b;
            }
            return item;
        }

        public FacturaStatusItemResponse ConverterFacturaStatus(JsonElement statusElement)
        {
            FacturaStatusItemResponse item = new FacturaStatusItemResponse { RawJson = statusElement.GetRawText() };
            if (statusElement.ValueKind != JsonValueKind.Object) return item;
            if (statusElement.TryGetProperty("idFacturaStatus", out JsonElement idFacturaStatus) && idFacturaStatus.ValueKind == JsonValueKind.Number && idFacturaStatus.TryGetInt64(out long v1)) item.IdFacturaStatus = v1;
            if (statusElement.TryGetProperty("idUsuario", out JsonElement idUsuario) && idUsuario.ValueKind == JsonValueKind.Number && idUsuario.TryGetInt64(out long v2)) item.IdUsuario = v2;
            if (statusElement.TryGetProperty("usuarioNick", out JsonElement usuarioNick) && usuarioNick.ValueKind == JsonValueKind.String) item.UsuarioNick = usuarioNick.GetString();
            if (statusElement.TryGetProperty("fecReg", out JsonElement fecReg) && fecReg.ValueKind == JsonValueKind.String && DateTime.TryParse(fecReg.GetString(), out DateTime d)) item.FecReg = d;
            if (statusElement.TryGetProperty("comentario", out JsonElement comentario) && comentario.ValueKind == JsonValueKind.String) item.Comentario = comentario.GetString();
            if (statusElement.TryGetProperty("status", out JsonElement status) && status.ValueKind == JsonValueKind.String) item.Status = status.GetString();
            return item;
        }
    }
}
