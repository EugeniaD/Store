using Store.API.Configuration;
using Store.API;
using Store.API.Models.InputModels;
using Store.API.Models.OutputModels;
using Store.Core;
using Store.DB.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Store.Core.ConfigurationOptions;

namespace Store.IntegrationTests
{
    public class TestHelper : IoCSupport<AutofacModule>
    {
        protected async ValueTask DropOrRestoreTestDbs(string name)
        {
            string connectionString = Resolve<IOptions<StorageOptions>>().Value.DBMasterConnectionString;
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                conn.ChangeDatabase("master");
                await conn.QueryAsync(
                    name,
                    null,
                    transaction: null,
                    commandTimeout: 3600,
                    commandType: CommandType.StoredProcedure);
            }
        }
        protected void DeepEqualForOrder(OrderInputModel inputModel, OrderOutputModel expOutputModel)
        {
            Assert.NotNull(expOutputModel.Date);
            Assert.NotNull(expOutputModel.WarehouseName);
            Assert.NotNull(expOutputModel.OrderDetailsOutput);
            var model = mapper.Map<Order>(inputModel);
            var outputModel = mapper.Map<OrderOutputModel>(model);
            Assert.IsTrue(outputModel.Equals(expOutputModel));
        }
          protected void DeepEqualForOrderDetails(OrderDetailsInputModel inputModel, OrderDetailsOutputModel expOutputModel)
        {
            Assert.NotNull(expOutputModel.LocalPrice);
            Assert.NotNull(expOutputModel.ProductBrand);
            Assert.NotNull(expOutputModel.ProductModel);
            Assert.NotNull(expOutputModel.Quantity);
            Assert.NotNull(expOutputModel.SubCategoryName);
            var model = mapper.Map<OrderDetails>(inputModel);
            var outputModel = mapper.Map<OrderDetailsOutputModel>(model);
            Assert.IsTrue(outputModel.Equals(expOutputModel));
        }

        protected T AssertAndConvert<T>(ActionResult<T> actionResult)
        {
            Assert.NotNull(actionResult);
            OkObjectResult result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            return (T)result.Value;
        }

        protected T AssertAndConvertSpecificResult<T>(ActionResult<T> actionResult)
        {
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            return (T)actionResult.Value;
        }

        protected void AssertAndConvertNotFoundActionResult<T>(ActionResult<T> actionResult)
        {
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Result);
            var result = actionResult.Result as NotFoundObjectResult;
            Assert.AreEqual(result.StatusCode, 404);
        }

        //protected void ChangeLead(LeadInputModel model)
        //{
        //    model.Email = $"reginaUberAlles@gmail.com{LeadInputModelMock.RandomNumber(1, 1000000000)}";
        //    model.Phone = $"+79995196034{LeadInputModelMock.RandomNumber(1, 1000000000)}";
        //    model.Login = $"regina{LeadInputModelMock.RandomNumber(1, 1000000000)}";
        //}
    }
}
