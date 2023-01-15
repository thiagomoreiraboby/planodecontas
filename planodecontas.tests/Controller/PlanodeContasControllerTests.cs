using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using planodecontas.domain.Vos;
using planodecontas.infra.DBContexts;
using planodecontas.domain.Repositorios;
using planodecontas.application.Contrato;
using planodecontas.application.Utils;
using Newtonsoft.Json;
using planodecontas.application.DTOs;
using planodecontas.domain.Enuns;

namespace planodecontas.tests.Controller
{
    public class PlanodeContasControllerTests : BaseControllerTest
    {
        const string url_base = "api/PlanodeContas";



        [Fact]
        public async Task GetPlanodeContasSuccess()
        {
            var url = $"{url_base}/GetPlanodeContas";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<IEnumerable<PlanodeContaVo>>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.Any());
        }

        [Theory]
        [InlineData("1 Receitas", "Receitas")]
        [InlineData("2 Despesas", "Despesas")]
        [InlineData("3 Despesas bancárias", "Despesas bancárias")]
        [InlineData("4 Outras receitas", "Outras receitas")]
        [InlineData("4", "Outras receitas")]
        [InlineData("Outras receitas", "Outras receitas")]
        public async Task GetPlanodeContasSuccessComFiltro(string descricaoConta, string nome)
        {
            var url = $"{url_base}/GetPlanodeContas?descricaoConta={descricaoConta}";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<IEnumerable<PlanodeContaVo>>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.FirstOrDefault().Nome == nome);
        }

        [Fact]
        public async Task GetPlanodeContasPaiSuccess()
        {
            var url = $"{url_base}/GetPlanodeContasPai";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<IEnumerable<PlanodeContaPaiVo>>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.Any());
        }

        [Theory]
        [InlineData(1, "1.2")]
        [InlineData(3, "2.2")]
        [InlineData(6, "3.2")]
        [InlineData(8, "4.2")]
        [InlineData(null, "10")]
        public async Task GetCodigoSugeridoByIdContaPai(int? id, string codigoSugerido)
        {
            var url = $"{url_base}/GetCodigoSugeridoByIdContaPai?id={id}";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<CodigoSugeridoDto>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.CodigoSugerido == codigoSugerido);
        }

        [Theory]
        [InlineData(2, "1.2")]
        [InlineData(5, "2.2")]
        public async Task GetCodigoSugeridoByIdContaPaiFalha(int? id, string codigoSugerido)
        {
            var url = $"{url_base}/GetCodigoSugeridoByIdContaPai?id={id}";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(!generic.Success);
            Assert.True(!string.IsNullOrEmpty(generic.ErrorMessage));
        }

        [Fact]
        public async Task SalvarPlanodeContasSucesso()
        {
            var dto = new PlanodeContaDto("1.2", "Teste", TipoMovimentacao.Receita, true, 1);
            var url = $"{url_base}";
            var request = CreateRequestMessage(HttpMethod.Post, url, dto);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = generic.Content.ToString();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn == "True");
        }

        [Fact]
        public async Task SalvarPlanodeContasFalha_no_api()
        {
            var dto = new PlanodeContaDto("1.2", "Teste", TipoMovimentacao.Receita, true, 2);
            var url = $"{url_base}";
            var request = CreateRequestMessage(HttpMethod.Post, url, dto);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(!generic.Success);
            Assert.True(!string.IsNullOrEmpty(generic.ErrorMessage));
        }

        [Fact]
        public async Task SalvarPlanodeContasFalha_codigo_repetido()
        {
            var dto = new PlanodeContaDto("1.1", "Teste", TipoMovimentacao.Receita, true, 1);
            var url = $"{url_base}";
            var request = CreateRequestMessage(HttpMethod.Post, url, dto);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(!generic.Success);
            Assert.True(!string.IsNullOrEmpty(generic.ErrorMessage));
        }

        [Fact]
        public async Task DeletePlanodeContasFalha_com_filhas()
        {
            var url = $"{url_base}/1";
            var request = CreateRequestMessage(HttpMethod.Delete, url);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(!generic.Success);
            Assert.True(!string.IsNullOrEmpty(generic.ErrorMessage));
            Assert.True(generic.ErrorMessage.Contains("Esta conta tem contas filhas, delete as contas filhas primeiro!"));
            
        }

        [Fact]
        public async Task DeletePlanodeContasSucesso()
        {
            var url = $"{url_base}/5";
            var request = CreateRequestMessage(HttpMethod.Delete, url);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = generic.Content.ToString();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn == "True");
        }


        [Fact]
        public async Task ValidarQuandoexcedeONumeroMaximoDeFilhos()
        {
            var dto = new PlanodeContaDto("1.1000", "Teste", TipoMovimentacao.Receita, true, 1);
            var url = $"{url_base}";
            var request = CreateRequestMessage(HttpMethod.Post, url, dto);

            //Act
            var response = await Client.SendAsync(request);
            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(!generic.Success);
            Assert.True(!string.IsNullOrEmpty(generic.ErrorMessage));
            Assert.True(generic.ErrorMessage.Contains("Não foi possível salvar o plano de conta: Excedeu o codigo maximo: 999!"));
            
        }

        [Fact]
        public async Task ValidarCodigoSugeridoQuandoMaximoDeFilhosjafoiatingido()
        {
            var dto = new PlanodeContaDto("1.999", "Teste", TipoMovimentacao.Receita, true, 1);
            var url = $"{url_base}";
            var request = CreateRequestMessage(HttpMethod.Post, url, dto);
            await Client.SendAsync(request);

            url = $"{url_base}/GetCodigoSugeridoByIdContaPai?id=1";
            request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);

            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<CodigoSugeridoDto>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.CodigoSugerido == "10");

        }


        [Fact]
        public async Task ValidarCodigoSugeridoQuandoMaximoDeFilhosjafoiatingido_variosniveis()
        {

            var url = $"{url_base}/GetCodigoSugeridoByIdContaPai?id=13";
            var request = CreateRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await Client.SendAsync(request);

            var generic = await response.Content.ReadFromJsonAsync<GenericResult>();
            var objReturn = JsonConvert.DeserializeObject<CodigoSugeridoDto>(generic.Content.ToString());

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(objReturn?.CodigoSugerido == "9.11");

        }


    }
}
