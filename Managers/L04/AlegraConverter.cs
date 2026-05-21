using PetraConectBack.Types.External.Alegra;
using PetraConectBack.Types.Request;
using System.Text.Json;

namespace PetraConectBack.Managers.L04
{
    public class AlegraConverter
    {
        public void AddGetItemsOptionalQueryParams(GetItemsRequest request, Dictionary<string, string?> queryParams)
        {
            if (request.Start.HasValue) queryParams["start"] = request.Start.Value.ToString();
            if (!string.IsNullOrWhiteSpace(request.OrderDirection)) queryParams["order_direction"] = request.OrderDirection;
            if (!string.IsNullOrWhiteSpace(request.OrderField)) queryParams["order_field"] = request.OrderField;
            if (!string.IsNullOrWhiteSpace(request.Query)) queryParams["query"] = request.Query;
            if (request.Metadata.HasValue) queryParams["metadata"] = request.Metadata.Value.ToString().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(request.IdWarehouse)) queryParams["idWarehouse"] = request.IdWarehouse;
            if (!string.IsNullOrWhiteSpace(request.Name)) queryParams["name"] = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Reference)) queryParams["reference"] = request.Reference;
            if (!string.IsNullOrWhiteSpace(request.Description)) queryParams["description"] = request.Description;
            if (!string.IsNullOrWhiteSpace(request.PriceListId)) queryParams["priceList_id"] = request.PriceListId;
            if (!string.IsNullOrWhiteSpace(request.IdItemCategory)) queryParams["idItemCategory"] = request.IdItemCategory;
            if (!string.IsNullOrWhiteSpace(request.Type)) queryParams["type"] = request.Type;
            if (!string.IsNullOrWhiteSpace(request.Status)) queryParams["status"] = request.Status;
            if (request.Inventariable.HasValue) queryParams["inventariable"] = request.Inventariable.Value.ToString().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(request.Fields)) queryParams["fields"] = request.Fields;
            if (!string.IsNullOrWhiteSpace(request.Mode)) queryParams["mode"] = request.Mode;
        }

        public AlegraInvoiceResponse ConverterGetLastFact(JsonElement invoiceElement)
        {
            AlegraInvoiceResponse response = new AlegraInvoiceResponse();
            response.RawJson = invoiceElement.GetRawText();

            if (invoiceElement.TryGetProperty("id", out JsonElement idElement))
                response.Id = idElement.ToString();
            if (invoiceElement.TryGetProperty("date", out JsonElement dateElement) && dateElement.ValueKind == JsonValueKind.String)
                response.Date = dateElement.GetString();
            if (invoiceElement.TryGetProperty("dueDate", out JsonElement dueDateElement) && dueDateElement.ValueKind == JsonValueKind.String)
                response.DueDate = dueDateElement.GetString();
            if (invoiceElement.TryGetProperty("datetime", out JsonElement datetimeElement) && datetimeElement.ValueKind == JsonValueKind.String)
                response.Datetime = datetimeElement.GetString();
            if (invoiceElement.TryGetProperty("observations", out JsonElement observationsElement) && observationsElement.ValueKind == JsonValueKind.String)
                response.Observations = observationsElement.GetString();
            if (invoiceElement.TryGetProperty("anotation", out JsonElement anotationElement) && anotationElement.ValueKind == JsonValueKind.String)
                response.Anotation = anotationElement.GetString();
            if (invoiceElement.TryGetProperty("termsConditions", out JsonElement termsConditionsElement) && termsConditionsElement.ValueKind == JsonValueKind.String)
                response.TermsConditions = termsConditionsElement.GetString();
            if (invoiceElement.TryGetProperty("status", out JsonElement statusElement))
                response.Status = statusElement.ToString();
            if (invoiceElement.TryGetProperty("total", out JsonElement totalElement) && totalElement.TryGetDecimal(out decimal totalValue))
                response.Total = totalValue;
            if (invoiceElement.TryGetProperty("totalPaid", out JsonElement totalPaidElement) && totalPaidElement.TryGetDecimal(out decimal totalPaidValue))
                response.TotalPaid = totalPaidValue;
            if (invoiceElement.TryGetProperty("balance", out JsonElement balanceElement) && balanceElement.TryGetDecimal(out decimal balanceValue))
                response.Balance = balanceValue;

            if (invoiceElement.TryGetProperty("client", out JsonElement clientElement) && clientElement.ValueKind == JsonValueKind.Object)
            {
                if (clientElement.TryGetProperty("id", out JsonElement clientIdElement))
                    response.ClientId = clientIdElement.ToString();
                if (clientElement.TryGetProperty("name", out JsonElement clientNameElement) && clientNameElement.ValueKind == JsonValueKind.String)
                    response.ClientName = clientNameElement.GetString();
                if (clientElement.TryGetProperty("identification", out JsonElement clientIdentificationElement))
                    response.ClientIdentification = clientIdentificationElement.ToString();
                if (clientElement.TryGetProperty("email", out JsonElement clientEmailElement) && clientEmailElement.ValueKind == JsonValueKind.String)
                    response.ClientEmail = clientEmailElement.GetString();
            }

            if (invoiceElement.TryGetProperty("numberTemplate", out JsonElement numberTemplateElement))
            {
                JsonElement numberTemplateObject = numberTemplateElement;
                if (numberTemplateElement.ValueKind == JsonValueKind.Array && numberTemplateElement.GetArrayLength() > 0)
                    numberTemplateObject = numberTemplateElement[0];

                if (numberTemplateObject.ValueKind == JsonValueKind.Object)
                {
                    if (numberTemplateObject.TryGetProperty("id", out JsonElement numberTemplateIdElement))
                        response.NumberTemplateId = numberTemplateIdElement.ToString();
                    if (numberTemplateObject.TryGetProperty("prefix", out JsonElement numberTemplatePrefixElement) && numberTemplatePrefixElement.ValueKind == JsonValueKind.String)
                        response.NumberTemplatePrefix = numberTemplatePrefixElement.GetString();
                    if (numberTemplateObject.TryGetProperty("number", out JsonElement numberTemplateNumberElement))
                        response.NumberTemplateNumber = numberTemplateNumberElement.ToString();
                }
            }

            if (invoiceElement.TryGetProperty("items", out JsonElement itemsElement) && itemsElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement itemElement in itemsElement.EnumerateArray())
                    response.Items.Add(ConverterGetLastFactItem(itemElement));
            }

            return response;
        }

        public AlegraInvoiceItemResponse ConverterGetLastFactItem(JsonElement itemElement)
        {
            AlegraInvoiceItemResponse response = new AlegraInvoiceItemResponse();
            response.RawJson = itemElement.GetRawText();
            if (itemElement.TryGetProperty("id", out JsonElement idElement))
                response.Id = idElement.ToString();
            if (itemElement.TryGetProperty("description", out JsonElement descriptionElement) && descriptionElement.ValueKind == JsonValueKind.String)
                response.Description = descriptionElement.GetString();
            if (itemElement.TryGetProperty("quantity", out JsonElement quantityElement) && quantityElement.TryGetDecimal(out decimal quantityValue))
                response.Quantity = quantityValue;
            if (itemElement.TryGetProperty("price", out JsonElement priceElement) && priceElement.TryGetDecimal(out decimal priceValue))
                response.Price = priceValue;
            if (itemElement.TryGetProperty("discount", out JsonElement discountElement) && discountElement.TryGetDecimal(out decimal discountValue))
                response.Discount = discountValue;
            if (itemElement.TryGetProperty("total", out JsonElement totalElement) && totalElement.TryGetDecimal(out decimal totalValue))
                response.Total = totalValue;
            if (itemElement.TryGetProperty("item", out JsonElement itemSubElement) && itemSubElement.ValueKind == JsonValueKind.Object)
            {
                if (itemSubElement.TryGetProperty("id", out JsonElement idItemElement)) response.IdItem = idItemElement.ToString();
                if (itemSubElement.TryGetProperty("name", out JsonElement nameElement) && nameElement.ValueKind == JsonValueKind.String) response.Name = nameElement.GetString();
                if (itemSubElement.TryGetProperty("reference", out JsonElement referenceElement) && referenceElement.ValueKind == JsonValueKind.String) response.Reference = referenceElement.GetString();
            }
            if (itemElement.TryGetProperty("product", out JsonElement productElement) && productElement.ValueKind == JsonValueKind.Object)
            {
                if (string.IsNullOrWhiteSpace(response.IdItem) && productElement.TryGetProperty("id", out JsonElement idProductElement)) response.IdItem = idProductElement.ToString();
                if (string.IsNullOrWhiteSpace(response.Name) && productElement.TryGetProperty("name", out JsonElement productNameElement) && productNameElement.ValueKind == JsonValueKind.String) response.Name = productNameElement.GetString();
                if (string.IsNullOrWhiteSpace(response.Reference) && productElement.TryGetProperty("reference", out JsonElement productReferenceElement) && productReferenceElement.ValueKind == JsonValueKind.String) response.Reference = productReferenceElement.GetString();
            }
            if (string.IsNullOrWhiteSpace(response.Name) && itemElement.TryGetProperty("name", out JsonElement nameRootElement) && nameRootElement.ValueKind == JsonValueKind.String) response.Name = nameRootElement.GetString();
            if (string.IsNullOrWhiteSpace(response.Reference) && itemElement.TryGetProperty("reference", out JsonElement referenceRootElement) && referenceRootElement.ValueKind == JsonValueKind.String) response.Reference = referenceRootElement.GetString();
            return response;
        }

        public AlegraItemResponse ConverterGetItems(JsonElement itemElement)
        {
            AlegraItemResponse response = new AlegraItemResponse { RawJson = itemElement.GetRawText(), Inventariable = false };
            if (itemElement.TryGetProperty("id", out JsonElement idElement)) response.Id = idElement.ToString();
            if (itemElement.TryGetProperty("name", out JsonElement nameElement) && nameElement.ValueKind == JsonValueKind.String) response.Name = nameElement.GetString();
            if (itemElement.TryGetProperty("description", out JsonElement descriptionElement) && descriptionElement.ValueKind == JsonValueKind.String) response.Description = descriptionElement.GetString();
            if (itemElement.TryGetProperty("reference", out JsonElement referenceElement))
            {
                if (referenceElement.ValueKind == JsonValueKind.String) response.Reference = referenceElement.GetString();
                else if (referenceElement.ValueKind == JsonValueKind.Object && referenceElement.TryGetProperty("reference", out JsonElement referenceSubElement) && referenceSubElement.ValueKind == JsonValueKind.String) response.Reference = referenceSubElement.GetString();
            }
            if (itemElement.TryGetProperty("status", out JsonElement statusElement)) response.Status = statusElement.ToString();
            if (itemElement.TryGetProperty("type", out JsonElement typeElement)) response.Type = typeElement.ToString();
            if (itemElement.TryGetProperty("category", out JsonElement categoryElement) && categoryElement.ValueKind == JsonValueKind.Object)
            {
                if (categoryElement.TryGetProperty("id", out JsonElement categoryIdElement)) response.CategoryId = categoryIdElement.ToString();
                if (categoryElement.TryGetProperty("name", out JsonElement categoryNameElement) && categoryNameElement.ValueKind == JsonValueKind.String) response.CategoryName = categoryNameElement.GetString();
            }
            if (itemElement.TryGetProperty("inventory", out JsonElement inventoryElement) && inventoryElement.ValueKind == JsonValueKind.Object)
            {
                response.Inventariable = true;
                if (inventoryElement.TryGetProperty("unit", out JsonElement unitElement) && unitElement.ValueKind == JsonValueKind.String) response.InventoryUnit = unitElement.GetString();
                if (inventoryElement.TryGetProperty("availableQuantity", out JsonElement availableQuantityElement) && availableQuantityElement.TryGetDecimal(out decimal availableQuantity)) response.AvailableQuantity = availableQuantity;
                if (inventoryElement.TryGetProperty("unitCost", out JsonElement unitCostElement) && unitCostElement.TryGetDecimal(out decimal unitCost)) response.UnitCost = unitCost;
                if (inventoryElement.TryGetProperty("initialQuantity", out JsonElement initialQuantityElement) && initialQuantityElement.TryGetDecimal(out decimal initialQuantity)) response.InitialQuantity = initialQuantity;
            }
            if (itemElement.TryGetProperty("price", out JsonElement priceElement) && priceElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement price in priceElement.EnumerateArray()) response.Prices.Add(ConverterGetItemPrice(price));
            }
            return response;
        }

        public AlegraItemPriceResponse ConverterGetItemPrice(JsonElement priceElement)
        {
            AlegraItemPriceResponse response = new AlegraItemPriceResponse { RawJson = priceElement.GetRawText() };
            if (priceElement.TryGetProperty("idPriceList", out JsonElement idPriceListElement)) response.IdPriceList = idPriceListElement.ToString();
            if (priceElement.TryGetProperty("name", out JsonElement nameElement) && nameElement.ValueKind == JsonValueKind.String) response.Name = nameElement.GetString();
            if (priceElement.TryGetProperty("price", out JsonElement priceValueElement) && priceValueElement.TryGetDecimal(out decimal price)) response.Price = price;
            return response;
        }
    }
}
